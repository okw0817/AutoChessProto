using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, IInitializer
{
    #region Members : protected
    [SerializeField]
    protected CharaterState charaterState;
    #endregion

    #region Members : Properties
    public CharaterState CharacterState { get => charaterState; }
    public HeroState max_HeroState;
    public HeroState cur_HeroState;
    #endregion

    #region Methdos : Public
    public virtual void Init()
    {
        max_HeroState.HP = charaterState.MaxHP;
        max_HeroState.MP = charaterState.MaxMP;
        max_HeroState.Defense = charaterState.Defense;
        max_HeroState.MagicDefense = charaterState.MagicDefense;
        max_HeroState.Damage = charaterState.Damage;
        max_HeroState.MagicDamage = charaterState.MagicDamage;
        max_HeroState.AttackRange = charaterState.AttackRange;
        max_HeroState.Critical = charaterState.Critical;
        max_HeroState.gain_Attack_MP = charaterState.Gain_Attack_MP;
        max_HeroState.gain_Defense_MP = charaterState.Gain_Defense_MP;
    }

    public void InitializeState()
    {
        cur_HeroState = max_HeroState;
    }
    #endregion
}
