using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id257_Haiyoru : MonoBehaviour
{
    [SerializeField] GameObject haiyoruParticleObj;
    [SerializeField] float speed = 5f;
    [SerializeField] float rollSpeed = 5f;

    bool isEnemy;
    GameObject haiyoruParticle;
    GameObject target;

    ParticleSystem ps;
    ParticleSystem.MainModule main;

    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //修正
        transform.parent = null;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        //生成
        haiyoruParticle = Instantiate(haiyoruParticleObj, transform);
        haiyoruParticle.transform.localPosition = new Vector3(1f, 0.5f, 0);
        ps = haiyoruParticle.GetComponent<ParticleSystem>();
        main = ps.main;
        main.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnemy)
        {
            //目標を取得
            target = Arts_Process.GetNearTarget(artsStatus);
            if (target != null)
            {
                isEnemy = true;
                main.loop = false;
            }
        }

        if (target == null)
        {
            //回す
            transform.position = artsStatus.myObj.transform.position;
            Arts_Process.ObjRoll(gameObject, rollSpeed);
        }
        else
        {
            //敵に向かう
            var pos = haiyoruParticle.transform.position;
            pos = Vector3.Lerp(pos, target.transform.position, Time.deltaTime * speed);
            haiyoruParticle.transform.position = pos;
        }

        //消す
        if (transform.childCount == 0) Destroy(gameObject);
    }
}
