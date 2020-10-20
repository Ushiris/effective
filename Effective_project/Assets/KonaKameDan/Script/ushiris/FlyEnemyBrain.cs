using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MoveState = EnemyState.MoveState;
using Enchants = EnemyState.Enchants;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public class FlyEnemyBrain : EnemyBrainBase
{
    void Start()
    {
        base.Start();

        Default = Default__;
        FindAction = FindAction__;

        state.moves = new Dictionary<Enchants, EnemyState.EnchantMove> {
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

    private void LateUpdate()
    {
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
            navMesh.SetDestination(new Vector3(
                            UnityEngine.Random.Range(transform.position.x - 10, transform.position.x + 10),
                            transform.position.y,
                            UnityEngine.Random.Range(transform.position.z - 10, transform.position.z + 10)
                            ));
        }
    }

    public void FindAction__()
    {
        var pos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        navMesh.SetDestination(pos);
    }
}