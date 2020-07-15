using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id024_Diffusion : MonoBehaviour
{
    [SerializeField] GameObject beamParticleObj;
    [SerializeField] float force = 100f;
    [SerializeField] float radius = 2f;
    [SerializeField] float lostTime = 3f;

    StopWatch timer;
    ArtsStatus artsStatus;

    const int count = 4;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;

        //円状の座標取得
        var pos = Arts_Process.GetCirclePutPos(count, radius, 360);

        GameObject[] beam = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            //円状に配置する
            beam[i] = Instantiate(beamParticleObj, transform);
            beam[i].transform.localPosition = pos[i];
            beam[i].transform.LookAt(transform);

            //移動
            Arts_Process.RbMomentMove(beam[i], force);
        }

        //オブジェクトの破壊
        var timer = Arts_Process.TimeAction(gameObject, lostTime);
        timer.LapEvent = () => { Destroy(gameObject); };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
