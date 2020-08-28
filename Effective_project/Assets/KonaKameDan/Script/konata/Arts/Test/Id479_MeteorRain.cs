using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id479_MeteorRain : MonoBehaviour
{
    [SerializeField] GameObject starDustParticleObj;

    [Header("隕石の生成位置関係")]
    [SerializeField] Vector3 instantSiz = new Vector3(6, 1, 6);
    [SerializeField] float instantSpace = 1.2f;
    [SerializeField] float h = 30;

    [Header("隕石の速度")]
    [SerializeField] float speed = 30f;

    [Header("生成タイミング")]
    [SerializeField] float interval = 0.1f;

    GameObject groupObj;

    List<ForwardMove> forwardMoves = new List<ForwardMove>();
    List<Vector3> boxPos = new List<Vector3>();

    StopWatch timer;

    const float r = 45;
    const float fixX = -30;

    // Start is called before the first frame update
    void Start()
    {
        Arts_Process.RollReset(gameObject);

        groupObj = new GameObject("GroupObj");

        boxPos = Arts_Process.SetBoxInstantPos(instantSiz, instantSpace);

        groupObj.transform.localRotation = Quaternion.Euler(0, 0, r);
        Vector3 pos = new Vector3(fixX, h, 8);
        groupObj.transform.localPosition = pos;

        timer = gameObject.AddComponent<StopWatch>();
        timer.LapTime = interval;
        timer.LapEvent = () => { Instant(); };
    }

    // Update is called once per frame
    void Update()
    {
        if (boxPos.Count == 0) Destroy(gameObject);
    }

    void Instant()
    {
        var ran = Random.Range(0, boxPos.Count);

        //隕石の生成
        var starDustParticle = Instantiate(starDustParticleObj, groupObj.transform);
        starDustParticle.transform.localPosition = boxPos[ran];
        boxPos.RemoveAt(ran);

        //隕石を動かす
        var forwardMove = Arts_Process.SetForwardMove(starDustParticle, -speed);
        forwardMove.isStart = true;
    }
}
