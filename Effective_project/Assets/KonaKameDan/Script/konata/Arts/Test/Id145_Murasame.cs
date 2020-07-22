using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id145_Murasame : MonoBehaviour
{
    [SerializeField] GameObject mostSlashParticleObj;
    [SerializeField] float speed = 10f;

    bool isStop;
    GameObject target;

    ArtsStatus artsStatus;
    ParticleHit slashDamage;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //オブジェクトが登録されている場合このオブジェクトを消す
        var objs = ArtsActiveObj.Id145_Murasame;
        if (Arts_Process.GetMyActiveArts(objs, artsStatus.myObj))
        {
            Destroy(gameObject);
        }

        //敵のポジションを持ってくる
        target = Arts_Process.GetNearTarget(artsStatus);

        transform.parent = null;

        //無敵にする
        Arts_Process.Invisible(artsStatus, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 targetPos = target.transform.position;
            float dis = Vector3.Distance(artsStatus.myObj.transform.position, targetPos);

            if (dis < 2.5f && !isStop)
            {
                //切るパーティクル生成
                var obj = Instantiate(mostSlashParticleObj, transform);
                obj.transform.position = targetPos;

                Damage(obj);

                isStop = true;
            }
            else if (!isStop)
            {
                //敵の元まで行く
                float step = speed * Time.deltaTime;
                artsStatus.myObj.transform.position =
                    Vector3.MoveTowards(artsStatus.myObj.transform.position, targetPos, step);
            }
        }

        if (isStop || target == null)
        {
            if (transform.childCount == 0)
            {
                //無敵を解除する
                Arts_Process.Invisible(artsStatus, true);

                //オブジェクトを消す
                ArtsActiveObj.Id145_Murasame.Remove(artsStatus.myObj);
                Destroy(gameObject);
            }
        }
    }

    void Damage(GameObject obj)
    {
        //当たり判定セット
        slashDamage = Arts_Process.SetParticleDamageProcess(obj);
    }
}