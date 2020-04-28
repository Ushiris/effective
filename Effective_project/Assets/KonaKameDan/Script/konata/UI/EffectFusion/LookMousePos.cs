using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI上でマウスの方向を見つめる
/// </summary>
public class LookMousePos : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //マウスの方向を見る
        transform.rotation =
            Quaternion.LookRotation(Vector3.forward, Input.mousePosition - GetComponent<RectTransform>().position);
    }
}
