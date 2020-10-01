using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MoveState = EnemyState.MoveState;

[RequireComponent(typeof(NavMeshAgent))]
public class FlyEnemyBrain : MonoBehaviour, EnemyBrainBase
{
    GameObject target;
    NavMeshAgent navMesh;
    public EnemyState state;
    StopWatch timer;

    void Start()
    {
        state = GetComponent<EnemyState>();
        target = GameObject.FindWithTag("Player");
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.SetDestination(target.transform.position);
        timer = gameObject.AddComponent<StopWatch>();
        timer.LapTime = 0.5f;
        timer.LapEvent = Think;
        transform.position = new Vector3(transform.position.x, Random.Range(7, 13), transform.position.z);
    }

    private void LateUpdate()
    {
        if (state.move == MoveState.Stan)
        {
            return;
        }

        if (Vector3.Distance(target.transform.position, transform.position) <= 40)
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
                var pos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
                navMesh.SetDestination(pos);
                break;

            case MoveState.Stay:
                Default();
                break;

            case MoveState.Stan:
                navMesh.SetDestination(transform.position);
                break;

            case MoveState.Blind:
                navMesh.SetDestination(new Vector3(
                            Random.Range(transform.position.x - 10, transform.position.x + 10),
                            transform.position.y,
                            Random.Range(transform.position.z - 10, transform.position.z + 10)
                            ));
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


    public void Blind(float time)
    {
        StopWatch.Summon(time, () => { state.move = MoveState.Stay; }, gameObject, true);
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
}
