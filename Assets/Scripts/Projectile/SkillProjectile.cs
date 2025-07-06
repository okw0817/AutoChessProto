using UnityEngine;

public class SkillProjectile : Projectile
{
    #region Members : Private
    [SerializeField]
    private int rowRange;

    [SerializeField]
    private int colRange;

    [SerializeField]
    private float damageMultiply;

    [SerializeField]
    private float m_DamageMultiply;
    #endregion

    #region Methods : Public
    public override async void Hit(Hero target, (int, int) damage)
    {
        AutoChessMaster.Instance.DeleteProjectile(this);
        AutoChessMaster.Instance.PushPrefabPool(projectileName, this.gameObject);
        string effectName = ResourceManager.Instance.GetEffectName(ProjectileName);

        var tiles = GetTilesInRage(target, rowRange, colRange);

        if (tiles == null)
            return;

        foreach(var enemyTile in tiles)
        {
            if (enemyTile == null)
                continue;

            var effect = await AutoChessMaster.Instance.GetPrefabInPool(effectName);
            effect.transform.position = enemyTile.transform.position;
            effect.transform.rotation = Quaternion.RotateTowards(effect.transform.rotation, target.transform.rotation, 360.0f);
            if (effect.TryGetComponent<EffectBase>(out EffectBase effectComp))
            {
                if (string.IsNullOrEmpty(effectComp.EffectName))
                    effectComp.EffectName = effectName;


                effectComp.PlayParticle();
            }

            if (enemyTile.StandingHero == null)
                continue;

            target.Attacked((int)(damage.Item1 * damageMultiply));
            target.Attacked((int)(damage.Item2 * m_DamageMultiply));
        }

        this.target = null;
    }
    #endregion

    #region Methods : Private
    private Tile[] GetTilesInRage(Hero target, int rowAmount, int colAmount)
    {
        if (target.CurTile == null)
            return null;

        return AutoChessMaster.Instance.GetTilesInRange(target.CurTile, rowAmount, colAmount);
    }
    #endregion
}
