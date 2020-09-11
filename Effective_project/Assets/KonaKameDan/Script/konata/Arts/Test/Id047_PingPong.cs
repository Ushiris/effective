using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id047_PingPong : MonoBehaviour
{
    [SerializeField] GameObject explosionParticleObj;
    [SerializeField] GameObject bulletObj;
    [SerializeField] float force = 30f;
    [SerializeField] float lostTime = 3.5f;
    [SerializeField] Vector3 v0 = new Vector3(0, 5, 10);

    Vector3 pos;
    GameObject bullet;

    StopWatch timer;
    ArtsStatus artsStatus;
    ParticleHitPlayExplosion particleHitPlay;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //親子解除
        transform.parent = null;

        //弾の生成
        bullet = Instantiate(bulletObj, transform);
        var rb = bullet.GetComponent<Rigidbody>();
        rb.AddRelativeFor​​ce(v0, ForceMode.VelocityChange);

        //爆発するエフェクトのセット
        particleHitPlay =
            Arts_Process.SetParticleHitPlay(bullet, explosionParticleObj, transform, artsStatus);

        var timer = Arts_Process.TimeAction(gameObject, lostTime);
        timer.LapEvent = () => { Lost(bullet); };
    }

    private void Update()
    {
        if (particleHitPlay.isTrigger) Lost(bullet);
    }

    //一定時間たったら爆発する
    void Lost(GameObject obj)
    {
        if (obj != null)
        {
            pos = obj.transform.position;
            Destroy(obj);
            var explosion = Instantiate(explosionParticleObj, pos, Quaternion.identity);
            Destroy(gameObject, 2);
        }
    }
}
