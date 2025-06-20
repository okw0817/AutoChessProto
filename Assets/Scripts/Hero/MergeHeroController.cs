using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeHeroController
{
    #region Members : Private
    private SynergyController synergyController;
    private Dictionary<string, List<Hero>> spawnHeroList = new Dictionary<string, List<Hero>>();
    private List<Hero> heroList;

    #endregion

    #region Methods : Private
    public MergeHeroController(SynergyController synergyController, List<Hero> heroList)
    {
        this.synergyController = synergyController;
        this.heroList = heroList;
    }
    #endregion

    #region Methods : PUblic
    public void AddHero(Hero hero, bool isStart = false)
    {
        if (!spawnHeroList.ContainsKey(hero.HeroData.name))
        {
            spawnHeroList.Add(hero.HeroData.name, new List<Hero>());
            spawnHeroList[hero.HeroData.name].Add(hero);
        }
        else
        {
            spawnHeroList[hero.HeroData.name].Add(hero);
        }

        if (!isStart)
        {
            Hero UpgradeHero = MergeHero(hero);

            if(UpgradeHero != null && UpgradeHero.CurGrade == 2)
                MergeHero(UpgradeHero);
        }
    }

    public void DeleteHero(Hero hero)
    {
        if (spawnHeroList.ContainsKey(hero.HeroData.name))
        {
            spawnHeroList[hero.HeroData.name].Remove(hero);
        }
    }

    public void ClearHeroList()
    {
        foreach (var list in spawnHeroList)
        {
            list.Value.Clear();
        }
    }

    public Hero MergeHero(Hero hero)
    {
        var tempList = new List<Hero>();
        foreach (var obj in spawnHeroList[hero.HeroData.name])
        {
            if (obj.CurGrade == hero.CurGrade)
                tempList.Add(obj);

            if (tempList.Count == 3)
                break;
        }


        if (tempList.Count < 3)
            return null;

        var upgradeHero = tempList[0];
        upgradeHero.CurGrade += 1;
        tempList.Remove(upgradeHero);

        foreach (var tempHero in tempList)
        {
            tempHero.CurTile.StandingHero = null;
            synergyController.DeleteSynergy(tempHero);
            heroList.Remove(tempHero);
            DeleteHero(tempHero);
            AutoChessMaster.Instance.PushPrefabPool(tempHero.HeroData.name, tempHero.gameObject);
        }

        return upgradeHero;
    }
    #endregion
}
