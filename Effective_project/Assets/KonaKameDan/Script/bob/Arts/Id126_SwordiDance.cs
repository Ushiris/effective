using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id126_SwordiDance : MonoBehaviour
{
    [SerializeField] GameObject swordObj;
    [SerializeField] float timeOver = 30f;
    [SerializeField] float radius = 2f;
    [SerializeField] float speed = 100f;

    StopWatch timer;
    ArtsStatus artsStatus;

    const int count = 3;

    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        transform.parent = null;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        //円状の座標取得
        var pos = Arts_Process.GetCirclePutPos(count, radius, 360);

        GameObject[] swords = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            //円状に配置する
            swords[i] = Instantiate(swordObj, transform);
            swords[i].transform.localPosition = pos[i];
            swords[i].transform.rotation = Look(swords[i], gameObject);

            //Layerのセット
            Arts_Process.SetArtsLayerMask(artsStatus, swords[i]);
        }

        //オブジェクトの破壊
        timer = Arts_Process.TimeAction(gameObject, timeOver);
        timer.LapEvent = () => { Lost(); };
    }

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
    }
    Quaternion Look(GameObject a, GameObject b)
    {
        var aim = a.transform.position - b.transform.position;
        var look = Quaternion.LookRotation(aim);
        return look * Quaternion.AngleAxis(90, Vector3.right);
    }
}
