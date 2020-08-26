using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id079_Amaterasu : MonoBehaviour
{
    [SerializeField] GameObject satelliteCannonParticleObj;
    [SerializeField] GameObject satelliteCannonStartParticleObj;
    [SerializeField] Vector3 instantPos = new Vector3(0, 0, 5);

    bool isSatelliteCannonInstant;
    GameObject satelliteCannonStart;

    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {

        //artsStatus = GetComponent<ArtsStatus>();

        //位置の初期設定
        transform.parent = null;
        var rot = transform.rotation;
        var rotV3 = new Vector3(0, 1, 0) * rot.eulerAngles.y;
        transform.rotation = Quaternion.Euler(rotV3);
        transform.localPosition += instantPos;

        //初手の演出生成
        satelliteCannonStart = Instantiate(satelliteCannonStartParticleObj, transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (satelliteCannonStart == null && !isSatelliteCannonInstant)
        {
            isSatelliteCannonInstant = true;

            //サテライトキャノンの生成
            var obj= Instantiate(satelliteCannonParticleObj, transform);
            obj.transform.localPosition = new Vector3(0, 10, 0);
        }

        //オブジェクトを消す
        if (isSatelliteCannonInstant)
        {
            if (transform.childCount == 0) Destroy(gameObject);
        }
    }
}
