using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id249_Icarus : MonoBehaviour
{
    [SerializeField] GameObject icarusParticleObj;
    [SerializeField] float range = 10f;
    [SerializeField] float force = 10f;
    [SerializeField] float lostTime = 30f;

    [Header("防御のスタック数に応じてたされる数")]
    [SerializeField] float plusTime = 0.5f;

    [Header("飛翔のスタック数に応じてたされる数")]
    [SerializeField] float plusRange = 0.3f;

    int layerMask;
    GameObject area;

    StopWatch timer;
    ArtsStatus artsStatus;

    int barrierCount;
    int spreadCount;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //オブジェクトが登録されている場合このオブジェクトを消す
        var objs = ArtsActiveObj.Id249_Icarus;
        if (Arts_Process.GetMyActiveArts(objs, artsStatus.myObj))
        {
            Destroy(gameObject);
        }

        transform.parent = null;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        //エフェクトの所持数を代入
        barrierCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Barrier);
        spreadCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Spread);

        //持続時間変更
        lostTime += plusTime * (float)barrierCount;

        //範囲変更
        range += plusRange * (float)spreadCount;

        //パーティクルの生成
        Instantiate(icarusParticleObj, transform);

        //レイヤーの獲得
        layerMask = Arts_Process.GetVsLayerMask(artsStatus);


        //特定の時間が来たらObjectを消す
        timer = Arts_Process.TimeAction(gameObject, lostTime);
        timer.LapEvent = () => { Lost(); };
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = artsStatus.myObj.transform.position;

        Collider[] enemies;
        Vector3 pos = transform.position;
        enemies = Physics.OverlapSphere(pos, range, layerMask);

        //範囲に入った敵に下向きに力を加える
        foreach (Collider hit in enemies)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(new Vector3(0, -1, 0) * force, ForceMode.Impulse);
            }
        }
    }

    void Lost()
    {
        ArtsActiveObj.Id249_Icarus.Remove(artsStatus.myObj);
        Destroy(gameObject);
    }
}
