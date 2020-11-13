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

    protected static GameObject player;
    protected NavMeshAgent navMesh;
    protected StopWatch thinkTimer;
    protected List<StopWatch> EnchantTimer=new List<StopWatch>();
    protected Vector3 DefaultPos;
    protected Formation formation = new Formation();
    protected Transform HidePos;
    public bool IsCommand { get; protected set; } = false;
    Rigidbody rb;

    public void Awake()
    {
        thinkTimer = gameObject.AddComponent<StopWatch>();
        navMesh = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        EnchantTimer.ForEach((item) => item = gameObject.AddComponent<StopWatch>());
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
                    navMesh.SetDestination(player.transform.position);
                }
            },
            {
                Enchants.Blind,()=>Default()
            },
        };

        rb.velocity = Vector3.zero;
    }

    void InitDefaultAction()
    {
        Think = Think_;
        Stan = Stan_;
        Blind = Blind_;
        Default = Default_;
        IsAttackable = IsAttackable_;
        FindFlag = FindFlag_;
        FindAction = FindAction_Circle;
        EnchantAction = EnchantAction_;
    }

    //オブジェクトがアクティブになった時 
    protected void OnEnable()
    {
        if (navMesh != null) navMesh.updatePosition = true;
        Hide();
    }

    //オブジェクトが非表示になった時 
    void OnDisable()
    {
        if (navMesh != null) navMesh.updatePosition = false;
    }

    public delegate void EnemyBrainAction();
    public delegate bool EnemyBrainFlag();
    public delegate void EnemyStateChange(float time);

    public StopWatch.TimeEvent Think { get => thinkTimer.LapEvent; protected set => thinkTimer.LapEvent = value; }
    public EnemyStateChange Stan { get; protected set; }
    public EnemyStateChange Blind { get; protected set; }
    public EnemyBrainAction Default { get; protected set; }
    public EnemyBrainFlag IsAttackable { get; protected set; }
    public EnemyBrainFlag FindFlag { get; protected set; }
    public EnemyBrainAction FindAction { get; protected set; }
    public EnemyBrainAction EnchantAction { get; protected set; }

    public void AddEnchant(Enchants enchant, float time)
    {
        state.enchants[(int)enchant] = true;
        EnchantTimer[(int)enchant].ResetTimer();
        EnchantTimer[(int)enchant].LapEvent = () => { state.enchants[(int)enchant] = false; EnchantTimer[(int)enchant].SetActive(false); };
        EnchantTimer[(int)enchant].LapTime = time;
    }

    private void Think_()
    {
        switch (state.move)
        {
            case MoveState.Chase:
                transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
                FindAction();
                break;

            case MoveState.Stay:
                Default();
                break;

            case MoveState.Confuse:
                Default();
                break;

            default:
                DebugLogger.Log(gameObject.name + "「こういう時(" + state.ToString() + ")にどうすればいいのかわからん」");
                break;
        }

        EnchantAction();
    }

    private void Default_()
    {
        navMesh.SetDestination(DefaultPos);
    }

    private bool IsAttackable_()
    {
        switch (state.move)
        {
            case MoveState.Chase:
                return true;
            case MoveState.Stay:
                return false;
            case MoveState.Confuse:
                return true;
            case MoveState.STATE_AMOUNT:
                throw new NotImplementedException();
            default:
                throw new NotImplementedException();
        }
    }

    private void Stan_(float time)
    {
        AddEnchant(Enchants.Stan, time);
        Think();
    }

    private void Blind_(float time)
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

        navMesh.SetDestination(player.transform.position + add);
        gameObject.transform.LookAt(player.transform);
    }

    private void EnchantAction_()
    {
        for (int i = 0; i < state.enchants.Count; i++)
        {
            if (state.enchants[i])
            {
                state.moves[(Enchants)i]();
            }
        }
    }

    private bool FindFlag_() 
    {
        return Vector3.Distance(player.transform.position, transform.position) <= EnemyProperty.PlayerFindDistance;
    }

    public void Hide()
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
        FindAction = () => navMesh.SetDestination(boss.position);
        Default = () => navMesh.SetDestination(boss.position);
    }

    public void AIset(StayAItype type)
    {
        switch (type)
        {
            case StayAItype.Ambush:
                Default = () =>
                {
                    navMesh.SetDestination(player.transform.position + (UnityEngine.Random.Range(0, 1) == 0 ? player.transform.right * 30 : -player.transform.right * 30));
                };
                break;

            case StayAItype.Ninja:
                FindAction = () => { };
                Hide();
                Default = () => { Hide(); navMesh.SetDestination(HidePos.position); };
                break;

            case StayAItype.Return:
                Default = Default_;
                break;

            default:
                break;
        }
    }

    public void AIset(FindAItype type)
    {
        switch (type)
        {
            case FindAItype.Soldier:
                IsCommand = false;
                Default = Default_;
                FindAction = FindAction_Circle;
                break;

            case FindAItype.Commander:
                IsCommand = true;
                var enemies = new List<GameObject>();
                foreach (var item in FindObjectsOfType<Enemy>())
                {
                    enemies.Add(item.gameObject);
                }
                RemoveNotActive(enemies);

                if (enemies.Count == 0) break;

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

                for(int i = 0; i < (enemies.Count< 4?enemies.Count:4); i++)
                {
                    enemies[idx[i]].GetComponent<EnemyBrainBase>().Follow(transform);
                }
                break;

            default:
                break;
        }
    }

    protected void RemoveNotActive(List<GameObject> enemies)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            var item = enemies[i];
            if (!item.activeSelf) enemies.Remove(item);
        }
    }
}
