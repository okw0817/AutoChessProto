
public class Hero : Character, IAttack, IAttacked, IMovable
{
    #region Members : private

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
        curHP -= damage;

        if (curHP <= 0) Die();
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        throw new System.NotImplementedException();
    }

    #endregion

}
