using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SynergyWarrior", menuName = "Scriptable Object/SynergyWarrior", order = int.MaxValue)]

public class SynergyWarrior : SynergyObjectable
{
    #region Members : Private
    private Dictionary<int, int> dic_Level;
    #endregion

    #region Methods : Override
    public override void ActiveSynergy(int level, Hero hero)
    {
        hero.max_HeroState.Damage = hero.CharacterState.Damage + dic_Level[level];
    }

    public override void Init()
    {
        dic_Level = new Dictionary<int, int>();
        dic_Level.Add(0, 0);
        dic_Level.Add(1, 10);
        dic_Level.Add(2, 20);
        dic_Level.Add(3, 30);
        dic_Level.Add(4, 50);
    }
    #endregion
}
