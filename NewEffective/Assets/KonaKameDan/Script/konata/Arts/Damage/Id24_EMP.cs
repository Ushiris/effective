using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id24_EMP : MonoBehaviour
{
    [Header("波紋の演出用")]
    [SerializeField] GameObject rippleObj;
    [SerializeField] Material rippleMaterial;
    [SerializeField] float rippleSpeed = 10f;

    [Header("効果範囲用")]
    [SerializeField] GameObject zoneObj;
    [SerializeField] float range = 10f;
    [SerializeField] float lostTime = 3f;

    [Header("拡散のスタック数に応じてたされる数")]
    [SerializeField] float rangeDown = 0.5f;

    float time;
    GameObject zone;

    StopWatch timer;
    ArtsStatus artsStatus;

    int spreadCount;

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
        Arts_Process.OldArtsDestroy(objs, artsStatus.myObj);

        transform.parent = null;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        //エフェクトの所持数を代入
        spreadCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Spread);

        //サイズの変更
        range += rangeDown * (float)spreadCount;

        //波紋の生成
        Instantiate(rippleObj, transform);
        Arts_Process.RippleShaderReset(rippleMaterial);

        //当たり判定の作成
        //area = Arts_Process.SphereColliderObjInstant(transform, range);
        zone = Instantiate(zoneObj, transform);

        //エリアに入った敵の攻撃を打ち消す
        Arts_Process.SetDestroyArtsZoneStatus(zone, artsTypes, particleTypes);
        zone.layer = Arts_Process.GetEMPLayerMask(artsStatus);

        //ゾーンのサイズ変更
        Arts_Process.SetAddObjSizChange(
                    zone, Vector3.zero, Vector3.one * range,
                    rippleSpeed,
                    ObjSizChange.SizChangeMode.ScaleUp);

        //特定の時間が来たらObjectを消す
        timer = Arts_Process.TimeAction(gameObject, lostTime);
        timer.LapEvent = () => { Lost(); };

        //SE
        Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id245_EMPCube_first, transform.position, artsStatus);
        Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id245_EMPCube_second, transform.position, artsStatus);
    }

    // Update is called once per frame
    void Update()
    {
        //シェーダーを動かす
        time += rippleSpeed * Time.deltaTime;
        Arts_Process.RippleShaderStart(rippleMaterial, time);
    }

    void Lost()
    {
        ArtsActiveObj.Id24_EMP.Remove(artsStatus.myObj);
        Destroy(gameObject);
    }
}
