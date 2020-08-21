using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id15_Fant : MonoBehaviour
{
    [SerializeField] GameObject slashParticleObj;
    [SerializeField] float speed = 10f;

    bool isStop;
    bool isSlash;
    GameObject target;

    ArtsStatus artsStatus;
    ParticleHit slashDamage;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //オブジェクトが登録されている場合このオブジェクトを消す
        var objs = ArtsActiveObj.Id15_Fant;
        Arts_Process.OldArtsDestroy(objs, artsStatus.myObj);

        //敵のポジションを持ってくる
        target = Arts_Process.GetNearTarget(artsStatus);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 targetPos = target.transform.position;
            float dis = Vector3.Distance(artsStatus.myObj.transform.position, targetPos);
            Debug.Log(dis);

            if (dis < 7f && !isSlash)
            {
                //切るパーティクル生成
                var obj = Instantiate(slashParticleObj, transform);

                Damage(obj);

                isSlash = true;
            }
            if(dis < 3.5f)
            {
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
            //オブジェクトを消す
            if (transform.childCount == 0)
            {
                ArtsActiveObj.Id15_Fant.Remove(artsStatus.myObj);
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
