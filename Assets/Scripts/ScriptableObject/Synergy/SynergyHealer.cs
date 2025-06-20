using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SynergyHealer", menuName = "Scriptable Object/SynergyHealer", order = int.MaxValue)]

public class SynergyHealer : SynergyObjectable
{
    #region Members : Private
    private Dictionary<int, int> dic_Level;
    #endregion

    #region Methods : Override
    public override void ActiveSynergy(int level, Hero hero)
    {
        hero.max_HeroState.MP = hero.CharacterState.MaxMP - dic_Level[level];
    }

    public override void Init()
    {
        if (dic_Level != null)
            return;

        dic_Level = new Dictionary<int, int>();
        dic_Level.Add(0, 0);
        dic_Level.Add(1, 15);
        dic_Level.Add(2, 20);
        dic_Level.Add(3, 30);
        dic_Level.Add(4, 50);

        synergyName = CharacterSynergy.Healer.ToString();
    }
    #endregion
}