using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBot : MonoBehaviour
{
    [SerializeField] public Vector3 muzzleAngleLimit = new Vector3(30, 60, 60);
    Vector3 PrevTargetPos;
    StopWatch timer;

    private void Start()
    {
        timer = StopWatch.Summon(EnemyProperty.EnemyAimSpeed, StopWatch.voidAction, gameObject);
        timer.LapEvent = () => { timer.SetActive(false); timer.LapTimer = EnemyProperty.EnemyAimSpeed; };
    }

    public void MuzzleLookAt(Vector3 target)
    {
        if (target != PrevTargetPos)
        {
            timer.ResetTimer();
            timer.SetActive(true);
        }
        gameObject.transform.LookAt(AimDelay(target));
        MuzzleLimitter();
        PrevTargetPos = target;
    }

    void MuzzleLimitter()
    {
        Vector3 angle = gameObject.transform.localEulerAngles;
        if (angle.y > muzzleAngleLimit.y)
        {
            float rotate = muzzleAngleLimit.y - angle.y;
            gameObject.transform.Rotate(Vector3.up, rotate, Space.Self);
        }
        else if (angle.y < -muzzleAngleLimit.y)
        {
            float rotate = -muzzleAngleLimit.y - angle.y;
            gameObject.transform.Rotate(Vector3.up, rotate, Space.Self);
        }

        if (angle.x > muzzleAngleLimit.x)
        {
            float rotate = muzzleAngleLimit.x - angle.x;
            gameObject.transform.Rotate(Vector3.up, rotate, Space.Self);
        }
        else if (angle.x < -muzzleAngleLimit.x)
        {
            float rotate = -muzzleAngleLimit.x - angle.x;
            gameObject.transform.Rotate(Vector3.up, rotate, Space.Self);
        }
    }

    //追いエイムの遅れを発生させ、いわゆる"手加減"されたエイム先を返す。
    private Vector3 AimDelay(Vector3 target)
    {
        Vector3 looking = gameObject.transform.forward * Vector3.Distance(target, gameObject.transform.position);
        return Vector3.Lerp(looking, target, timer.LapTimer);
    }
}
