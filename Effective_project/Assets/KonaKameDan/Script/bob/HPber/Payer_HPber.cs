using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Payer_HPber : MonoBehaviour
{
    private float maxHP;
    [SerializeField] private Status playerStatus;
    [SerializeField] private Life playerLife;
    private int tmpPlayerLv;
    private float tmpPlayerHP;
    public Slider berHP;
    void Start()
    {
        GameObject playerStatusObject = GameObject.FindWithTag("Player");
        playerStatus = playerStatusObject.GetComponent<Status>();
        playerLife = playerStatusObject.GetComponent<Life>();
        StatusUpdate();
    }
    void Update()
    {
        if(tmpPlayerLv != playerStatus.Lv)
            StatusUpdate();

        if (playerLife.HP != null)
            if (tmpPlayerHP != playerLife.HP)
                DamageUpdate();
    }
    public void StatusUpdate()
    {
        maxHP = playerStatus.status[Status.Name.HP];
        berHP.maxValue = maxHP;// HPバーマックス
        berHP.value = maxHP;// HP回復
        tmpPlayerLv = playerStatus.Lv;
        tmpPlayerHP = maxHP;
    }
    public void DamageUpdate()
    {
        maxHP = playerStatus.status[Status.Name.HP];
        berHP.value = (float)playerLife.HP+ maxHP;// ダメージくらってHP減少
        tmpPlayerHP = (float)playerLife.HP;
        //getHitpointHp
    }
}
