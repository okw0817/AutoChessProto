using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class Hero : Character, IAttack, IAttacked, IMovable
{
    #region Members : private
    private HeroData heroData;

    [SerializeField]
    private List<SynergyObjectable> synergyData;
    #endregion

    #region Members : Property
    public HeroData HeroData { get => heroData; }
    #endregion

    #region Methods : Mono
    private void Awake()
    {
        var box = GetComponent<BoxCollider>();
        if (box == null) gameObject.AddComponent<BoxCollider>();
        foreach(var synergy in synergyData)
        {
            synergy.Init();
        }
    }
    #endregion

    #region Methods : override
    public override void Init()
    {
        base.Init();
    }
    #endregion

    #region Methods : Interface
    public void Attack(Character target, int damage)
    {
        if(target is IAttacked)
        {
            (target as IAttacked).Attacked(damage);
        }
    }

    public void Attacked(int damage)
    {
        cur_HeroState.HP -= damage;

        if (cur_HeroState.HP <= 0) Die();
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"{name}");
    }

    public void SetHeroData(HeroData heroData)
    {
        this.heroData = heroData;
    }

    #endregion

    #region Methods : public
    public void AdjustSynergy(int level)
    {
        foreach(var synergy in synergyData)
        {
            synergy.ActiveSynergy(level, this);
        }
    }

    public SynergyObjectable HasSynergy(string synergyName)
    {
        foreach(var synergy in synergyData)
        {
            if (synergy.IsSame(synergyName))
                return synergy;
        }

        return null;
    }

    #endregion

}
