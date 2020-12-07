using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id257_Haiyoru : MonoBehaviour
{
    [SerializeField] GameObject bomberObj;
    [SerializeField] GameObject explosionParticleObj;
    [SerializeField] float speed = 5f;
    [SerializeField] float rollSpeed = 5f;

    bool isEnemy;
    GameObject target;
    GameObject bomb;


    ArtsStatus artsStatus;
    ParticleHitPlayExplosion playExplosion;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //修正
        transform.parent = null;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        //生成
        bomb = Instantiate(bomberObj, transform);
        bomb.transform.localPosition = new Vector3(1f, 0.5f, 0);

        //触れたものを爆発させる
        playExplosion =
            Arts_Process.SetParticleHitPlay(bomb, explosionParticleObj, transform, artsStatus);

        //SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Id257_Haiyoru_first);
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
            }
        }

        if (target == null)
        {
            //回す
            transform.position = artsStatus.myObj.transform.position;
            Arts_Process.ObjRoll(gameObject, rollSpeed);
        }
        else if(bomb!=null)
        {
            //敵に向かう
            var pos = bomb.transform.position;
            pos = Vector3.MoveTowards(pos, target.transform.position, Time.deltaTime * speed);
            bomb.transform.position = pos;
        }

        //爆弾を破壊
        if (playExplosion.isTrigger == true)
        {
            Destroy(bomb);
        }

        //消す
        if (transform.childCount == 0)
        {
            Destroy(gameObject);

            //SE
            SE_Manager.SePlay(SE_Manager.SE_NAME.Id047_PingPong_third);
        }
    }
}
