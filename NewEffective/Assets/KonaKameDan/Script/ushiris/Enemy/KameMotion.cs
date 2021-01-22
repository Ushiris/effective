using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KameMotion : MonoBehaviour
{
    [SerializeField] Animator animator;
    NavMeshAgent agent;

    private void Start()
    {
        agent = gameObject.GetComponentInParent<NavMeshAgent>();
    }

    private void Update()
    {
        animator.SetBool("IsMove", !agent.isStopped);
    }
}
