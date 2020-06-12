using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーが一定距離まで近づいたらオブジェクトをアクティブにする
/// </summary>
public class DisPopUp : MonoBehaviour
{
    [SerializeField] float dis;
    [SerializeField] GameObject my;

    GameObject target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float f = Vector3.Distance(transform.position, PlayerManager.GetManager.GetPlObj.transform.position);

        if (f < dis)
        {
            if (!my.activeSelf) my.SetActive(true);
        }
        else
        {
            if (my.activeSelf) my.SetActive(false);
        }
    }
}
