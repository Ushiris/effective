using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    GameObject target;
    NavMeshAgent navMesh;

    void Start()
    {
        target = GameObject.FindWithTag("Player");
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.SetDestination(target.transform.position);
    }

    private void LateUpdate()
    {
        navMesh.SetDestination(target.transform.position);
    }
}
