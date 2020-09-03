using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラとプレイヤーの向きを制御
/// </summary>
public class TpsPlayerControl : MonoBehaviour
{
    [SerializeField] GameObject pl;
    [SerializeField] GameObject cameraObj;
    [SerializeField] GameObject head;
    [SerializeField] LayerMask wallLayers;
    [SerializeField] float distance = 1.5f;

    Vector2 mousePos = Vector2.zero;

    Vector3 targetPosition, desiredPosition, wallHitPosition = Vector3.zero;
    RaycastHit wallHit;

    // Update is called once per frame
    void Update()
    {
        mousePos.x += Input.GetAxis("Mouse X") * PlayerManager.GetManager.mouseSensitivity;
        mousePos.y += Input.GetAxis("Mouse Y") * PlayerManager.GetManager.mouseSensitivity;
        if (mousePos.x >= 180) { mousePos.x -= 360; }
        if (mousePos.y >= 180) { mousePos.y -= 360; }
        if (mousePos.x <= -180) { mousePos.x += 360; }
        if (mousePos.y <= -180) { mousePos.y += 360; }

        Quaternion qua = Quaternion.Euler(0, mousePos.x, 0);

        float X_Rotation = Input.GetAxis("Mouse X") * PlayerManager.GetManager.mouseSensitivity;
        float Y_Rotation = Input.GetAxis("Mouse Y") * PlayerManager.GetManager.mouseSensitivity;
        pl.transform.SetPositionAndRotation(pl.transform.position, qua);
        cameraObj.transform.rotation = Quaternion.Euler(0, 0, 0);
        cameraObj.transform.RotateAround(head.transform.position, new Vector3(1, 0, 0), -mousePos.y);

        var dist = Vector3.Distance(cameraObj.transform.position, head.transform.position);
        if ((dist <= distance - 0.1 || dist >= distance + 0.1) && dist != distance)
        {
            var diff = cameraObj.transform.position - head.transform.position;
            cameraObj.transform.position = diff * ((distance / (dist + 0.01f)));
        }

        desiredPosition = cameraObj.transform.position;

        if (WallCheck())
        {
            cameraObj.transform.position = wallHitPosition;
            cameraObj.transform.LookAt(head.transform);
        }
        else
        {
            cameraObj.transform.position = desiredPosition;
            cameraObj.transform.LookAt(head.transform);
        }

    }

    protected bool WallCheck()
    {
        targetPosition = head.transform.position;
        if (Physics.Raycast(targetPosition, desiredPosition - targetPosition, out wallHit, Vector3.Distance(targetPosition, desiredPosition), wallLayers, QueryTriggerInteraction.Ignore))
        {
            wallHitPosition = wallHit.point;
            return true;
        }
        else
        {
            return false;
        }
    }
}
