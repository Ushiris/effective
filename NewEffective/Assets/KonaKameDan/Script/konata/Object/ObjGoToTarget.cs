using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjGoToTarget : MonoBehaviour
{
    public bool isGo;
    public Transform targetPos;

    float time;
    Rigidbody rb;
    new BoxCollider collider;

    static readonly float speed = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        rb.isKinematic = true;
        collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGo) return;
        time += Time.deltaTime * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos.position, speed * time);
    }
}
