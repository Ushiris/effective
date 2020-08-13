using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id012_Invisible : MonoBehaviour
{
    [SerializeField] float lostTime = 30f;

    StopWatch timer;
    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //すでに存在しているArtsを消す
        var objs = ArtsActiveObj.Id012_Invisible;
        Arts_Process.OldArtsDestroy(objs, gameObject);

        //透明化
        Arts_Process.Invisible(artsStatus, true);

        //特定の時間が来たらObjectを消す
        timer = Arts_Process.TimeAction(gameObject, lostTime);
        timer.LapEvent = () => { Lost(); };
    }

    void Lost()
    {
        Arts_Process.Invisible(artsStatus, false);
        Destroy(gameObject);
    }
}
