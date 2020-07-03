using System.Collections;
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
        if (GameObject.FindWithTag("PlayerBody") == null)
        {
            player_tr = transform;
        }
        else
        {
            player_tr = GameObject.FindWithTag("PlayerBody").transform;
        }
        
        rb = GetComponent<Rigidbody>();

        timer = gameObject.AddComponent<StopWatch>();
        timer.LapTime = 5;
        timer.LapEvent = () => { Destroy(gameObject); };
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (transform.position.y<0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBody")
        {
            if (other.GetComponent<Life>() == null)
            {
                Debug.Log("error! player is undead!(add life to player.)");
                Life pl_life = other.gameObject.AddComponent<Life>();
                pl_life.LifeSetup(1);
            }

            other.gameObject.GetComponent<Life>().Damage(1);
            Debug.Log("HIT! player's life=" + other.gameObject.GetComponent<Life>().HP);
            Destroy(gameObject);
        }
    }

}
