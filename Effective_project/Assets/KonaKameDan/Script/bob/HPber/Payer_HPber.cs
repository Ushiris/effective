using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Payer_HPber : MonoBehaviour
{
    private float maxHP;
    [SerializeField] private Status playerStatus;
    public Slider berHP;
    void Start()
    {
        GameObject playerStatusObject = GameObject.FindWithTag("Player");
        playerStatus = playerStatusObject.GetComponent<Status>();
        StatusUpdate();
    }
    public void StatusUpdate()
    {
        maxHP = playerStatus.status[Status.Name.HP];
        berHP.maxValue = maxHP;// HPバーマックス
        berHP.value = maxHP;// HP回復
    }
}
