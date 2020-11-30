using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 選択後に発生するカメラの動き
/// </summary>
public class TitleMenuNextStageCameraMove : MonoBehaviour
{
    enum MoveProcess { Start, Next, End }

    [SerializeField] GameObject[] movePint;
    [SerializeField] Transform pivot;
    [SerializeField] float speed = 3;
    [SerializeField] ImageAlpha imageAlpha;

    MoveProcess moveProcess = MoveProcess.Start;
    Vector3 target;

    // Update is called once per frame
    void Update()
    {
        if (!TitleMenuSelectIcon.IsNextStageMove) return;

        switch (moveProcess)
        {
            case MoveProcess.Start: Move(MoveProcess.Next); break;
            case MoveProcess.Next: Move(MoveProcess.End); break;
            case MoveProcess.End: TitleMenuSelectIcon.IsSceneChange = true; break;
            default: break;
        }
        transform.LookAt(pivot);
    }

    void Move(MoveProcess process)
    {
        target = movePint[(int)moveProcess].transform.position;
        var dis = Vector3.Distance(transform.position, target);

        if (moveProcess == MoveProcess.Start)
        {
            if (dis == 0) { moveProcess = process; imageAlpha.IsAlpha = true; }

            transform.position =
                Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime * 10);
        }
        else
        {
            if (dis < 0.2f) moveProcess = process;

            transform.position =
               Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
        }
    }
}
