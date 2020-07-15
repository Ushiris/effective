using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id259_FlyCurse : MonoBehaviour
{
    [SerializeField] GameObject zoneObj;
    [SerializeField] Vector3 siz = new Vector3(10f, 10f, 10f);
    [SerializeField] float sizUpSpeed = 5f;
    [SerializeField] float lostTime = 60f;

    GameObject zone;

    StopWatch timer;
    ArtsStatus artsStatus;

    List<ArtsStatus.ArtsType> artsTypes = new List<ArtsStatus.ArtsType>()
    {
        ArtsStatus.ArtsType.Shot
    };

    List<ArtsStatus.ParticleType> particleTypes = new List<ArtsStatus.ParticleType>()
    {
        ArtsStatus.ParticleType.Enemy,ArtsStatus.ParticleType.Player
    };

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //オブジェクトが登録されている場合このオブジェクトを消す
        var objs = ArtsActiveObj.Id259_FlyCurse;
        if (Arts_Process.GetMyActiveArts(objs, artsStatus.myObj))
        {
            Destroy(gameObject);
        }

        //生成
        zone = Instantiate(zoneObj, transform);

        //ゾーンに入った遠距離攻撃Artsを消す
        Arts_Process.SetDestroyArtsZone(zone, artsTypes, particleTypes);

        //ゾーンのサイズ変更
        Arts_Process.SetAddObjSizChange(
                    zone, Vector3.zero, siz,
                    sizUpSpeed,
                    ObjSizChange.SizChangeMode.ScaleUp);

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
        ArtsActiveObj.Id259_FlyCurse.Remove(artsStatus.myObj);
        Destroy(gameObject);
    }
}
