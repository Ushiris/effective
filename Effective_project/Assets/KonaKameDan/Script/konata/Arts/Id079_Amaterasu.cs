using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id079_Amaterasu : MonoBehaviour
{
    [SerializeField] GameObject satelliteCannonParticleObj;
    [SerializeField] GameObject satelliteCannonStartParticleObj;
    [SerializeField] GameObject satelliteCannonParticleHitObj;
    [SerializeField] Vector3 instantPos = new Vector3(0, 1, 5);
    [SerializeField] float sizUpSpeed = 3;

    GameObject satelliteCannonParticle;

    int frame = 0;
    bool isSatelliteCannonInstant;
    Vector3? hitParticlePos;
    GameObject satelliteCannonStart;

    HitCollision hitCollision;
    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {

        //artsStatus = GetComponent<ArtsStatus>();

        //位置の初期設定
        transform.localPosition = instantPos;
        Arts_Process.RollReset(gameObject);
        var pos = transform.position;
        pos.y = 0;
        transform.position = pos;

        //初手の演出生成
        satelliteCannonStart = Instantiate(satelliteCannonStartParticleObj, transform);
    }

    // Update is called once per frame
    void Update()
    {
        //初手の演出が終わったら実行
        if (satelliteCannonStart == null && !isSatelliteCannonInstant)
        {
            isSatelliteCannonInstant = true;

            //サテライトキャノンの生成
            satelliteCannonParticle = Instantiate(satelliteCannonParticleObj, transform);
            satelliteCannonParticle.transform.localPosition = new Vector3(0, 30, 0);
            satelliteCannonParticle.transform.localScale = new Vector3(10, 0, 10);
            hitCollision = Arts_Process.SetHitCollision(satelliteCannonParticle);
            hitCollision.tags.Add("Ground");
        }

        if (hitCollision != null)
        {
            //地面に当たるまでオブジェクトを伸ばす
            if (!hitCollision.GetOnTrigger)
            {
                var siz = satelliteCannonParticle.transform.localScale;
                siz.y += Time.deltaTime * sizUpSpeed;
                satelliteCannonParticle.transform.localScale = siz;
            }
            else
            {
                //20フレームごとにHitParticleを生成
                if (frame % 20 == 0)
                {
                    if (hitParticlePos == null) hitParticlePos = HitParticlePos();
                    else
                    {
                        var obj = Instantiate(satelliteCannonParticleHitObj, transform);
                        obj.transform.localPosition = (Vector3)hitParticlePos;
                    }
                }
                frame++;
            }
        }

        //削除
        if (transform.childCount == 0) Destroy(gameObject);
    }

    Vector3 HitParticlePos()
    {
        var pos = satelliteCannonParticle.transform.localPosition;
        var siz = satelliteCannonParticle.transform.localScale;
        return new Vector3(pos.x, (pos.y - (siz.y * 2)) + 1f, pos.z);
    }
}
