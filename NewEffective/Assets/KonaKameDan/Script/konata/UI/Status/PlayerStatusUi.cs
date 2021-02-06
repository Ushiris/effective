using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUi : MonoBehaviour
{
    [SerializeField] Text lvText;
    [SerializeField] Text speedText;
    [SerializeField] Text strText;
    [SerializeField] Text expText;

    int tmpLv = 0;
    Status status;

    static readonly string klv = "Lv: ";
    static readonly string kSpeed = "Speed: ";
    static readonly string kStr = "STR: ";
    static readonly string kExp = "EXP: ";

    private void Start()
    {
        status = NewMap.GetPlayerObj.GetComponent<Status>();
    }

    private void Update()
    {
        tmpLv = status.Lv;
        lvText.text = string.Format("{0}{1}", klv, tmpLv);
        speedText.text = string.Format("{0}{1}", kSpeed, status.GetMoveSpeed);
        strText.text = string.Format("{0}{1}", kStr, Mathf.CeilToInt(status.status[Status.Name.STR]));
        expText.text = string.Format("{0}{1}/{2}", kExp, status.GetExpInt, status.nextExp);
    }
}
