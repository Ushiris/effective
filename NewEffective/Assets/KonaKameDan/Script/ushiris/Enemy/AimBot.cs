using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBot : MonoBehaviour
{
    public Vector3 muzzleAngleLimit = new Vector3(30, 60, 60);

    public void Aim()
    {
        transform.LookAt(PlayerManager.GetManager.aimPoint.AimPoint());
        MuzzleLimitter();
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
}