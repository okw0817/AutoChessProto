using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SynergyRider", menuName = "Scriptable Object/SynergyRider", order = int.MaxValue)]
public class SynergyRider : SynergyObjectable
{
    #region Members : Private
    private Dictionary<int, (int, int)> dic_Level;
    #endregion

    #region Methods : Override
    public override void ActiveSynergy(int level, Hero hero)
    {
        hero.max_HeroState.MagicDefense = hero.CharacterState.MagicDefense + dic_Level[level].Item1;
        hero.max_HeroState.Defense = hero.CharacterState.Defense + dic_Level[level].Item2;
    }

    public override void Init()
    {
        if (dic_Level != null)
            return;

        dic_Level = new Dictionary<int, (int, int)>();
        dic_Level.Add(0, (0, 0));
        dic_Level.Add(1, (10, 10));
        dic_Level.Add(2, (20, 20));
        dic_Level.Add(3, (30, 30));
        dic_Level.Add(4, (40, 40));

        synergyName = CharacterSynergy.Rider.ToString();

    }
    #endregion
}
