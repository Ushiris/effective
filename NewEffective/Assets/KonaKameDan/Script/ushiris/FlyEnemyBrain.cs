using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MoveState = EnemyState.MoveState;
using Enchants = EnemyState.Enchants;

[RequireComponent(typeof(NavMeshAgent))]
public class FlyEnemyBrain : EnemyBrainBase
{
    EnemyArtsInstant bag;
    private new void Awake()
    {
        base.Awake();
        bag = gameObject.GetComponent<EnemyArtsInstant>();
    }

    new void Start()
    {
        base.Start();
        
        Default = Default__;
        var rand = Random.Range(1, 10);
        if (rand == 10)
        {
            AIset(FindAItype.Commander);
        }
        else
        {
            AIset(FindAItype.Soldier);

            if (rand < 4)
            {
                AIset(StayAItype.Ambush);
            }
            else if (rand > 8)
            {
                AIset(StayAItype.Ninja);
            }
            else
            {
                AIset(StayAItype.Return);
            }
        }

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
                Enchants.Blind,()=>Default()
            },
        };

        bag.ChangeAction(() => bag.OnSelfIdSetAction(Random.Range(0, 2) == 0 ? "04" : "045"));
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
                            Random.Range(transform.position.x - 10, transform.position.x + 10),
                            transform.position.y,
                            Random.Range(transform.position.z - 10, transform.position.z + 10)
                            ));
        }
    }

    private void FindAction_Sniper()
    {
        var pos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        var dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist < EnemyProperty.BestAttackDistance_Range)
        {
            transform.LookAt(pos);
            navMesh.SetDestination(transform.position - (transform.forward * 5));
        }
        else
        {
            navMesh.SetDestination(pos);
        }
    }
}