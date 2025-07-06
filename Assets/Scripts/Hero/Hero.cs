using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

[RequireComponent(typeof(Collider))]
public class Hero : Character, IAttack, IAttacked, IMovable, ISkillAttack
{
    #region Members : private
    private HeroData heroData;
    private int[] AmountArr = {1, -1};
    [SerializeField]
    private int curGrade = 1;

    [SerializeField]
    private Team heroTeam;

    [SerializeField]
    private List<SynergyObjectable> synergyData;

    [SerializeField]
    private ColorObjectable colorData;

    private Animator animator;

    [SerializeField]
    private SkinnedMeshRenderer meshRenderer;

    private UI_HeroState ui_state;

    private Tile curTile;
    private Tile nextTile;
    private Hero targetHero;
    private bool isMoving;
    private bool isAttackWating;
    private float moveSpeed = 2.0f;

    private int findCount;
    #endregion

    #region Members : Property

    public bool IsMoving { get => isMoving; }
    public HeroData HeroData { get => heroData; }
    public Tile CurTile { get => curTile; set => curTile = value; }
    public Hero TargetHero { get => targetHero; set => targetHero = value; }

    public UI_HeroState UI_HeroState { get => ui_state; set => ui_state = value; }

    public int CurGrade { 
        get => curGrade;
        set
        {
            curGrade = value;
            SetGradeState(curGrade);
        }
    }

    public Team HeroTeam {
        get => heroTeam; 
        set {
            heroTeam = value;
        } }

    public int FindCount { get => findCount; set => findCount = value; }
    #endregion

    #region Methods : Mono
    private void Awake()
    {
        var box = GetComponent<BoxCollider>();
        if (box == null) gameObject.AddComponent<BoxCollider>();

        Init();
    }
    #endregion

    #region Methods : override
    public override void Init()
    {
        isMoving = false;
        isAttackWating = false;

        animator = GetComponentInChildren<Animator>(true);

        foreach (var synergy in synergyData)
        {
            synergy.Init();
        }

        findCount = 0;
        base.Init();
    }

    public override void InitializeState()
    {
        base.InitializeState();

        ui_state.SetMP(0.0f);
        ui_state.SetHp(100.0f);
    }
    #endregion

    #region Methods : Interface
    public async void Attack(Character target, int damage)
    {
        Vector3 direction = (targetHero.transform.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 360.0f * Time.deltaTime);

        if (isAttackWating)
            return;

        if(cur_HeroState.MP >= max_HeroState.MP)
        {
            isAttackWating = true;

            animator.Play("Skill", -1, 0.0f);
            animator.speed = 2.0f - cur_HeroState.attackSpeed;

            cur_HeroState.MP = 0;
            ui_state.SetMP((float)cur_HeroState.MP);

            var chessMaster = AutoChessMaster.Instance;
            var obj = await chessMaster.GetPrefabInPool("Skill_" + heroData.projectile);
            var skillProjectile = obj.GetComponent<SkillProjectile>();

            skillProjectile.Init();
            skillProjectile.Target = targetHero.gameObject;
            skillProjectile.SetDamage(cur_HeroState.Damage, cur_HeroState.MagicDamage);
            skillProjectile.transform.position = transform.position;
            skillProjectile.transform.LookAt(target.transform, Vector3.up);
            skillProjectile.ProjectileName = heroData.projectile;
            chessMaster.AddProjectile(skillProjectile);
        }else{
            animator.Play("Attack", -1, 0.0f);
            animator.speed = 2.0f - cur_HeroState.attackSpeed;

            isAttackWating = true;
            cur_HeroState.MP += cur_HeroState.gain_Attack_MP;
            ui_state.SetMP((float)cur_HeroState.MP / (float)max_HeroState.MP);

            var chessMaster = AutoChessMaster.Instance;
            var obj = await chessMaster.GetPrefabInPool(heroData.projectile);
            var projectile = obj.GetComponent<Projectile>();
            projectile.Init();
            projectile.Target = targetHero.gameObject;
            projectile.SetDamage(cur_HeroState.Damage, cur_HeroState.MagicDamage);
            projectile.transform.position = transform.position;
            projectile.transform.LookAt(target.transform, Vector3.up);
            projectile.ProjectileName = heroData.projectile;
            chessMaster.AddProjectile(projectile);

            Debug.Log($"Attack : {heroData.name}");
        }

        await UniTask.WaitForSeconds(cur_HeroState.attackSpeed);
        isAttackWating = false;
    }
    public void SkillAttack()
    {
        throw new System.NotImplementedException();
    }

    public void Attacked(int damage)
    {
        if (cur_HeroState.HP <= 0 || damage == 0)
            return;

        cur_HeroState.HP -= damage;
        ui_state.SetHp((float)cur_HeroState.HP / (float)max_HeroState.HP);

        Debug.Log($"{heroData.name}: Attacked {damage}");

        if (cur_HeroState.HP <= 0) Die();
    }

    public void Die()
    {
        if(HeroTeam == Team.Enemy)
            AutoChessMaster.Instance.DeleteHero(this);
        else
        {
            AutoChessMaster.Instance.DieHero(this);
        }
    }

