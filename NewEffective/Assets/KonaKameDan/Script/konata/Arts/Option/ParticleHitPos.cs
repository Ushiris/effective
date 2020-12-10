using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHitPos : MonoBehaviour
{
    public delegate void Action();
    public Action OnPlay;
    public Vector3 GetParticlePos { get; private set; }
    public ArtsStatus artsStatus;

    bool isCheck;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("aaa");
        if(other.gameObject.tag == "Ground")
        {
            GetParticlePos = transform.position;
            OnPlay();
        }
    }
}
