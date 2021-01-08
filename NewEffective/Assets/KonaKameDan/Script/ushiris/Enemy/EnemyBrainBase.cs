using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MoveState = EnemyState.MoveState;
using Enchants = EnemyState.Enchants;
using System;
using System.Linq;

public class EnemyBrainBase : MonoBehaviour
{
    public EnemyState state = new EnemyState();
    public bool IsCommand { get; protected set; } = false;

    protected static GameObject player;
    protected NavMeshAgent navMesh;
    protected StopWatch thinkTimer;
    protected Rigidbody rb;
    protected List<StopWatch> EnchantTimer=new List<StopWatch>();
    protected Vector3 DefaultPos;
    protected Formation formation = new Formation();
    protected Transform HidePos;
    protected Enemy enemyData;
    protected bool IsLockAI = false;
    protected Material mainMaterial;
    StayAItype ai_stay = StayAItype.Return;
    FindAItype ai_find = FindAItype.Soldier;


    #region VariableMoves
    public delegate void EnemyBrainAction();
    public delegate bool EnemyBrainFlag();
    public delegate void EnemyStateChange(float time);

    public StopWatch.TimeEvent Think { get => thinkTimer.LapEvent; protected set => thinkTimer.LapEvent = value; }
    public EnemyStateChange Stan { get; protected set; }
    public EnemyStateChange Blind { get; protected set; }
    EnemyBrainAction stay_;
    public EnemyBrainAction Stay { get { return stay_; } protected set { if (!IsLockAI) stay_ = value; } }
    public EnemyBrainFlag IsAttackable { get; protected set; }
    public EnemyBrainFlag FindFlag { get; protected set; }
    EnemyBrainAction find_;
    public EnemyBrainAction FindAction {  get { return find_; } protected set { if (!IsLockAI) find_ = value; } }
    public EnemyBrainAction EnchantAction { get; protected set; }
    protected EnemyBrainAction ApplyChangeColor = () => { };
    #endregion

    public void Awake()
    {
        enemyData = GetComponent<Enemy>();
        thinkTimer = gameObject.AddComponent<StopWatch>();
        navMesh = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        for(int i = 0; i < (int)Enchants.ENCHANT_AMOUNT; ++i)
        {
            EnchantTimer.Add(gameObject.AddComponent<StopWatch>());
            EnchantTimer[i].IsActive = false;
        }
    }

    protected void Start()
    {
        if (player == null) player = GameObject.FindWithTag("Player");

        DefaultPos = transform.position;
        thinkTimer.LapTime = 0.5f;
        InitDefaultAction();

        StopWatch.Summon(3, () => { _ = UnityEngine.Random.Range(0, 1) == 0 ? formation++ : formation--; }, gameObject);

        formation.value = (Formation.Formation_)UnityEngine.Random.Range(0, (int)Formation.Formation_.FORMATION_COUNT);

        state.moves = new Dictionary<Enchants, EnemyState.EnchantMove>
        {
            {
                Enchants.Stan,
                ()=>
                {
                    if(navMesh.pathStatus != NavMeshPathStatus.PathInvalid)navMesh.SetDestination(transform.position);
                }
            },
            {
                Enchants.Blind,()=>Stay()
            },
        };

        rb.velocity = Vector3.zero;
        transform.localScale *= UnityEngine.Random.Range(0.8f, 1.3f);
    }

    protected void Update()
    {
        if (state.move == MoveState.Confuse)
        {
            return;
        }

        if (FindFlag())
        {
            state.move = MoveState.Chase;
            Think();
            enemyData.MuzzleLookAt(player.transform.position);
        }
        else
        {
            state.move = MoveState.Stay;
        }
    }

    protected void OnEnable()
    {
        if (navMesh != null) navMesh.updatePosition = true;
        HidePositionSet();
    }

    void OnDisable()
    {
        if (navMesh != null) navMesh.updatePosition = false;
    }

    public void AddEnchant(Enchants enchant, float time)
    {
        state.enchants[(int)enchant] = true;
        EnchantTimer[(int)enchant].ResetTimer();
        EnchantTimer[(int)enchant].LapEvent = () => { state.enchants[(int)enchant] = false; EnchantTimer[(int)enchant].SetActive(false); };
        EnchantTimer[(int)enchant].LapTime = time;
        EnchantTimer[(int)enchant].IsActive = true;
    }

