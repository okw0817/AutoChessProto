using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Object/CharacterData", order = int.MaxValue)]
public class CharaterState : ScriptableObject
{
    [SerializeField]
    private int maxHP;
    public int MaxHP { get { return maxHP; } }

    [SerializeField]
    private int maxMP;
    public int MaxMP { get { return maxMP; } }

    [SerializeField]
    private int defense;
    public int Defense { get { return defense; } }

    [SerializeField]
    private int magicDefense;
    public int MagicDefense { get { return magicDefense; } }

    [SerializeField]
    private int damage;
    public int Damage { get { return damage; } }

    [SerializeField]
    private int attackRange;
    public int AttackRange { get { return attackRange; } }

    [SerializeField]
    private int critical;
    public int Critical { get { return critical; } }

    [SerializeField]
    private int gain_Attack_MP;
    public int Gain_Attack_MP { get { return gain_Attack_MP; } }


    [SerializeField]
    private int gain_Defense_MP;
    public int Gain_Defense_MP { get { return gain_Defense_MP; } }

    [SerializeField]
    private List<CharacterSynergy> synergies;
    public IEnumerator<CharacterSynergy> Synergies { get { return synergies.GetEnumerator(); } }
}