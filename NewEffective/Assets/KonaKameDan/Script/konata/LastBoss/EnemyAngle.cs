using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAngle : MonoBehaviour
{
    public enum Angle
    {
        Player, Center, Front, Idol
    }

    [SerializeField] Transform centerPos;
    [SerializeField] Transform playerPos;

    [SerializeField] float speed=40;

    [SerializeField] Angle angleType;

    // Update is called once per frame
    void Update()
    {
        var roll = transform.rotation;

        switch (angleType)
        {
            case Angle.Player:
                roll = Look(transform, playerPos, speed);
                break;

            case Angle.Center:
                roll = Look(transform, centerPos, speed);
                break;

            case Angle.Front:
                
                break;

            case Angle.Idol:
                break;

            default: break;
        }

        transform.rotation = roll;
    }

    Quaternion Look(Transform a, Transform b, float speed)
    {
        var aim = a.position - b.position;
        var look = Quaternion.LookRotation(aim);
        look *= Quaternion.AngleAxis(180, Vector3.up);
        return Quaternion.RotateTowards(transform.rotation, look, speed * Time.deltaTime);
    }

    public void ForcedRotation(Transform target, float angle)
    {
        var aim = transform.position - target.position;
        var look = Quaternion.LookRotation(aim);
        look *= Quaternion.AngleAxis(180, Vector3.up);
        transform.rotation = look * Quaternion.AngleAxis(angle, Vector3.up);
    }

    public void SetAngleType(Angle type, float speed)
    {
        this.speed = speed;
        angleType = type;
    }

    public void SetPos(Transform player, Transform center)
    {
        playerPos = player;
        centerPos = center;
    }
}
