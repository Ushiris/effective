using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Payer_HPber : MonoBehaviour
{
    private float maxHP;
    [SerializeField] private Status playerStatus;
    private int tmpPlayerLv;
    public Slider berHP;
    void Start()
    {
        GameObject playerStatusObject = GameObject.FindWithTag("Player");
        playerStatus = playerStatusObject.GetComponent<Status>();
        StatusUpdate();
    }
    void Update()
    {
        if(tmpPlayerLv != playerStatus.Lv)
            StatusUpdate();
    }
    public void StatusUpdate()
    {
        maxHP = playerStatus.status[Status.Name.HP];
        berHP.maxValue = maxHP;// HPバーマックス
        berHP.value = maxHP;// HP回復
        tmpPlayerLv = playerStatus.Lv;
    }
}
