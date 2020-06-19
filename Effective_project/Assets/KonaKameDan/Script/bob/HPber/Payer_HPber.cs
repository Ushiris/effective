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
        if(tmpPlayerLv != playerStatus.Lv)// レベル上がった！
            StatusUpdate();

        berHP.value = playerLife.getHitPointSafety();// ダメージくらった！
    }
    public void StatusUpdate()
    {
        maxHP = playerStatus.status[Status.Name.HP];
        berHP.maxValue = maxHP;// HPバーマックス
        berHP.value = maxHP;// HP回復
        tmpPlayerLv = playerStatus.Lv;
        tmpPlayerHP = maxHP;
    }
}
