using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KameMotion : MonoBehaviour
{
    [SerializeField] Animator animator;
    bool isMove = false;
    NavMeshAgent agent;

    private void Start()
    {
        agent = gameObject.GetComponentInParent<NavMeshAgent>();
    }

    private void Update()
    {
        isMove = !agent.isStopped;
    }
}
