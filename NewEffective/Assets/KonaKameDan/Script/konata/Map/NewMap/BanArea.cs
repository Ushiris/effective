using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanArea : MonoBehaviour
{
    static readonly string kPlayerTag = "Player";
    static readonly string kEnemyTag = "Enemy";

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == kPlayerTag)
        {
            other.transform.position = NewMapManager.GetPlayerRespawnPos;
            var rb = other.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
        }
        else if(other.tag== kEnemyTag)
        {
            Destroy(other);
        }
    }
}
