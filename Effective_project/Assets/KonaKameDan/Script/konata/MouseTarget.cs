using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マウスに触れたオブジェクトのところまで飛んで行く
/// </summary>
public class MouseTarget : MonoBehaviour
{
    public static Transform ms;
    public float speed = 1f;
    Vector3 mouseWorldPosition;

    // Start is called before the first frame update
    void Start()
    {
        ms = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            mouseWorldPosition = hit.point;
        }

        Quaternion toRotation = Quaternion.LookRotation(mouseWorldPosition - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);
        ms.position = mouseWorldPosition;
        
    }

    
}
