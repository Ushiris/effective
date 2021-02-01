using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id025_PrimitiveShield : MonoBehaviour
{
    [SerializeField] GameObject shieldObj;
    [SerializeField] float siz = 3f;
    [SerializeField] float force = 8f;
    [SerializeField] float lostTime = 4f;

    [Header("追尾のスタック数に応じてたされる数")]
    [SerializeField] float plusTime = 0.2f;

    Rigidbody rb;

    StopWatch timer;
    ArtsStatus artsStatus;

    int homingCount;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        var objs = ArtsActiveObj.Id025_PrimitiveShield;
        Arts_Process.OldArtsDestroy(objs, artsStatus.myObj);

        transform.parent = null;

        //エフェクトの所持数を代入
        homingCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Homing);

        //持続時間変更
        lostTime += plusTime * (float)homingCount;

        //オブジェクトの生成
        GameObject shield = Instantiate(shieldObj, transform);

        //Layerのセット
        Arts_Process.SetShieldLayer(artsStatus, shield);

        //サイズの変更
        Vector3 pos = shield.transform.localPosition;
        pos.y = siz / 2 + pos.y + 0.2f;
        shield.transform.localScale = new Vector3(siz, siz, 1f);
        shield.transform.localPosition = pos;

        //移動
        rb = shield.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.mass = 5000f;
        rb.drag = 40f;
        //Arts_Process.RbMomentMove(shield, force);

        //消す
        timer = Arts_Process.TimeAction(gameObject, lostTime);
        timer.LapEvent = () => { Lost(); };

        //SE
        Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id025_PrimitiveShield_first, transform.position, artsStatus);
    }

    // Update is called once per frame
    void Update()
    {
        
        rb.velocity = force/2.5f*Time.timeScale * rb.transform.forward;
    }

    void Lost()
    {
        ArtsActiveObj.Id025_PrimitiveShield.Remove(artsStatus.myObj);
        Destroy(gameObject);

        //SE
        Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id025_PrimitiveShield_third, transform.position, artsStatus);
    }
}
