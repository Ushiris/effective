﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MoveState = EnemyState.MoveState;
using Enchants = EnemyState.Enchants;

[RequireComponent(typeof(NavMeshAgent))]
public class FlyEnemyBrain : EnemyBrainBase
{
    [SerializeField] GameObject model;
    SinWaver waver;
    int IcarusZone = 0;
    float defaultY;

    private new void Awake()
    {
        base.Awake();
        waver = SinWaver.Summon(20, 0.5f, gameObject);
    }

    new void Start()
    {
        base.Start();

        Stay = Default__;
        SetRandomAI();

        FindAction = FindAction_Sniper;

        state.moves = new Dictionary<Enchants, EnemyState.EnchantMove> {
            {
                Enchants.Stan,
                ()=>
                {
                    navMesh.SetDestination(player.transform.position);
                }
            },
            {
                Enchants.Blind,()=>Stay()
            },
        };

        defaultY = model.transform.position.y;
    }

    private void Update()
    {
        if (IcarusZone == 0 && model.transform.position.y < defaultY - 0.3f)
        {
            model.transform.position = new Vector3(model.transform.position.x, model.transform.position.y + 0.1f, model.transform.position.z);
        }

        if (IcarusZone >= 1)
        {
            if (model.transform.position.y <= transform.position.y + 0.5) return;

            model.transform.position = new Vector3(model.transform.position.x, model.transform.position.y - 0.1f, model.transform.position.z);
        }
    }

    private void LateUpdate()
    {
        model.transform.position = new Vector3(model.transform.position.x, model.transform.position.y + waver.GetDeltaHeight(), model.transform.position.z);
        if (state.move == MoveState.Confuse)
        {
            return;
        }

        if (FindFlag())
        {
            state.move = MoveState.Chase;
            Think();
        }
        else
        {
            state.move = MoveState.Stay;
        }
    }

    public void Default__()
    {
        if (navMesh.isStopped)
        {
            LookAtPlayerXZ();
        }
    }

    private void FindAction_Sniper()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < EnemyProperty.BestAttackDistance_Range)
        {
            Vector3 run_target = transform.position - (transform.forward * 5);
            transform.LookAt(run_target);
            navMesh.SetDestination(run_target);
        }
        else
        {
            navMesh.SetDestination(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            LookAtPlayerXZ();
        }
    }

    void SetRandomAI()
    {
        var rand = Random.Range(1, 10);
        if (rand == 9)
        {
            AIset(FindAItype.Commander);
        }
        else
        {
            AIset(FindAItype.Soldier);

            if (rand < 3)
            {
                AIset(StayAItype.Ambush);
            }
            else if (rand > 7)
            {
                AIset(StayAItype.Ninja);
            }
            else
            {
                AIset(StayAItype.Return);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Id249_Icarus>() == null) return;
        IcarusZone++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Id249_Icarus>() == null) return;
        IcarusZone--;
    }
}