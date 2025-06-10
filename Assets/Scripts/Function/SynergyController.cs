using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyController
{
    #region Members : Private
    private Dictionary<string, List<Hero>> synergies = new Dictionary<string, List<Hero>>();
    #endregion

    #region Methods : Public
    public void AddSynergy(Hero hero)
    {
        if(synergies.ContainsKey(hero.HeroData.name))
        {
            if(synergies[hero.HeroData.name].Count == 0)
            {
                synergies[hero.HeroData.name].Add(hero);
                SendData();
            }
            else
            {
                synergies[hero.HeroData.name].Add(hero);
            }
        }
        else
        {
            synergies.Add(hero.HeroData.name, new List<Hero>());
            synergies[hero.HeroData.name].Add(hero);
            SendData();
        }

        void SendData()
        {
            var command = new UIPageCommand(UIPageString.Synergy, true);
            command.SetData(CommandDataString.AddSynergy.ToString(), hero.HeroData);
            command.Excute();
        }
    }

    public void DeleteSynergy(Hero hero)
    {
        if(synergies.ContainsKey(hero.HeroData.name))
        {
            var list = synergies[hero.HeroData.name];
            list.Remove(hero);
            if(list.Count == 0)
            {
                //synergies[hero.HeroData.name].RemoveAt(0);
                var command = new UIPageCommand(UIPageString.Synergy, true);
                command.SetData(CommandDataString.DeleteSynergy.ToString(), hero.HeroData);
                command.Excute();
            }
        }
    }
    #endregion

    #region Methods : Private 
    #endregion
}
