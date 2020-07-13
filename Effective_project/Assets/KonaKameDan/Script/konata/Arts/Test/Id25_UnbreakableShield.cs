using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id25_UnbreakableShield : MonoBehaviour
{
    [SerializeField] GameObject shieldObj;
    [SerializeField] float timeOver = 30f;
    [SerializeField] float radius = 2f;
    [SerializeField] float speed = 100f;

    StopWatch timer;
    ArtsStatus artsStatus;

    const int count = 3;

    static bool onObj;

    // Start is called before the first frame update
    void Start()
    {
        if (!onObj) onObj = true;
        else Destroy(gameObject);

        artsStatus = GetComponent<ArtsStatus>();

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
    }

    // Update is called once per frame
    void Update()
    {
        //回転
        Arts_Process.ObjRoll(gameObject, speed);
    }

    void Lost()
    {
        onObj = false;
        Destroy(gameObject);
    }
}
