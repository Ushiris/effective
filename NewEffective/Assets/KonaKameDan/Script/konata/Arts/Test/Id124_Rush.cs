using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id124_Rush : MonoBehaviour
{
    [SerializeField] GameObject swordParticleObj;
    [SerializeField] float lostTime = 30;

    StopWatch timer;
    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //火力アップ用
        var objs = ArtsActiveObj.Id145_Murasame;
        if (Arts_Process.OldArtsDestroy(objs, artsStatus.myObj))
        {

        }

        //斬撃の生成
        float rotX = Random.Range(-90f, 90f);
        var swordParticle = Instantiate(swordParticleObj, transform);
        swordParticle.transform.localRotation = Quaternion.Euler(rotX, 0, 0);

        //消す
        timer = Arts_Process.TimeAction(gameObject, lostTime);
        timer.LapEvent = () => { Lost();  };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Lost()
    {
        ArtsActiveObj.Id124_Rush.Remove(artsStatus.myObj);
        Destroy(gameObject);
    }
}
