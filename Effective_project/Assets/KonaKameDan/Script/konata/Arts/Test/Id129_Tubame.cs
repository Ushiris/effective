using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id129_Tubame : MonoBehaviour
{
    [SerializeField] GameObject mostSlashParticleObj;

    [Header("ゾーンステータス")]
    [SerializeField] GameObject zoneObj;
    [SerializeField] float zoneSpeed=8f;
    [SerializeField] float range = 15f;

    [Header("敵を吹き飛ばすやつ")]
    [SerializeField] float explosionForce = 300f;
    [SerializeField] float uppersModifier = 10f;

    bool isTrigger;
    GameObject mostSlashParticle;
    GameObject zone;

    ObjSizChange zoneObjSiz;
    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        transform.parent = null;
        transform.localRotation = Quaternion.Euler(0, 0, 0);

        //ゾーンの展開
        zone = Instantiate(zoneObj, transform);
        zoneObjSiz = Arts_Process.SetAddObjSizChange(
            zone, Vector3.zero, Vector3.one * range,
            zoneSpeed,
            ObjSizChange.SizChangeMode.ScaleUp);
    }

    // Update is called once per frame
    void Update()
    {
        if (zoneObjSiz.GetSizFlag&& !isTrigger)
        {
            //斬撃生成
            mostSlashParticle = Instantiate(mostSlashParticleObj, transform);

            isTrigger = true;
        }
        if (isTrigger)
        {
            if (mostSlashParticle == null)
            {
                //敵を吹き飛ばす
                int layerMask = Arts_Process.GetVsLayerMask(artsStatus);
                Arts_Process.Impact(transform.position, range * 2, layerMask, explosionForce, uppersModifier);

                //オブジェクトの破棄
                Destroy(gameObject);
            }
        }
    }
}
