using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtsHit : MonoBehaviour
{
    private ArtsHitMessage artsHitMessage;
    public int hitCount;
    public void Start()
    {
        GameObject anotherObject = GameObject.FindWithTag("Enemy");
        artsHitMessage = anotherObject.GetComponent<ArtsHitMessage>();
        hitCount = 0;
    }
    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.tag == "Enemy")// タグが"Enemy"の時だけ実行
        {
            artsHitMessage.HitMessage();
        }
    }

    public int HitCount
    {
        get { return hitCount; }
        private set { hitCount = value; }
    }
}