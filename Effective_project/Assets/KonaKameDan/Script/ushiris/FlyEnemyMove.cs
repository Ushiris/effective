using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MoveState = EnemyState.MoveState;

[RequireComponent(typeof(NavMeshAgent))]
public class FlyEnemyBrain : MonoBehaviour
{
    GameObject target;
    NavMeshAgent navMesh;
    public MoveState state;
    StopWatch timer;

    void Start()
    {
        target = GameObject.FindWithTag("Player");
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.SetDestination(target.transform.position);
        timer = gameObject.AddComponent<StopWatch>();
        timer.LapTime = 0.5f;
        timer.LapEvent = EnemyBrain();

    }

    private void LateUpdate()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= 40)
        {
            state = MoveState.Chase;
        }
        else
        {
            state = MoveState.Stay;
        }
    }

    StopWatch.TimeEvent EnemyBrain()
    {
        switch (state)
        {
            case MoveState.Chase:
                return () =>
                {
                    var pos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
                    navMesh.SetDestination(pos);
                };

            case MoveState.Stay:
                return () =>
                {
                    navMesh.SetDestination(new Vector3(
                            Random.Range(transform.position.x - 10, transform.position.x + 10),
                            transform.position.y,
                            Random.Range(transform.position.z - 10, transform.position.z + 10)
                            ));
                };

            default:
                return () =>
                {
                    Debug.Log(gameObject.name + "「こういう時(" + state.ToString() + ")にどうすればいいのかわからん」");
                };
        }
    }
}
