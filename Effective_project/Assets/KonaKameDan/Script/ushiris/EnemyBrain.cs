using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MoveState = EnemyState.MoveState;
using Enchants = EnemyState.Enchants;
using System;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyState))]
public class EnemyBrain : MonoBehaviour, IEnemyBrainBase
{
    GameObject target;
    NavMeshAgent navMesh;
    public EnemyState state;
    StopWatch timer;
    List<StopWatch> EnchantTimer = new List<StopWatch>((int)Enchants.ENCHANT_AMOUNT);

    void Start()
    {
        timer = gameObject.AddComponent<StopWatch>();
        navMesh = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player");
        state = GetComponent<EnemyState>();

        EnchantTimer.ForEach((item) => item = gameObject.AddComponent<StopWatch>());
        navMesh.SetDestination(target.transform.position);
        timer.LapTime = 0.5f;
        timer.LapEvent = Think;

        state.moves = new Dictionary<Enchants, EnemyState.EnchantMove> {
            {
                Enchants.Stan,
                ()=>
                {
                    navMesh.SetDestination(target.transform.position);
                }
            },
            {
                Enchants.Blind,Default
            },
        };
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
            Think();
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

            case MoveState.Confuse:
                Default();
                break;

            default:
                Debug.Log(gameObject.name + "「こういう時(" + state.ToString() + ")にどうすればいいのかわからん」");
                break;
        }

        for(int i = 0; i < state.enchants.Count; i++)
        {
            if (state.enchants[i])
            {
                state.moves[(Enchants)i]();
            }
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
        Think();
    }

    public void Blind(float time)
    {
        AddEnchant(Enchants.Blind, time);
        Think();
    }

    public void Default()
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

    public bool IsAttackable()
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
}
