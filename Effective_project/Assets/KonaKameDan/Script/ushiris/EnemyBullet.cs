﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Transform player_tr;
    Rigidbody rb;
    StopWatch timer;

    // Start is called before the first frame update
    void Start()
    {
        player_tr = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody>();

        timer = gameObject.AddComponent<StopWatch>();
        timer.LapTime = 8;
        timer.LapEvent = () => { Destroy(gameObject); };
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.LookAt(player_tr);
        rb.AddForce(transform.forward * 0.01f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<Life>() == null)
            {
                Debug.Log("error! player is undead!(add test parameter life to player.)");
                Life pl_life = other.gameObject.AddComponent<Life>();
                pl_life.LifeSetup(10, 10, 1);
            }
            other.gameObject.GetComponent<Life>().Damage(1);
            Debug.Log("HIT! player's life=" + other.gameObject.GetComponent<Life>().HP);
        }
    }
}
