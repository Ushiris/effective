using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KameMotion : MonoBehaviour
{
    [SerializeField] Animator animator;
    NavMeshAgent agent;
    Vector3 prevPos = Vector3.zero;

    private void Start()
    {
        agent = gameObject.GetComponentInParent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        animator.SetBool("IsMove", Vector3.Distance(prevPos, transform.position) > 0.01f);
        prevPos = transform.position;
    }
}
