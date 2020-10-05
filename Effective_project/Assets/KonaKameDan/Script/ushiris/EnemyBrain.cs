using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MoveState = EnemyState.MoveState;
using Enchants = EnemyState.Enchants;
using System.Security.Cryptography;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyState))]
public class EnemyBrain : MonoBehaviour, IEnemyBrainBase
{
    GameObject target;
    NavMeshAgent navMesh;
    public EnemyState state;
    StopWatch timer;
    List<StopWatch> EnchantTimer = new List<StopWatch>((int)Enchants.ENCHANT_AMOUNT);
    StopWatch ConfuseTimer;


    private void Awake()
    {
        state.moves = new Dictionary<Enchants, EnemyState.EnchantMove> {
            {
                Enchants.Stan,
                ()=>
                {
                    navMesh.SetDestination(target.transform.position);
                }
            },
            {
                Enchants.Blind,
                () =>
                {

                }
            },
        };
    }

    void Start()
    {
        state = GetComponent<EnemyState>();
        timer = gameObject.AddComponent<StopWatch>();
        navMesh = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player");
        EnchantTimer.ForEach((item) => item = gameObject.AddComponent<StopWatch>());
        ConfuseTimer = gameObject.AddComponent<StopWatch>();
        navMesh.SetDestination(target.transform.position);
        timer.LapTime = 0.5f;
        timer.LapEvent = Think;
    }

    private void LateUpdate()
    {
        if (state.move == MoveState.Confuse)
        {
            return;
        }

        if (Vector3.Distance(target.transform.position, transform.position) <= 30)
        {
            state.move = MoveState.Chase;
        }
        else
        {
            state.move = MoveState.Stay;
        }
    }

    public void Think()
    {
        switch (state.move)
        {
            case MoveState.Chase:
                navMesh.SetDestination(target.transform.position);
                break;

            case MoveState.Stay:
                Default();
                break;

            default:
                Debug.Log(gameObject.name + "「こういう時(" + state.ToString() + ")にどうすればいいのかわからん」");
                break;
        }
    }

    public void AddEnchant(Enchants enchant,float time)
    {
        state.enchants[(int)enchant] = true;
        EnchantTimer[(int)enchant].ResetTimer();
        EnchantTimer[(int)enchant].LapEvent = () => { state.enchants[(int)enchant] = false; EnchantTimer[(int)enchant].SetActive(false); };
        EnchantTimer[(int)enchant].LapTime = time;
    }

    public void Stan(float time)
    {
        AddEnchant(Enchants.Stan,time);
    }

    public void Blind(float time)
    {
        AddEnchant(Enchants.Blind, time);
    }

    public void Default()
    {
        if (navMesh.isStopped)
        {
            navMesh.SetDestination(new Vector3(
                            Random.Range(transform.position.x - 10, transform.position.x + 10),
                            transform.position.y,
                            Random.Range(transform.position.z - 10, transform.position.z + 10)
                            ));
        }
    }

    public bool IsAttackable()
    {
        return state.move switch
        {
            MoveState.Chase => true,
            MoveState.Stay => false,
            MoveState.Confuse => true,
            _ => false,
        };
    }
}
