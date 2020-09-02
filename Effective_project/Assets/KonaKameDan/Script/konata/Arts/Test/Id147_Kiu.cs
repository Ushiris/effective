using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id147_Kiu : MonoBehaviour
{
    [SerializeField] GameObject slashParticleObj;
    [SerializeField] GameObject explosionParticleObj;

    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //切るエフェクトの生成
        var slashParticle = Instantiate(slashParticleObj, transform);

        //爆発するエフェクトのセット
        Arts_Process.SetParticleHitPlay(slashParticle, explosionParticleObj, transform, artsStatus);
    }

    // Update is called once per frame
    void Update()
    {
        //本体を消す
        if (transform.childCount == 0) Destroy(gameObject);
    }
}
