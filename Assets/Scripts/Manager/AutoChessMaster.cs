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
    private PickUp pickup;

    private Dictionary<int, List<HeroData>> heroDic = new Dictionary<int, List<HeroData>>();

    private int curLevel = 1;
    private int requireExperience = 0;
    private int curExperience = 0;
    private int maxStoreLevel = 8;
    private int maxStoreList = 5;
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
    #endregion

    #region Methods : Override
    public override void Init()
    {
        pickup = new PickUp();
        synergyController = new SynergyController();
        tileController = GetComponentInChildren<TileController>();
        heroWatingRoom = GetComponentInChildren<HeroWatingRoom>();
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
    public Vector3 GetTiltePosition(Vector3 pos)
    {
        return Vector3.zero;
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
                    if(tile.type == TileType.Stage)
                    {
                        synergyController.DeleteSynergy(tile.StandingHero);
                    }
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
                if (tile != null)
                {
                    tile.StandingHero = pickup.PickupObject.GetComponent<Hero>();
                    pickup.DropOff(tile.transform);

                    if(tile.type == TileType.Stage)
                    {
                        synergyController.AddSynergy(tile.StandingHero);
                    }
                }
            }
        }

        if(pickup.PickupObject != null)
        {
            var mousePosition = Input.mousePosition;
            var screenPoint = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10.0f));
            pickup.Attach(screenPoint);
        }

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
            if (percent >= random)
            {
                data.Add(heroDic[1][Random.Range(0, 3)]);
                continue;
            }

            percent += probabilityData.two;
            if (percent >= random)
            {
                data.Add(heroDic[2][Random.Range(0, 3)]);
                continue;
            }
            percent += probabilityData.three;

            if (percent >= random)
            {
                data.Add(heroDic[3][Random.Range(0, 3)]);
                continue;
            }
            percent += probabilityData.four;

            if (percent >= random)
            {
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
            obj = await ResourceManager.Instance.GetHeroRasources(heroName);

            var heroData = ResourceManager.Instance.Heroes;
            while(heroData.MoveNext())
            {
                if(heroData.Current.name == heroName)
                {
                    obj.GetComponent<Hero>().SetHeroData(heroData.Current);
                    break;
                }
            }

        }

        if (heroWatingRoom.AddHero(obj.GetComponent<Hero>()))
        {
            uI_Hero_Icon.IsSale = true;
        }
        else
        {
            prefabPool.PushPool(heroName, obj);
        }

    }
    #endregion
}
