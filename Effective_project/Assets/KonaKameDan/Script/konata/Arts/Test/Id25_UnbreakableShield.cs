using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id25_UnbreakableShield : MonoBehaviour
{
    [SerializeField] GameObject shieldObj;
    [SerializeField] float timeOver = 30f;
    [SerializeField] float radius = 2f;
    [SerializeField] float speed = 100f;

    const int count = 3;

    // Start is called before the first frame update
    void Start()
    {
        //円状の座標取得
        var pos = Arts_Process.GetCirclePutPos(count, radius, 360);

        //円状に配置する
        GameObject[] shields = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            shields[i] = Instantiate(shieldObj, transform);
            shields[i].transform.localPosition = pos[i];
            shields[i].transform.LookAt(transform);
        }

        //オブジェクトの破壊
        var timer = Arts_Process.TimeAction(gameObject, timeOver);
        timer.LapEvent = () => { Destroy(gameObject); };
    }

    // Update is called once per frame
    void Update()
    {
        //回転
        Arts_Process.ObjRoll(gameObject, speed);
    }
}
