using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBarrier : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        TutorialBarrierInfo.instance.Burrier();
    }
}
