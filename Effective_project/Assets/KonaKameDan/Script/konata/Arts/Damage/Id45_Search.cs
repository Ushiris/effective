using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id45_Search : MonoBehaviour
{
    [Header("サーチシェーダー")]
    [SerializeField] Material worldSearchShader;
    [SerializeField] float speed = 30;

    [Header("エフェクトオブジェクト")]
    [SerializeField] Material effectObjectMaterialChange;
    [SerializeField] Material effectObjectMaterialDefault;
    [SerializeField] float materialChangeTime = 50f;

    [Header("追尾のスタック数に応じてたされる数")]
    [SerializeField] float plusTime = 0.05f;

    StopWatch timer;
    GameObject[] effectObj;
    float dis;

    ArtsStatus artsStatus;

    int homingCount;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        Vector3 pos = PlayerManager.GetManager.GetPlObj.transform.position;

        //エフェクトの所持数を代入
        homingCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Homing);
        //持続時間変更
        materialChangeTime += plusTime * (float)homingCount;

        //シェーダーの初期化
        Arts_Process.SearchPosSet(worldSearchShader, pos);
        Arts_Process.SearchShaderReset(worldSearchShader);

        //エフェクトオブジェクトのマテリアル変更
        effectObj = GameObject.FindGameObjectsWithTag("EffectObject");
        Arts_Process.MaterialsChange(effectObj, effectObjectMaterialChange, 1);

        timer = gameObject.AddComponent<StopWatch>();
        timer.LapTime = materialChangeTime;
        timer.LapEvent = () => { EffectMaterialDefault(); };

        //SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Shot);
    }

    // Update is called once per frame
    void Update()
    {
        dis += speed * Time.deltaTime;
        Arts_Process.SearchShaderStart(worldSearchShader, dis);

        if (dis > 200f)
        {
            Arts_Process.SearchShaderStart(worldSearchShader, 0);
            Destroy(gameObject);
        }
    }

    void EffectMaterialDefault()
    {
        Arts_Process.MaterialsChange(effectObj, effectObjectMaterialDefault, 1);
    }
}
