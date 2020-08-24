using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラとプレイヤーの向きを制御
/// </summary>
public class TpsPlayerControl : MonoBehaviour
{
    [SerializeField] GameObject cameraPivot;
    [SerializeField] GameObject pl;
    [SerializeField] GameObject cameraObj;
    [SerializeField] LayerMask wallLayers;
    [SerializeField] float distance = 1.5f;

    Vector3 targetPosition, desiredPosition, wallHitPosition = Vector3.zero;
    RaycastHit wallHit;

    // Update is called once per frame
    void Update()
    {
        float X_Rotation = Input.GetAxis("Mouse X") * PlayerManager.GetManager.mouseSensitivity;
        float Y_Rotation = Input.GetAxis("Mouse Y") * PlayerManager.GetManager.mouseSensitivity;
        pl.transform.Rotate(0, X_Rotation, 0);
        cameraPivot.transform.Rotate(-Y_Rotation, 0, 0);

        var dist = Vector3.Distance(cameraObj.transform.position, pl.transform.position);
        if (dist != distance)
        {
            var diff = cameraObj.transform.position - pl.transform.position;
            cameraObj.transform.position = diff * (distance / dist);
        }

        desiredPosition = -cameraObj.transform.position;

        cameraObj.transform.position = WallCheck() ? wallHitPosition : desiredPosition;
    }

    protected bool WallCheck()
    {
        targetPosition = pl.transform.position;
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
