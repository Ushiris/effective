using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id25_UnbreakableShield : MonoBehaviour
{
    [SerializeField] GameObject shieldObj;
    [SerializeField] float timeOver = 30f;
    [SerializeField] float radius = 2f;
    [SerializeField] float speed = 100f;

    [Header("追尾のスタック数に応じてたされる数")]
    [SerializeField] float plusTime = 0.5f;

    StopWatch timer;
    ArtsStatus artsStatus;

    int homingCount;

    const int count = 3;

    AudioSource se;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //オブジェクトが登録されている場合このオブジェクトを消す
        var objs = ArtsActiveObj.Id25_UnbreakableShield;
        Arts_Process.OldArtsDestroy(objs, artsStatus.myObj);

        transform.parent = null;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        //エフェクトの所持数を代入
        homingCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Homing);

        //持続時間変更
        timeOver += plusTime * (float)homingCount;

        //円状の座標取得
        var pos = Arts_Process.GetCirclePutPos(count, radius, 360);

        GameObject[] shields = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            //円状に配置する
            shields[i] = Instantiate(shieldObj, transform);
            shields[i].transform.localPosition = pos[i];
            shields[i].transform.LookAt(transform);

            //Layerのセット
            Arts_Process.SetShieldLayer(artsStatus, shields[i]);
        }

        

        //オブジェクトの破壊
        timer = Arts_Process.TimeAction(gameObject, timeOver);
        timer.LapEvent = () => { Lost(); };

        //SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Id025_PrimitiveShield_first);
        se = SE_Manager.SePlay(SE_Manager.SE_NAME.Id25_UnbreakableShield_second);
    }

    // Update is called once per frame
    void Update()
    {
        //回転
        transform.position = artsStatus.myObj.transform.position;
        Arts_Process.ObjRoll(gameObject, speed);
    }

    void Lost()
    {
        ArtsActiveObj.Id25_UnbreakableShield.Remove(artsStatus.myObj);
        Destroy(gameObject);

        SE_Manager.ForcedPlayStop(se);
        //SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Id025_PrimitiveShield_third);
    }
}
