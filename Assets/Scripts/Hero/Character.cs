using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, IInitializer
{
    #region Members : protected
    [SerializeField]
    protected CharaterState charaterState;

    protected int curHP;
    protected int curMP;
    protected int curDefense;
    protected int curDamage;
    protected int curAttackRange;
    protected int curCritical;
    protected int cur_gain_Attack_MP;
    protected int cur_gain_Defense_MP;
    #endregion

    #region Methdos : Public
    public virtual void Init()
    {
        curHP = charaterState.MaxHP;
        curMP = charaterState.MaxMP;
        curDefense = charaterState.Defense;
        curDamage = charaterState.Damage;
        curAttackRange = charaterState.AttackRange;
        curCritical = charaterState.Critical;
        cur_gain_Attack_MP = charaterState.MaxHP;
        cur_gain_Defense_MP = charaterState.Gain_Defense_MP;
    }
    #endregion
}
