using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyController
{
    #region Members : Private
    private Dictionary<string, List<Hero>> synergies = new Dictionary<string, List<Hero>>();
    private Dictionary<string, int> synergyCount = new Dictionary<string, int>();
    private Dictionary<string, List<Hero>> sortSynergyDic = new Dictionary<string, List<Hero>>();
    #endregion

    #region Methods : Public
    public void AddSynergy(Hero hero)
    {
        string heroName = hero.HeroData.name;
        if (synergies.ContainsKey(heroName))
        {
            if(synergies[heroName].Count == 0)
            {
                synergies[heroName].Add(hero);
                SendData();
            }
            else
            {
                synergies[heroName].Add(hero);
            }

        }
        else
        {
            synergies.Add(heroName, new List<Hero>());
            synergies[heroName].Add(hero);
            SendData();
        }

        foreach (var synergy in hero.HeroData.synergies)
        {
            if (!sortSynergyDic.ContainsKey(synergy))
            {
                sortSynergyDic.Add(synergy, new List<Hero>());
                sortSynergyDic[synergy].Add(hero);
            }
            else
            {
                sortSynergyDic[synergy].Add(hero);
            }
        }

        void SendData()
        {
            var command = new UIPageCommand(UIPageString.Synergy, true);
            command.SetData(CommandDataString.AddSynergy.ToString(), hero.HeroData);
            command.SetData(CommandDataString.SynergyCount.ToString(), synergyCount);
            command.Excute();
        }

        ActiveSynergy(heroName);
    }

    public void DeleteSynergy(Hero hero)
    {
        string heroName = hero.HeroData.name;
        if (synergies.ContainsKey(heroName))
        {
            var list = synergies[heroName];
            list.Remove(hero);

            foreach (var synergy in hero.HeroData.synergies)
            {
                sortSynergyDic[synergy].Remove(hero);
            }

            if (list.Count == 0)
            {
                //synergies[hero.HeroData.name].RemoveAt(0);
                var command = new UIPageCommand(UIPageString.Synergy, true);
                command.SetData(CommandDataString.DeleteSynergy.ToString(), hero.HeroData);
                command.SetData(CommandDataString.SynergyCount.ToString(), synergyCount);
                command.Excute();
            }
            ActiveSynergy(heroName);
        }
    }
    #endregion

    #region Methods : Private
    private void ActiveSynergy(string heroName)
    {
        if (synergies[heroName].Count == 0)
            return;

        var synergyArr = synergies[heroName][0].HeroData.synergies;

        foreach(var synergyName in synergyArr)
        {
            foreach(var hero in sortSynergyDic[synergyName])
            {
                var synergyObjectable = hero.HasSynergy(synergyName);

                if(synergyObjectable != null)
                {
                    synergyObjectable.ActiveSynergy(synergyCount[synergyName]/2, hero);
                }
            }
        }

    }
    #endregion
}
