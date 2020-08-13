using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id14_RollSlash : MonoBehaviour
{
    [SerializeField] GameObject rollSlashParticleObj;

    ArtsStatus artsStatus;
    ParticleHit slashDamage;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //オブジェクトが登録されている場合このオブジェクトを消す
        var objs = ArtsActiveObj.Id14_RollSlash;
        if (Arts_Process.GetMyActiveArts(objs, artsStatus.myObj))
        {
            Destroy(gameObject);
        }

        //回転切りパーティクル生成
        var obj = Instantiate(rollSlashParticleObj, transform);

        //当たり判定セット
        slashDamage = Arts_Process.SetParticleDamageProcess(obj);

    }

    // Update is called once per frame
    void Update()
    {
        //オブジェクトを消す
        if (transform.childCount == 0)
        {
            ArtsActiveObj.Id14_RollSlash.Remove(artsStatus.myObj);
            Destroy(gameObject);
        }
    }
}
