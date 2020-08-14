using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id12_ShieldBash : MonoBehaviour
{
    [SerializeField] GameObject shieldObj;
    [SerializeField] Vector3 shieldPos = new Vector3(1.5f, 0, 0);
    [SerializeField] float endRot = -120f;
    [SerializeField] float startSpeed = 5f;
    [SerializeField] float acc = 30f;
    [SerializeField] float lostTime = 1.5f;

    float rot;
    float time;

    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        //artsStatus = GetComponent<ArtsStatus>();

        //シールドの生成
        var shield = Instantiate(shieldObj, transform);
        shield.transform.localPosition = shieldPos;

    }

    // Update is called once per frame
    void Update()
    {
        //回転速度の計算
        time += Time.deltaTime;
        rot -= (Time.deltaTime * startSpeed) + (time * acc);
        rot = Mathf.Clamp(rot, endRot, 0);

        if (rot != endRot)
        {
            //回転
            transform.localRotation = Quaternion.AngleAxis(rot, Vector3.up);
        }
        else
        {
            //オブジェクトの破壊
            Destroy(gameObject, lostTime);
        }
    }
}
