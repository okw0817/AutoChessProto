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

}
