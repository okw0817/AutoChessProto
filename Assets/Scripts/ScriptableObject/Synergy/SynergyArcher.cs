using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SynergyArcher", menuName = "Scriptable Object/SynergyArcher", order = int.MaxValue)]
public class SynergyArcher : SynergyObjectable
{
    #region Members : Private
    private Dictionary<int, (int, int)> dic_Level;
    #endregion

    #region Methods : Public
    public override void ActiveSynergy(int level, Hero hero)
    {
        hero.max_HeroState.AttackRange = hero.CharacterState.AttackRange + dic_Level[level].Item1;
        hero.max_HeroState.Damage = hero.CharacterState.AttackRange + dic_Level[level].Item2;
    }

    public override void Init()
    {
        dic_Level = new Dictionary<int, (int,int)>();
        dic_Level.Add(0, (0,0));
        dic_Level.Add(1, (1,5));
        dic_Level.Add(2, (1,10));
        dic_Level.Add(3, (2,15));
        dic_Level.Add(4, (2,20));
    }
    #endregion
}
