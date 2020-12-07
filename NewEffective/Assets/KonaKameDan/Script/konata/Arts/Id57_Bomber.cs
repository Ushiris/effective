using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id57_Bomber : MonoBehaviour
{
    [SerializeField] GameObject bomberObj;
    [SerializeField] GameObject explosionParticleObj;
    [SerializeField] float rollSpeed = 100f;

    GameObject bomber;

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
        bomber = Instantiate(bomberObj, transform);
        bomber.transform.localPosition=new Vector3(1.5f,0,0);

        //触れたものを爆発させる
        playExplosion =
            Arts_Process.SetParticleHitPlay(bomber, explosionParticleObj, transform, artsStatus);

        //SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Id257_Haiyoru_first);
    }

    // Update is called once per frame
    void Update()
    {
        //爆弾を破壊
        if (playExplosion.isTrigger == true)
        {
            Destroy(bomber);
        }
        else
        {
            //回す
            transform.position = artsStatus.myObj.transform.position;
            Arts_Process.ObjRoll(gameObject, rollSpeed);
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
