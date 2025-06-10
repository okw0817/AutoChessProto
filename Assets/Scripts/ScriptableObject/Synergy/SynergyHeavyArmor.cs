using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SynergyHeavyArmor", menuName = "Scriptable Object/SynergyHeavyArmor", order = int.MaxValue)]

public class SynergyHeavyArmor : SynergyObjectable
{
    #region Members : Private
    private Dictionary<int, (int, int)> dic_Level;
    #endregion

    #region Methods : Override
    public override void ActiveSynergy(int level, Hero hero)
    {
        hero.max_HeroState.Damage = hero.CharacterState.Defense + dic_Level[level].Item1;
        hero.max_HeroState.Defense = hero.CharacterState.MaxHP + dic_Level[level].Item2;
    }

    public override void Init()
    {
        dic_Level = new Dictionary<int, (int, int)>();
        dic_Level.Add(0, (0, 0)); 
        dic_Level.Add(1, (5, 5));
        dic_Level.Add(2, (10, 10));
        dic_Level.Add(3, (15, 15));
        dic_Level.Add(4, (20, 20));

    }
    #endregion
}
