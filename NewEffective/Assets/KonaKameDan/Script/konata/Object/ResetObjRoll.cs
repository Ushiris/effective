using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 向きをリセットする
/// </summary>
public class ResetObjRoll : MonoBehaviour
{

    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

}
