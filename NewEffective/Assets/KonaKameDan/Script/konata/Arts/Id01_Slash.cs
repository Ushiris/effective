using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id01_Slash : MonoBehaviour
{
    [SerializeField] GameObject flySlashParticleObj;

    ParticleHit flySlashDamage;

    // Start is called before the first frame update
    void Start()
    {
        var obj = Instantiate(flySlashParticleObj, transform);

        //当たり判定セット
        flySlashDamage = Arts_Process.SetParticleDamageProcess(obj);
    }

    // Update is called once per frame
    void Update()
    {
        //オブジェクトを消す
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
