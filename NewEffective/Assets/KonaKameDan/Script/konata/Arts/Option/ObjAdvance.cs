using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjAdvance : MonoBehaviour
{
    public float speed = 30f;
    public GameObject hitObj { get; private set; }
    [SerializeField] bool hitMoveStop = true;

    bool isMove = true;

    // Update is called once per frame
    void Update()
    {
        if (!isMove) return;
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            if (hitMoveStop)
            {
                isMove = false;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                hitObj = other.gameObject;
            }
        }
    }
}
