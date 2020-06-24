using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Transform player;
    Rigidbody rb;
    float speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.GetManager.GetPlObj.transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);

        if (Vector3.Distance(player.position, transform.position) < 3)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            rb.velocity = transform.forward * speed;
        }
    }
}
