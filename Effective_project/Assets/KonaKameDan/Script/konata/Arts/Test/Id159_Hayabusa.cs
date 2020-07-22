using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id159_Hayabusa : MonoBehaviour
{
    [SerializeField] GameObject slashParticleObj;
    [SerializeField] float radius = 10f;

    int layerMask;

    ArtsStatus artsStatus;
    ParticleHit slashDamage;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //親子解除
        transform.parent = null;

        //敵のLayerを取得
        layerMask = Arts_Process.GetVsLayerMask(artsStatus);

        Vector3 pos = transform.position;
        Collider[] enemies = Physics.OverlapSphere(pos, radius, layerMask);

        //範囲に入った敵を切る
        foreach (Collider hit in enemies)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                var obj = Instantiate(slashParticleObj, transform);
                obj.transform.position = hit.transform.position;
                obj.transform.localRotation = Look(obj, gameObject);

                Damage(obj);
            }
        }
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

    void Damage(GameObject obj)
    {
        //当たり判定セット
        slashDamage = Arts_Process.SetParticleDamageProcess(obj);
    }

    Quaternion Look(GameObject a, GameObject b)
    {
        var aim = b.transform.position - a.transform.position;
        return Quaternion.LookRotation(aim, new Vector3(0, 0, -1));
    }
}
