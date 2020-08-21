using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] LayerMask wallLayers = new LayerMask();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected bool WallCheck()
    {
        var targetPosition = PlayerManager.GetManager.GetPlObj.transform.position;
        var desiredPosition = transform.position;
        RaycastHit wallHit = new RaycastHit();
        Vector3 wallHitPosition;


        if (Physics.Raycast(targetPosition, desiredPosition - targetPosition, out wallHit, Vector3.Distance(targetPosition, desiredPosition), wallLayers, QueryTriggerInteraction.Ignore))
        {
            wallHitPosition = wallHit.point;
            transform.position = wallHitPosition;
            return true;
        }
        else
        {
            return false;
        }
    }
}
