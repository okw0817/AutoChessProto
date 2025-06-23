using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : IInitializer
{
    #region Members : Private
    private TileController tileController;
    private List<Hero> stageHeroes;
    private List<Hero> enemies;
    private List<HeroPosition> heroPositions;
    // List<Hero> heroDeadList;
    private int curRound;
    #endregion

    #region Members : Public
    public int CurRound { get => curRound; }
    #endregion

    #region Metdhos : Mono
    public RoundController(TileController tileController, List<Hero> stageHeroes, List<Hero> enemies)
    {
        this.tileController = tileController;
        this.stageHeroes = stageHeroes;
        this.enemies = enemies;
    }
    #endregion

    #region Methods : Interface
    public void Init()
    {
        curRound = 0;
        //heroDeadList = new List<Hero>();
        heroPositions = new List<HeroPosition>();
    }
    #endregion

    #region Methods : Public
    public void SetNextRound()
    {
        enemies.Clear();

        //SetEnemies
        ++curRound;
        HeroInitPosition();

        var roundData = ResourceManager.Instance.GetEnemies(curRound);
        foreach(var round in roundData)
        {
            AutoChessMaster.Instance.AddEnemy(round.name, (round.x, round.y), round.grade);
        }
    }

    public void SaveHeroPosition()
    {
        heroPositions.Clear();
        foreach (var hero in stageHeroes)
        {
            heroPositions.Add(new HeroPosition(hero, (hero.CurTile.Index.Item1, hero.CurTile.Index.Item2)));
        }
    }

    public void AddDeadList(Hero hero)
    {
        //heroDeadList.Add(hero);
    }
    #endregion

    #region Members : Private
    private void HeroInitPosition()
    {
        if (heroPositions.Count == 0)
            return;

        stageHeroes.Clear();

        foreach (var position in heroPositions)
        {
            if(position.Hero != null)
            {
                position.Hero.CurTile.StandingHero = null;
                position.Hero.CurTile = position.InitTile;
                position.Hero.CurTile.StandingHero = position.Hero;
                position.Hero.transform.position = position.InitTile.transform.position;
                position.Hero.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                position.Hero.InitializeState();
                position.Hero.gameObject.SetActive(true);
                stageHeroes.Add(position.Hero);
            }
        }
    }

    private void InitializeTile()
    {
        var enumerator = tileController.AllTiles;
        while(enumerator.MoveNext())
        {
            var enumerator2 = enumerator.Current.Tiles;
            while(enumerator2.MoveNext())
            {
                if(enumerator2.Current.StandingHero != null && enumerator2.Current.StandingHero.HeroTeam == Team.Enemy)
                {
                    var chessMaster = AutoChessMaster.Instance;
                    enumerator2.Current.StandingHero.CurTile = null;
                    //chessMaster.DeleteHeroUI(enumerator2.Current.StandingHero);
                    //chessMaster.PushPrefabPool(enumerator2.Current.StandingHero.HeroData.name, enumerator2.Current.StandingHero.gameObject);
                    chessMaster.DeleteHero(enumerator2.Current.StandingHero);
                    enumerator2.Current.StandingHero = null;
                }
            }
        }
    }
    #endregion
}

public class HeroPosition
{
    #region Members : Private
    private Hero hero;
    private Tile tile;
    private (int, int) position;
    #endregion

    #region Members : Property
    public Hero Hero { get => hero; }
    public (int, int) Position { get => position; }
    public Tile InitTile { get => tile; }
    #endregion

    #region Members : Constructor
    public HeroPosition(Hero hero, (int, int) position)
    {
        this.hero = hero;
        this.position = position;
        this.tile = hero.CurTile;
    }
    #endregion


}
