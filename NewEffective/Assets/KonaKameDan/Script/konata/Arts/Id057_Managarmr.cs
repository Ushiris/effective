using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id057_Managarmr : MonoBehaviour
{
    [SerializeField] GameObject managarmrParticleObj;
    [SerializeField] float force = 30f;

    GameObject managarmrParticle;
    GameObject target;

    new ParticleSystem particleSystem;
    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        managarmrParticle = Instantiate(managarmrParticleObj, transform);
        particleSystem = managarmrParticle.GetComponent<ParticleSystem>();

        //敵のポジションを持ってくる
        target = Arts_Process.GetNearTarget(artsStatus);

        //SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Id057_Managarmr_first);
    }

    // Update is called once per frame
    void Update()
    {
        //ホーミング処理
        if (target != null && managarmrParticle != null)
        {
            Arts_Process.HomingParticle(particleSystem, target, force);
        }

        //オブジェクトを消す

        if (managarmrParticle != null)
        {
            if (managarmrParticle.transform.childCount == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
