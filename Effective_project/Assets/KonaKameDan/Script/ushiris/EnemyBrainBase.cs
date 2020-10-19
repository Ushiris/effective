using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MoveState = EnemyState.MoveState;
using Enchants = EnemyState.Enchants;
using System;

public class EnemyBrainBase : MonoBehaviour
{
    public class Formation
    {
        public enum Formation_:int
        {
            forward,
            forward_right,
            right,
            behind_right,
            behind,
            behind_left,
            left,
            forward_left,
            FORMATION_COUNT
        }
        public Formation_ value;

        public static Formation operator ++(Formation value)
        {
            switch (value.value)
            {
                case Formation_.forward:
                    value.value = Formation_.forward_right;
                    break;
                case Formation_.forward_right:
                    value.value = Formation_.right;
                    break;
                case Formation_.right:
                    value.value = Formation_.behind_right;
                    break;
                case Formation_.behind_right:
                    value.value = Formation_.behind;
                    break;
                case Formation_.behind:
                    value.value = Formation_.behind_left;
                    break;
                case Formation_.behind_left:
                    value.value = Formation_.left;
                    break;
                case Formation_.left:
                    value.value = Formation_.forward_left;
                    break;
                case Formation_.forward_left:
                    value.value = Formation_.forward;
                    break;
                default:
                    DebugLogger.Log("Enum error!");
                    break;
            }

            return value;
        }

        public static Formation operator --(Formation value)
        {
            switch (value.value)
            {
                case Formation_.forward:
                    value.value = Formation_.forward_left;
                    break;
                case Formation_.forward_right:
                    value.value = Formation_.forward;
                    break;
                case Formation_.right:
                    value.value = Formation_.forward_right;
                    break;
                case Formation_.behind_right:
                    value.value = Formation_.right;
                    break;
                case Formation_.behind:
                    value.value = Formation_.behind_right;
                    break;
                case Formation_.behind_left:
                    value.value = Formation_.behind;
                    break;
                case Formation_.left:
                    value.value = Formation_.behind_left;
                    break;
                case Formation_.forward_left:
                    value.value = Formation_.left;
                    break;
                default:
                    DebugLogger.Log("Enum error!");
                    break;
            }

            return value;
        }
    }
    public EnemyState state;

    protected GameObject target;
    protected NavMeshAgent navMesh;
    protected StopWatch timer;
    protected List<StopWatch> EnchantTimer = new List<StopWatch>((int)Enchants.ENCHANT_AMOUNT);
    protected Vector3 DefaultPos;
    protected Formation formation = new Formation();
    protected float distance = 5;

    private void Awake()
    {
        timer = gameObject.AddComponent<StopWatch>();
        navMesh = GetComponent<NavMeshAgent>();
        state = GetComponent<EnemyState>();
    }

    protected void Start()
    {
        target = GameObject.FindWithTag("Player");
        DefaultPos = transform.position;
        EnchantTimer.ForEach((item) => item = gameObject.AddComponent<StopWatch>());
        timer.LapTime = 0.5f;
        InitDefaultAction();

        StopWatch.Summon(3, () => { _ = UnityEngine.Random.Range(0, 1) == 0 ? formation++ : formation--; }, gameObject);

        formation.value = (Formation.Formation_)UnityEngine.Random.Range(0, (int)Formation.Formation_.FORMATION_COUNT);

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
        FindAction = FindAction_Circle;
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

    public StopWatch.TimeEvent Think { get => timer.LapEvent; protected set => timer.LapEvent = value; }
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

    private void FindAction_Circle()
    {
        Vector3 add = Vector3.zero;

        switch (formation.value)
        {
            case Formation.Formation_.forward:
                add += Vector3.forward * distance;
                break;
            case Formation.Formation_.forward_right:
                add += (Vector3.forward + Vector3.right) * (distance * 0.7f);
                break;
            case Formation.Formation_.right:
                add += Vector3.right * distance;
                break;
            case Formation.Formation_.behind_right:
                add += (Vector3.back + Vector3.right) * (distance * 0.7f);
                break;
            case Formation.Formation_.behind:
                add += Vector3.back*distance;
                break;
            case Formation.Formation_.behind_left:
                add += (Vector3.back + Vector3.left) * (distance * 0.7f);
                break;
            case Formation.Formation_.left:
                add += Vector3.left*distance;
                break;
            case Formation.Formation_.forward_left:
                add += (Vector3.forward + Vector3.right) * (distance * 0.7f);
                break;
            default:
                break;
        }

        navMesh.SetDestination(target.transform.position + add);
        gameObject.transform.LookAt(target.transform);
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
        return Vector3.Distance(target.transform.position, transform.position) <= 30;
    }
}
