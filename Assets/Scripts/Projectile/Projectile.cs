using UnityEngine;

public class Projectile : MonoBehaviour, IInitializer, IMovable
{
    #region Members : Private
    private string projectileName;

    [SerializeField]
    private AudioSource audioSource;

    private float moveSpeed = 3.0f;
    private float rotateSpeed = 360.0f;

    private GameObject target;
    private float hitDistance = 0.5f;
    private (int, int) damage;
    #endregion

    #region Members : Properties
    public GameObject Target { get=> target; set=> target = value; }
    public string ProjectileName { get=> projectileName; set=> projectileName = value; }
    #endregion

    #region Methods : Publics
    public async void ActiveEffect(bool value)
    {
        if (value)
        {
            //AutoChessMaster.Instance.GetPrefabInPool();
            //effect.Play();
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
        else
        {

        }
        //effect.Stop();

    }

    public async void ArriveToTarget()
    {
        //ResourceManager.Instance.GetEffectName()
        var obj = await AutoChessMaster.Instance.GetPrefabInPool(name);
    }
    #endregion

    #region Methods : Interface
    public void Init()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void Move()
    {
        if (target == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.deltaTime);

        if(Vector3.Distance(target.transform.position, transform.position) < hitDistance)
        {
            if(target.TryGetComponent<Hero>(out Hero hero))
            {
                Hit(hero, damage);
            }
        }
    }

    public void SetDamage(int damage, int magicDamage)
    {
        this.damage.Item1 = damage;
        this.damage.Item2 = magicDamage;
    }

    public async void Hit(Hero target, (int, int) damage)
    {
        target.Attacked(damage.Item1);
        target.Attacked(damage.Item2);

        AutoChessMaster.Instance.DeleteProjectile(this);
        AutoChessMaster.Instance.PushPrefabPool(projectileName, this.gameObject);

        string effectName = ResourceManager.Instance.GetEffectName(ProjectileName);
        var effect = await AutoChessMaster.Instance.GetPrefabInPool(effectName);
        effect.transform.position = target.transform.position;
        effect.GetComponent<EffectBase>().PlayParticle();
        this.target = null;
    }
    #endregion
}
