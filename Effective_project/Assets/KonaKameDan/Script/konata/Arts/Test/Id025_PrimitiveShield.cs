using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id025_PrimitiveShield : MonoBehaviour
{
    [SerializeField] GameObject shieldObj;
    [SerializeField] float siz = 3f;
    [SerializeField] float force = 8f;
    [SerializeField] float lostTime = 4f;

    StopWatch timer;
    ArtsStatus artsStatus;

    static bool onObj;

    // Start is called before the first frame update
    void Start()
    {
        if (!onObj) onObj = true;
        else Destroy(gameObject);

        artsStatus = GetComponent<ArtsStatus>();
        transform.parent = null;

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
        var rb = shield.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.mass = 1000f;
        Arts_Process.RbMomentMove(shield, force);

        //消す
        timer = Arts_Process.TimeAction(gameObject, lostTime);
        timer.LapEvent = () => { Lost(); };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Lost()
    {
        onObj = false;
        Destroy(gameObject);
    }
}
