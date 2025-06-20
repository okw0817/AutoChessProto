using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SynergyShielder", menuName = "Scriptable Object/SynergyShielder", order = int.MaxValue)]
public class SynergyShielder : SynergyObjectable
{
    #region Members : Private
    private Dictionary<int, int> dic_Level;
    #endregion

    #region Methods : Override
    public override void ActiveSynergy(int level, Hero hero)
    {
         hero.max_HeroState.Defense = hero.CharacterState.Defense + dic_Level[level];
    }

    public override void Init()
    {
        if (dic_Level != null)
            return;

        dic_Level = new Dictionary<int, int>();
        dic_Level.Add(0, 0);
        dic_Level.Add(1, 5);
        dic_Level.Add(2, 10);
        dic_Level.Add(3, 15);
        dic_Level.Add(4, 30);

        synergyName = CharacterSynergy.Shielder.ToString();

    }
    #endregion
}
