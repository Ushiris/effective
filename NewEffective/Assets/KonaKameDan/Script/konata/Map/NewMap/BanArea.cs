using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanArea : MonoBehaviour
{
    static readonly string kPlayerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == kPlayerTag)
        {
            other.transform.position = NewMapManager.GetPlayerRespawnPos;
        }
    }
}
