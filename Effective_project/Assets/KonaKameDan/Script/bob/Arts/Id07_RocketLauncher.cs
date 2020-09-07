using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id07_RocketLauncher : MonoBehaviour
{
    [SerializeField] GameObject RocketLauncherParticleObj;
    GameObject RocketLauncherParticle;

    void Start()
    {
        //パーティクル生成
        RocketLauncherParticle = Instantiate(RocketLauncherParticleObj, transform);
    }
    void Update()
    {
        //オブジェクトを消す
        if (transform.childCount == 0) Destroy(gameObject);
    }
}
