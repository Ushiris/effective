using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanArea : MonoBehaviour
{
    static readonly string kPlayerTag = "Player";

    PlayerRespawn playerRespawn;

    private void Start()
    {
        playerRespawn = NewMap.GetPlayerObj.GetComponent<PlayerRespawn>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == kPlayerTag)
        {
            playerRespawn.OnLock(true);
        }
    }
}
