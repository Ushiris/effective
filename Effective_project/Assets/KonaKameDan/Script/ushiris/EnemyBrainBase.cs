using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MoveState = EnemyState.MoveState;
using Enchants = EnemyState.Enchants;
using System;

public class EnemyBrainBase : MonoBehaviour
{
    public EnemyState state;

    protected GameObject target;
    protected NavMeshAgent navMesh;
    protected StopWatch timer;
    protected List<StopWatch> EnchantTimer = new List<StopWatch>((int)Enchants.ENCHANT_AMOUNT);

    private void Awake()
    {
        timer = gameObject.AddComponent<StopWatch>();
        navMesh = GetComponent<NavMeshAgent>();
        state = GetComponent<EnemyState>();
    }

    protected void Start()
    {
        target = GameObject.FindWithTag("Player");

        EnchantTimer.ForEach((item) => item = gameObject.AddComponent<StopWatch>());
        timer.LapTime = 0.5f;
        InitDefaultAction();
        timer.LapEvent = Think;


        state.moves = new Dictionary<Enchants, EnemyState.EnchantMove>
        {
            {
                Enchants.Stan,
                ()=>
                {
                    navMesh.SetDestination(target.transform.position);
                }
            },
            {
                Enchants.Blind,()=>Default()
            },
        };
    }

    void InitDefaultAction()
    {
        Think = Think_;
        Stan = Stan_;
        Blind = Blind_;
        Default = Default_;
        IsAttackable = IsAttackable_;
        FindFlag = FindFlag_;
        FindAction = FindAction_;
        EnchantAction = EnchantAction_;
    }

    //オブジェクトがアクティブになった時 
    void OnEnable()
    {
        if (navMesh != null) navMesh.updatePosition = true;
    }

    //オブジェクトが非表示になった時 
    void OnDisable()
    {
        if (navMesh != null) navMesh.updatePosition = false;
    }

    public delegate void EnemyBrainAction();
    public delegate bool EnemyBrainFlag();
    public delegate void EnemyStateChange(float time);

    public StopWatch.TimeEvent Think { get; protected set; }
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

    public void Default_()
    {
        if (navMesh.isStopped)
        {
            navMesh.SetDestination(new Vector3(
                            UnityEngine.Random.Range(transform.position.x - 10, transform.position.x + 10),
                            transform.position.y,
                            UnityEngine.Random.Range(transform.position.z - 10, transform.position.z + 10)
                            ));
        }
    }

    public bool IsAttackable_()
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

    public void Stan_(float time)
    {
        AddEnchant(Enchants.Stan, time);
        Think();
    }

    public void Blind_(float time)
    {
        AddEnchant(Enchants.Blind, time);
        Think();
    }

    public void FindAction_()
    {
        navMesh.SetDestination(target.transform.position);
    }

    public void EnchantAction_()
    {
        for (int i = 0; i < state.enchants.Count; i++)
        {
            if (state.enchants[i])
            {
                state.moves[(Enchants)i]();
            }
        }
    }

    public bool FindFlag_() { return false; }
}
