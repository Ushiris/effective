using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MoveState = EnemyState.MoveState;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyState))]
public class EnemyBrain : MonoBehaviour, EnemyBrainBase
{
    GameObject target;
    NavMeshAgent navMesh;
    public EnemyState state;
    StopWatch timer;

    void Start()
    {
        state = GetComponent<EnemyState>();
        timer = gameObject.AddComponent<StopWatch>();
        navMesh = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player");
        navMesh.SetDestination(target.transform.position);
        timer.LapTime = 0.5f;
        timer.LapEvent = Think;
    }

    private void LateUpdate()
    {
        if (state.move == MoveState.Stan)
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
                navMesh.SetDestination(new Vector3(
                        Random.Range(transform.position.x - 10, transform.position.x + 10),
                        transform.position.y,
                        Random.Range(transform.position.z - 10, transform.position.z + 10)
                        ));
                break;

            case MoveState.Stan:
                navMesh.SetDestination(transform.position);
                break;

            default:
                Debug.Log(gameObject.name + "「こういう時(" + state.ToString() + ")にどうすればいいのかわからん」");
                break;
        }
    }

    public void Stan(float time)
    {
        StopWatch.Summon(time, () => { state.move = MoveState.Stay; }, gameObject, true);
    }
}
