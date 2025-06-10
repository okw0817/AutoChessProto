using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SynergyClothArmor", menuName = "Scriptable Object/SynergyClothArmor", order = int.MaxValue)]

public class SynergyClothArmor : SynergyObjectable
{
    #region Members : Private
    private Dictionary<int, (int, int)> dic_Level;
    #endregion

    #region Methods : Override
    public override void ActiveSynergy(int level, Hero hero)
    {
        hero.max_HeroState.Defense = hero.CharacterState.Defense + dic_Level[level].Item1;
        hero.max_HeroState.HP = hero.CharacterState.MaxHP + dic_Level[level].Item2;
    }

    public override void Init()
    {
        dic_Level = new Dictionary<int, (int, int)>();
        dic_Level.Add(0, (0,0));
        dic_Level.Add(1, (5,50));
        dic_Level.Add(2, (10,75));
        dic_Level.Add(3, (15,100));
        dic_Level.Add(4, (20,150));

    }
    #endregion
}
