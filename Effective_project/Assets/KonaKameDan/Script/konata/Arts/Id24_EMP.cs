using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id24_EMP : MonoBehaviour
{
    [SerializeField] GameObject rippleParticleObj;
    [SerializeField] float range = 10f;
    [SerializeField] float lostTime = 3f;

    GameObject area;

    StopWatch timer;
    ArtsStatus artsStatus;

    List<ArtsStatus.ArtsType> artsTypes = new List<ArtsStatus.ArtsType>()
    {
        ArtsStatus.ArtsType.Shot,ArtsStatus.ArtsType.Slash,ArtsStatus.ArtsType.Support
    };

    List<ArtsStatus.ParticleType> particleTypes = new List<ArtsStatus.ParticleType>()
    {
        ArtsStatus.ParticleType.Enemy
    };

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //オブジェクトが登録されている場合このオブジェクトを消す
        var objs = ArtsActiveObj.Id24_EMP;
        if (Arts_Process.GetMyActiveArts(objs, artsStatus.myObj))
        {
            Destroy(gameObject);
        }

        //波紋の作成
        Instantiate(rippleParticleObj, transform);

        //当たり判定の作成
        area = Arts_Process.SphereColliderObjInstant(transform, range);

        //エリアに入った敵の攻撃を打ち消す
        Arts_Process.SetDestroyArtsZone(area, artsTypes, particleTypes);

        //特定の時間が来たらObjectを消す
        timer = Arts_Process.TimeAction(gameObject, lostTime);
        timer.LapEvent = () => { Lost(); };
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Lost()
    {
        ArtsActiveObj.Id24_EMP.Remove(artsStatus.myObj);
        Destroy(gameObject);
    }
}
