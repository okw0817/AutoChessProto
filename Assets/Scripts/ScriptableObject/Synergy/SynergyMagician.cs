using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SynergyMagician", menuName = "Scriptable Object/SynergyMagician", order = int.MaxValue)]
public class SynergyMagician : SynergyObjectable
{
    #region Members : Private
    private Dictionary<int, (int,int)> dic_Level;
    #endregion

    #region Methods : Override
    public override void ActiveSynergy(int level, Hero hero)
    {
        hero.max_HeroState.gain_Attack_MP = hero.CharacterState.MaxMP - dic_Level[level].Item1;
        hero.max_HeroState.MagicDamage = hero.CharacterState.MagicDamage - dic_Level[level].Item2;
    }

    public override void Init()
    {
        dic_Level = new Dictionary<int, (int,int)>();
        dic_Level.Add(0, (0,0));
        dic_Level.Add(1, (5, 5));
        dic_Level.Add(2, (20, 10));
        dic_Level.Add(3, (30, 15));
        dic_Level.Add(4, (50, 20));

    }
    #endregion
}
