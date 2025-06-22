using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class AutoChessMaster : SigletoneBase<AutoChessMaster>
{
    #region Members : Private
    [SerializeField]
    private PrefabPool prefabPool;

    private SynergyController synergyController;
    private TileController tileController;
    private HeroWatingRoom heroWatingRoom;
    private HeroBehaviorController heroBehaviorController;
    private ProjectileController projectileController;
    private MergeHeroController mergeHeroController;
    private RoundController roundController;
    private HeroUIController heroUIController;
    private PickUp pickup;

    private Dictionary<int, List<HeroData>> heroDic = new Dictionary<int, List<HeroData>>();
    private List<Hero> enemyList = new List<Hero>();
    private List<Hero> stageHeroList = new List<Hero>();

    private int curLevel = 1;
    private int requireExperience = 0;
    private int curExperience = 0;
    private int maxStoreLevel = 8;
    private int maxStoreList = 5;

    private bool gameStart = false;
    #endregion

    #region Members : Properties
    public int CurLevel { 
        get => curLevel; 
        set
        {
            curLevel = value;
            var command = new UIPageCommand(UIPageString.Store, true);
            command.SetData(UIDataType.Level.ToString(), curLevel);
            command.SetData(UIDataType.Probability.ToString(), ResourceManager.Instance.GetProbabilityLevel(curLevel));
            command.Excute();
        }
    }
    #endregion

    #region Methods : Mono
    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        PickupSequence();
    }

    private void FixedUpdate()
    {
        if(mergeHeroController != null && heroUIController != null)
        {
            var enumerator = mergeHeroController.heroes;
            while (enumerator.MoveNext())
            {
                heroUIController.UpdateHeroUI(enumerator.Current.Value.GetEnumerator(), enemyList.GetEnumerator());
            }
        }

        if (!gameStart)
            return;

        heroBehaviorController.UpdateMove();
        projectileController.UpdateMove();
    }

    private void Start()
    {
        roundController.SetNextRound();
    }
    #endregion

    #region Methods : Override
    public override void Init()
    {
        pickup = new PickUp();
        synergyController = new SynergyController();
        projectileController = new ProjectileController();    
        tileController = GetComponentInChildren<TileController>();
        heroWatingRoom = GetComponentInChildren<HeroWatingRoom>();
        heroBehaviorController = new HeroBehaviorController(tileController, stageHeroList, enemyList);
        mergeHeroController = new MergeHeroController(synergyController, stageHeroList);
        heroUIController = new HeroUIController(prefabPool);
        roundController = new RoundController(tileController, stageHeroList, enemyList);
        roundController.Init();
        heroWatingRoom.Init();

        for(int i=1; i<= maxStoreList; ++i)
        {
            heroDic.Add(i, new List<HeroData>());
        }

        requireExperience = ResourceManager.Instance.GetRequireExperience(curLevel);
        SeparateHeroData();
    }
    #endregion

    #region Methods : Public
    public Tile GetTiltePosition((int, int)index)
    {
        return tileController.GetTile(index.Item1, index.Item2);
    }

    public void StageStart()
    {
        gameStart = true;
        roundController.SaveHeroPosition();     
    }

    public void StageEnd()
    {
        gameStart = false;

        roundController.SetNextRound();

        foreach (var hero in stageHeroList)
        {
            hero.InitializeState();
        }

        RaiseExperience(3);
    }
    #endregion

    #region Methods : Private
    private void SeparateHeroData()
    {
        var enumerator = ResourceManager.Instance.Heroes;
        while (enumerator.MoveNext())
        {
            var hero = enumerator.Current;
            heroDic[hero.level].Add(hero);
        }
    }

    private void PickupSequence()
    {
        if (gameStart)
            return;

        if (Input.GetMouseButtonDown(0) && pickup.PickupObject == null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Tile")))
            {
                Debug.Log("Pickup");
                var tile = hit.transform.GetComponent<Tile>();
                if (tile != null && tile.StandingHero != null)
                {
                    pickup.Pickup(tile.StandingHero.gameObject);
                    if (tile.type == TileType.Stage)
                    {
                        DeleteHeroInController(tile.StandingHero);
                    }
                    tile.StandingHero = null;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && pickup.PickupObject != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Tile")))
            {
                Debug.Log("Drop");
                var tile = hit.transform.GetComponent<Tile>();
                if (tile != null && tile.StandingHero == null)
                {
                    tile.StandingHero = pickup.PickupObject.GetComponent<Hero>();

                    if (tile.StandingHero.HeroTeam == Team.Enemy)
                        return;

                    pickup.DropOff(tile.transform);
                    Debug.Log($"TilePos : {tile.Index.Item1},{tile.Index.Item2}");

                    if(tile.type == TileType.Stage )
                    {
                        tile.StandingHero.CurTile = tile;
                        AddHeroInController(tile.StandingHero);
                    }
                }
            }
            else if(Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Tile")))
            {

            }

        }

        if(pickup.PickupObject != null)
        {
            var mousePosition = Input.mousePosition;
            var screenPoint = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10.0f));
            pickup.Attach(screenPoint);
        }

    }

    private void AddHeroInController(Hero hero)
    {
        synergyController.AddSynergy(hero);
        stageHeroList.Add(hero);
    }

    private void DeleteHeroInController(Hero hero)
    {
        synergyController.DeleteSynergy(hero);
        stageHeroList.Remove(hero);
    }
    #endregion

    #region Methods : Public
    public void RaiseExperience(int amount)
    {
        if (maxStoreLevel == curLevel)
            return;

        curExperience += amount;

        if(requireExperience <= curExperience)
        {
            curExperience = curExperience - requireExperience;
            requireExperience = ResourceManager.Instance.GetRequireExperience(curLevel + 1);
            CurLevel += 1;


            Debug.Log($"LevelUp: {CurLevel}");
        }

        if (maxStoreLevel == curLevel)
            curExperience = requireExperience;

        var command = new UIPageCommand(UIPageString.Store, true);
        command.SetData(UIDataType.Exe.ToString(), new List<int>() { curExperience, requireExperience });
        command.Excute();

        Debug.Log($"RaiseExerience: {amount}");
    }
    public ProbabilityData GetProbabilityLevel(int curLevel)
    {
        return ResourceManager.Instance.GetProbabilityLevel(curLevel);
    }

    public List<HeroData> GetStoreList(ProbabilityData probabilityData)
    {
        List<HeroData> data = new List<HeroData>();

        for (int i = 0; i < maxStoreList; ++i)
        {
            int random = Random.Range(0, 100);
            int percent = 0;

            percent += probabilityData.one;
            int maxCount = 0;
            if (percent >= random)
            {
                maxCount = heroDic[1].Count;
                data.Add(heroDic[1][Random.Range(0, maxCount)]);
                continue;
            }

            percent += probabilityData.two;
            if (percent >= random)
            {
                maxCount = heroDic[2].Count;
                data.Add(heroDic[2][Random.Range(0, 3)]);
                continue;
            }
            percent += probabilityData.three;

            if (percent >= random)
            {
                maxCount = heroDic[3].Count;
                data.Add(heroDic[3][Random.Range(0, 3)]);
                continue;
            }
            percent += probabilityData.four;

            if (percent >= random)
            {
                maxCount = heroDic[4].Count;
                data.Add(heroDic[4][Random.Range(0, 0)]);
                continue;
            }
        }

        return data;
    }

    public async void AddHeroPrefab(string heroName, UI_Hero_Icon uI_Hero_Icon)
    {
        GameObject obj = prefabPool.PopPool(heroName);
        if (obj == null)
        {
            obj = await ResourceManager.Instance.GetAddressablesRasources(heroName);

            var heroData = ResourceManager.Instance.Heroes;
            while(heroData.MoveNext())
            {
                if(heroData.Current.name == heroName)
                {
                    var hero = obj.GetComponent<Hero>();
                    hero.SetHeroData(heroData.Current);
                    hero.HeroTeam = Team.Friendly;
                    hero.CurGrade = 1;
                    break;
                }
            }
        }

        var heroComponent = obj.GetComponent<Hero>();
        if (heroWatingRoom.AddHero(heroComponent))
        {
            uI_Hero_Icon.IsSale = true;
            mergeHeroController.AddHero(heroComponent);

            var ui = await heroUIController.GetUI<UI_HeroState>(ResorucesName.UI_HeroState);
            ui.DivisionTeamColor(Color.green);
            heroComponent.UI_HeroState = ui;
            heroComponent.InitializeState();
        }
        else
        {
            prefabPool.PushPool(heroName, obj);
        }
    }

    public async void AddEnemy(string heroName, (int, int) position, int grade)
    {
        GameObject obj = prefabPool.PopPool(heroName);
        if (obj == null)
        {
            obj = await ResourceManager.Instance.GetAddressablesRasources(heroName);

            var heroData = ResourceManager.Instance.Heroes;
            while (heroData.MoveNext())
            {
                if (heroData.Current.name == heroName)
                {
                    var enemy = obj.GetComponent<Hero>();
                    enemy.SetHeroData(heroData.Current);
                    enemy.HeroTeam = Team.Enemy;
                    var tile = tileController.GetTile(position.Item1, position.Item2);
                    enemy.CurTile = tile;
                    tile.StandingHero = enemy;
                    enemy.transform.position = tile.transform.position;
                    enemy.transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
                    enemy.CurGrade = grade;

                    var ui = await heroUIController.GetUI<UI_HeroState>(ResorucesName.UI_HeroState);
                    ui.DivisionTeamColor(Color.red);
                    enemy.UI_HeroState = ui;

                    enemy.InitializeState();

                    enemyList.Add(enemy);
                    break;
                }
            }

        }
    }

    public void DeleteHero(Hero hero)
    {
        if (hero.HeroTeam == Team.Friendly)
            stageHeroList.Remove(hero);
        else
            enemyList.Remove(hero);

        hero.CurTile.StandingHero = null;
        hero.CurTile = null;
        prefabPool.PushPool(hero.HeroData.name, hero.gameObject);
        heroUIController.PushPool(ResorucesName.UI_HeroState, hero.UI_HeroState.gameObject);
        hero.UI_HeroState = null;

        if (enemyList.Count == 0)
            StageEnd();

    }

    public void DieHero(Hero hero)
    {
        roundController.AddDeadList(hero);
        hero.CurTile.StandingHero = null;
        hero.CurTile = null;
    }

    public async UniTask<GameObject> GetPrefabInPool(string name)
    {
        GameObject obj = prefabPool.PopPool(name);
        if (obj == null)
        {
            obj = await ResourceManager.Instance.GetAddressablesRasources(name);
        }

        return obj;
    }

    public void PushPrefabPool(string name, GameObject gameObject)
    {
        prefabPool.PushPool(name, gameObject);
    }

    public void AddProjectile(Projectile projectile)
    {
        projectileController.AddProjectile(projectile);
    }

    public void DeleteProjectile(Projectile projectile)
    {
        projectileController.DeleteProjectile(projectile);
    }
    #endregion
}
