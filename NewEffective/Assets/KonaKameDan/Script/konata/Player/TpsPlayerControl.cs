using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// カメラとプレイヤーの向きを制御
/// </summary>
public class TpsPlayerControl : MonoBehaviour
{
    [SerializeField] GameObject pl;
    [SerializeField] GameObject cameraPivot;
    [SerializeField] GameObject cameraObj;
    [SerializeField] GameObject cameraDesirePos;
    [SerializeField] GameObject head;
    [SerializeField] LayerMask wallLayers;
    [SerializeField] float distance = 1.5f;

    Vector2 mouseDelta = Vector2.zero;

    Vector3 targetPosition, desiredPosition, wallHitPosition = Vector3.zero;
    RaycastHit wallHit;


    Terrain t = NewMapTerrainData.GetTerrain;

    private void Start()
    {
        var pl_look = new Vector3(0, pl.transform.position.y,0);
        pl.transform.LookAt(pl_look);

        cameraObj.transform.localPosition = new Vector3(0, Mathf.Sqrt(2), -Mathf.Sqrt(2)) * distance;
        cameraDesirePos.transform.position = cameraObj.transform.position;
        cameraObj.transform.LookAt(head.transform);
        cameraDesirePos.transform.LookAt(head.transform);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mouseDelta.x = Input.GetAxis("Mouse X") * PlayerManager.GetManager.mouseSensitivity;
        mouseDelta.y = Input.GetAxis("Mouse Y") * PlayerManager.GetManager.mouseSensitivity;
        
        pl.transform.Rotate(0, mouseDelta.x, 0);

        desiredPosition = cameraDesirePos.transform.position;
        bool isBlock = WallCheck();
        if (desiredPosition.y <= head.transform.position.y - distance + 0.1f && mouseDelta.y >= 0)
        {
            mouseDelta.y = 0;
        }
        if (desiredPosition.y >= head.transform.position.y + distance - 0.1f && mouseDelta.y <= 0)
        {
            mouseDelta.y = 0;
        }
        cameraPivot.transform.Rotate(-mouseDelta.y, 0, 0);

        cameraObj.transform.position = isBlock ? wallHitPosition : desiredPosition;
        cameraObj.transform.LookAt(head.transform);
    }

    protected bool WallCheck()
    {
        targetPosition = head.transform.position;
        if (Physics.Raycast(targetPosition, desiredPosition - targetPosition, out wallHit, Vector3.Distance(targetPosition, desiredPosition), wallLayers, QueryTriggerInteraction.Ignore))
        {
            wallHitPosition = wallHit.point - (desiredPosition - wallHit.point) * 0.2f;
            return true;
        }
        else
        {
            return false;
        }
    }
}
