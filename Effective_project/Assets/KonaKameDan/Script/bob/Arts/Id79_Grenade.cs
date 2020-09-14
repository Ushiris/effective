using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id79_Grenade : MonoBehaviour
{
    [SerializeField] GameObject trajectoryObj;
    [SerializeField] GameObject grenadeObj;
    [SerializeField] GameObject bulletObj;
    [SerializeField] Vector3 v0 = new Vector3(0, 5, 7);
    [SerializeField] float trajectoryCount = 10;
    [SerializeField] float trajectorySpace = 0.1f;
    [SerializeField] float lostTime = 1;
    [SerializeField] float slidingThroughLostTime = 5;

    Vector3 pos;
    GameObject grenade;
    List<GameObject> miracles = new List<GameObject>();

    bool isStart = true;

    ArtsStatus artsStatus;
    ParticleHitPlayExplosion particleHitPlay;


    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        for (int i = 0; i < trajectoryCount; i++)
        {
            miracles.Add(Instantiate(trajectoryObj, transform));
        }

        //軌跡を生成
        miracles = Arts_Process.Trajectory(miracles, trajectorySpace, v0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && isStart)
        {
            transform.parent = null;

            //Grenade生成
            grenade = Instantiate(bulletObj, transform);

            //grenadeを投げる
            Rigidbody jumpCubeRb = grenade.GetComponent<Rigidbody>();
            jumpCubeRb.AddRelativeFor​​ce(v0, ForceMode.VelocityChange);

            //爆発するエフェクトのセット
            particleHitPlay =
                Arts_Process.SetParticleHitPlay(grenade, grenadeObj, transform, artsStatus, lostTime, ParticleHitPlayExplosion.Mode.My, true);

            isStart = false;

            for (int i = 0; i < trajectoryCount; i++)
            {
                Destroy(miracles[i]);
            }

            var timer = Arts_Process.TimeAction(gameObject, slidingThroughLostTime);
            timer.LapEvent = () => { Lost(grenade); };
        }

        if(particleHitPlay != null)
        {
            //爆弾を破壊
            if (particleHitPlay.isTrigger == true)
            {
                Destroy(grenade);
                Destroy(gameObject, 3);
            }
        }
    }
    //一定時間たったら爆発する
    void Lost(GameObject obj)
    {
        if (obj != null)
        {
            pos = obj.transform.position;
            Destroy(obj);
            var explosion = Instantiate(grenadeObj, pos, Quaternion.identity);
            Destroy(gameObject, 2);
        }
    }
}
