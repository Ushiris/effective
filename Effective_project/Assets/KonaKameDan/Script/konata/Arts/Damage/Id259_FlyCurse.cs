using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id259_FlyCurse : MonoBehaviour
{
    [SerializeField] GameObject zoneObj;
    [SerializeField] Vector3 siz = new Vector3(10f, 10f, 10f);
    [SerializeField] float sizUpSpeed = 5f;
    [SerializeField] float lostTime = 60f;

    [Header("防御のスタック数に応じてたされる数")]
    [SerializeField] float plusRange = 0.5f;

    [Header("追尾のスタック数に応じてたされる数")]
    [SerializeField] float plusTime = 0.5f;

    GameObject zone;

    StopWatch timer;
    ArtsStatus artsStatus;

    int barrierCount;
    int homingCount;

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

        //エフェクトの所持数を代入
        barrierCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Barrier);
        homingCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Homing);

        //効果範囲変更
        siz += (Vector3.one * (plusRange * (float)barrierCount)) * 2;

        //持続時間変更
        lostTime += plusTime * (float)plusTime;

        //生成
        zone = Instantiate(zoneObj, transform);

        //ゾーンに入った遠距離攻撃Artsを消す
        Arts_Process.SetDestroyArtsZoneStatus(zone, artsTypes, particleTypes);
        zone.layer = LayerMask.NameToLayer("FlyCurse");

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