    public Color GetHeroBorderColor(int level)
    {
        return colorData.GetHeroBoderColor(level);
    }
    #endregion

    #region Methods : public
    public void AdjustSynergy(int level)
    {
        foreach (var synergy in synergyData)
        {
            synergy.ActiveSynergy(level, this);
        }
    }

    public SynergyObjectable HasSynergy(string synergyName)
    {
        foreach (var synergy in synergyData)
        {
            if (synergy.IsSame(synergyName))
                return synergy;
        }

        return null;
    }
    public void Move()
    {
        if (targetHero == null || targetHero.IsDie())
            return;

        if(!isMoving && nextTile == null)
        {
            nextTile = GetNextTile();
            if(nextTile != null)
            {
                curTile.StandingHero = null;
                nextTile.StandingHero = this;
            }
        }
        else
        {
            isMoving = MoveToTarget(nextTile);
            if (!isMoving)
            {
                curTile = nextTile;
                nextTile = null;
            }
        }
    }

    public void SetHeroData(HeroData heroData)
    {
        this.heroData = heroData;
    }

    public bool IsDie()
    {
        return cur_HeroState.HP <= 0;
    }


    public bool isArrive()
    {
        if (targetHero == null || CurTile == null)
            return true;

        int horizontalLenth = Mathf.Abs(targetHero.curTile.Index.Item1 - CurTile.Index.Item1);
        int vertiacalLenth = Mathf.Abs(targetHero.curTile.Index.Item2 - CurTile.Index.Item2);

        return (horizontalLenth + vertiacalLenth) <= cur_HeroState.AttackRange;
    }

    public void UpdateUITransform()
    {
        if (ui_state == null)
            return;

        ui_state.FollowHero(transform.position + new Vector3(0, 2f, 0.5f));
    }
    #endregion

    #region Methods : Private
    private Tile GetNextTile()
    {
        var chessMaster = AutoChessMaster.Instance;
        if (curTile.Index.Item2 >= targetHero.CurTile.Index.Item2 - 1 && curTile.Index.Item2 <= targetHero.CurTile.Index.Item2 + 1)
        {
            //horizontal
            if (curTile.Index.Item1 <= targetHero.CurTile.Index.Item1 - 1)
            {
                //return chessMaster.GetTiltePosition((curTile.Index.Item1 + 1, curTile.Index.Item2));
                return CheckTile(curTile.Index, 1, 0);
            }
            else if (curTile.Index.Item1 >= targetHero.CurTile.Index.Item1 + 1)
            {
                return CheckTile(curTile.Index, -1, 0);
            }
        }
        else
        {
            //vertical
            if (curTile.Index.Item2 < targetHero.CurTile.Index.Item2 - 1)
            {
                return CheckTile(curTile.Index, 0, 1);
            }
            else if (curTile.Index.Item2 > targetHero.CurTile.Index.Item2 + 1)
            {
                return CheckTile(curTile.Index, 0, -1);
            }
        }

        return null;
    }

    private Tile CheckTile((int, int) index, int HorizontalAmount, int VerticalAmount)
    {
        var chessMaster = AutoChessMaster.Instance;
        var tile = chessMaster.GetTiltePosition((index.Item1 + HorizontalAmount, index.Item2 + VerticalAmount));
        if (tile.StandingHero != null)
        {
            ++findCount;
            if (VerticalAmount != 0)
            {
                foreach(var amount in AmountArr)
                {
                    tile = chessMaster.GetTiltePosition((index.Item1 + amount, index.Item2));

                    if (tile != null && tile.StandingHero == null)
                        return tile;
                }
            }else if (HorizontalAmount != 0)
            {
                foreach (var amount in AmountArr)
                {
                    tile = chessMaster.GetTiltePosition((index.Item1, index.Item2 + amount));

                    if (tile != null && tile.StandingHero == null)
                        return tile;
                }
            }
        }
        else
            return tile;

        return null;
    }

    private bool MoveToTarget(Tile targetTie)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTie.transform.position, moveSpeed * Time.deltaTime);
        Vector3 direction = (targetTie.transform.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 360.0f * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetTie.transform.position) < 0.1)
            return false;
        else
            return true;
    }

    private void SetGradeState(int grade)
    {
        var mat = new Material(meshRenderer.material);
        mat.color = colorData.GetHeroColor(grade);
        meshRenderer.material = mat;

        float rate = 0.0f;
        switch(grade)
        {
            case 1:
                rate = 1.0f;
                break;
            case 2:
                rate = 1.5f;
                break;
            case 3:
                rate = 2.0f;
                break;
        }

        max_HeroState.HP = (int)(max_HeroState.HP * rate);
        max_HeroState.Defense = (int)(max_HeroState.Defense * rate);
        max_HeroState.MagicDefense = (int)(max_HeroState.MagicDefense * rate);
        max_HeroState.MagicDamage = (int)(max_HeroState.MagicDamage * rate);
    }
    #endregion

}