    protected void RemoveNotActive(List<GameObject> enemies)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            var item = enemies[i];
            if (!item.activeSelf) enemies.Remove(item);
        }
    }

    protected void RemoveCommand(List<GameObject> enemies)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            var item = enemies[i];
            if (item.GetComponent<EnemyBrainBase>().IsCommand) enemies.Remove(item);
        }
    }

    protected void LookAtPlayerXZ()
    {
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        enemyData.MuzzleLookAt(player.transform.position);
    }

    protected void ColorSet()
    {
        Material material = new Material(mainMaterial);
        var color= EnemyProperty.enemyColors[ai_stay.ToString() + "_" + ai_find.ToString()];
        if (color != Color.clear) material.SetColor(EnemyProperty.EnemyColorID, color);

        mainMaterial = material;
        ApplyChangeColor();
    }

    protected void ColorSet(Color32 color)
    {
        Material material = new Material(mainMaterial);
        if (color != Color.clear) material.SetColor(EnemyProperty.EnemyColorID, color);

        mainMaterial = material;
        ApplyChangeColor();
    }

    #region AIsetting
    void InitDefaultAction()
    {
        Think = StandardThink;
        Stan = StandardStan;
        Blind = StandardBlind;
        Stay = ReturnToHome;
        IsAttackable = StandardIsAttackable;
        FindFlag = StandardFindFlag;
        FindAction = FindAction_Circle;
        EnchantAction = StandardEnchantAction;
    }

    public void LockAI()
    {
        IsLockAI = true;
    }

    public void AIset(StayAItype type)
    {
        if (IsLockAI) return;
        ai_stay = type;

        switch (type)
        {
            case StayAItype.Ambush:
                Stay = () =>
                {
                    if(navMesh.pathStatus != NavMeshPathStatus.PathInvalid)navMesh.SetDestination(player.transform.position + player.transform.right * (UnityEngine.Random.Range(0, 1) == 0 ? 20 : -20));
                };
                break;

            case StayAItype.Ninja:
                FindAction = () => { };
                HidePositionSet();
                Stay = () => { HidePositionSet(); if(navMesh.pathStatus != NavMeshPathStatus.PathInvalid)navMesh.SetDestination(HidePos.position); };
                break;

            case StayAItype.Return:
                Stay = ReturnToHome;
                break;

            default:
                break;
        }

        ColorSet();
    }

    public void AIset(FindAItype type)
    {
        if (IsLockAI) return;
        ai_find = type;

        switch (type)
        {
            case FindAItype.Soldier:
                Stay = ReturnToHome;
                FindAction = FindAction_Circle;
                break;

            case FindAItype.Commander:
                IsCommand = true;
                Invoke("Influence", 0.2f);
                break;

            default:
                break;
        }

        ColorSet();
    }

    public void HidePositionSet()
    {
        var effects=new List<GameObject>();
        foreach (var item in FindObjectsOfType<TreasureBoxPresenter>())
        {
            effects.Add(item.gameObject);
        }
        if (effects.Count == 0)
        {
            HidePos = transform;
            return;
        }
        List<Transform> e_trans = new List<Transform>();
        effects.ForEach(item => e_trans.Add(item.transform));
        HidePos = e_trans[0];

        var k_dist = Vector3.Distance(transform.position, HidePos.position);
        e_trans.ForEach(item =>
        {
            if (k_dist > Vector3.Distance(item.position, HidePos.position))
            {
                HidePos = item;
                k_dist = Vector3.Distance(transform.position, HidePos.position);
            }
        });
    }

    public void Follow(Transform boss)
    {
        if (IsLockAI) return;

        FindAction = () =>
        {
            if (!boss.gameObject.activeSelf) AIset(ai_find);
            if (navMesh.pathStatus != NavMeshPathStatus.PathInvalid) navMesh.SetDestination(boss.position);
        };
        Stay = () =>
        {
            if (!boss.gameObject.activeSelf) AIset(ai_stay);
            if (navMesh.pathStatus != NavMeshPathStatus.PathInvalid) navMesh.SetDestination(boss.position);
        };

        ColorSet(EnemyProperty.enemyColors["Follower"]);
    }

    void Influence()
    {
        var enemies = new List<GameObject>();
        foreach (var item in FindObjectsOfType<Enemy>())
        {
            enemies.Add(item.gameObject);
        }
        RemoveNotActive(enemies);
        RemoveCommand(enemies);

        if (enemies.Count == 0) return;

        var tr = new List<Transform>();
        enemies.ForEach(item => tr.Add(item.transform));

        List<float> dist = new List<float> { Vector3.Distance(transform.position, tr[0].position) };
        List<int> idx = new List<int>() { 0 };

        for (int j = 0; j < tr.Count; j++)
        {
            for (int i = 0; i < dist.Count; i++)
            {
                var dist_x = Vector3.Distance(transform.position, tr[j].position);
                if (dist[i] > dist_x || i == dist.Count - 1)
                {
                    dist.Insert(i, dist_x);
                    idx.Insert(i, j);
                    break;
                }
            }
        }

        for (int i = 0; i < (enemies.Count < 4 ? enemies.Count : 4); i++)
        {
            enemies[idx[i]].GetComponent<EnemyBrainBase>().Follow(transform);
        }
    }

    #endregion

    #region Standard
    private void StandardThink()
    {
        switch (state.move)
        {
            case MoveState.Chase:
                LookAtPlayerXZ();
                FindAction();
                break;

            case MoveState.Stay:
                Stay();
                break;

            case MoveState.Confuse:
                Stay();
                break;

            default:
                DebugLogger.Log(gameObject.name + "「こういう時(" + state.ToString() + ")にどうすればいいのかわからん」");
                break;
        }

        EnchantAction();
    }

    private void ReturnToHome()
    {
        if(navMesh.pathStatus != NavMeshPathStatus.PathInvalid)navMesh.SetDestination(DefaultPos);
    }

    private bool StandardIsAttackable()
    {
        bool isEnchant = false;
        state.enchants.ForEach(item =>
        {
            if (item == true) isEnchant = true;
        });
        if (isEnchant) return false;

        switch (state.move)
        {
            case MoveState.Chase:
                return true;
            case MoveState.Stay:
                return false;
            case MoveState.Confuse:
                return true;
            default:
                throw new NotImplementedException();
        }
    }

    private void StandardStan(float time)
    {
        AddEnchant(Enchants.Stan, time);
        Think();
    }

    private void StandardBlind(float time)
    {
        AddEnchant(Enchants.Blind, time);
        Think();
    }

    public void FindAction_Circle()
    {
        Vector3 add = Vector3.zero;

        switch (formation.value)
        {
            case Formation.Formation_.forward:
                add += Vector3.forward * EnemyProperty.BestAttackDistance_Melee;
                break;
            case Formation.Formation_.forward_right:
                add += (Vector3.forward + Vector3.right) * (EnemyProperty.BestAttackDistance_Melee * 0.7f);
                break;
            case Formation.Formation_.right:
                add += Vector3.right * EnemyProperty.BestAttackDistance_Melee;
                break;
            case Formation.Formation_.behind_right:
                add += (Vector3.back + Vector3.right) * (EnemyProperty.BestAttackDistance_Melee * 0.7f);
                break;
            case Formation.Formation_.behind:
                add += Vector3.back*EnemyProperty.BestAttackDistance_Melee;
                break;
            case Formation.Formation_.behind_left:
                add += (Vector3.back + Vector3.left) * (EnemyProperty.BestAttackDistance_Melee * 0.7f);
                break;
            case Formation.Formation_.left:
                add += Vector3.left*EnemyProperty.BestAttackDistance_Melee;
                break;
            case Formation.Formation_.forward_left:
                add += (Vector3.forward + Vector3.right) * (EnemyProperty.BestAttackDistance_Melee * 0.7f);
                break;
            default:
                break;
        }

        if(navMesh.pathStatus != NavMeshPathStatus.PathInvalid)navMesh.SetDestination(player.transform.position + add);
        LookAtPlayerXZ();
    }

    private void StandardEnchantAction()
    {
        for (int i = 0; i < state.enchants.Count; i++)
        {
            if (state.enchants[i])
            {
                state.moves[(Enchants)i]();
            }
        }
    }

    private bool StandardFindFlag() 
    {
        return Vector3.Distance(player.transform.position, transform.position) <= EnemyProperty.PlayerFindDistance;
    }
    #endregion
}
