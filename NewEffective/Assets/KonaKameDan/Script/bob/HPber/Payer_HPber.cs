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
    public Slider berHP;
    public Slider berEXP;
    public Text lv_text;
    public Text str_text;
    public Text exp_text;
    public Image[] speed_image;
    static readonly string klv = "Lv:";
    static readonly string kStr = "STR:";
    static readonly string kExp = "EXP:";
    float tmpPlayerSpeed;
    void Start()
    {
        GameObject playerStatusObject = GameObject.FindWithTag("Player");
        playerStatus = playerStatusObject.GetComponent<Status>();
        playerLife = playerStatusObject.GetComponent<Life>();
        StatusUpdate();
        for (int i = 0; i < speed_image.Length; i++)
            speed_image[i].gameObject.SetActive(false);
        tmpPlayerSpeed = playerStatus.GetMoveSpeed;
    }
    void Update()
    {
        if(tmpPlayerLv != playerStatus.Lv)// レベル上がった！
            StatusUpdate();

        berHP.value = playerLife.GetHitPointSafety();// ダメージくらった！
        berEXP.value = playerStatus.GetExpInt;// 経験値を得た！

        lv_text.text = string.Format("{0}{1}", klv, tmpPlayerLv);// レベル表記
        str_text.text = string.Format("{0}{1}", kStr, Mathf.CeilToInt(playerStatus.status[Status.Name.STR]));// STR表記
        exp_text.text = string.Format("{0}{1}/{2}", kExp, playerStatus.GetExpInt, playerStatus.nextExp);// 経験値表記

        if (speed_image[Mathf.CeilToInt(playerStatus.GetMoveSpeed) - Mathf.CeilToInt(tmpPlayerSpeed)].gameObject.activeSelf == false)// Speed表記
        {
            for(int i = 0; i <= Mathf.CeilToInt(playerStatus.GetMoveSpeed) - Mathf.CeilToInt(tmpPlayerSpeed); i++)
                speed_image[i].gameObject.SetActive(true);
        }
    }
    public void StatusUpdate()
    {
        maxHP = playerStatus.status[Status.Name.HP];
        berHP.maxValue = maxHP;// HPバーマックス
        tmpPlayerLv = playerStatus.Lv;
        berEXP.maxValue = playerStatus.nextExp;// EXPバーマックス
        //Debug.Log("あいうえおplayerStatus.nextExp : " + playerStatus.nextExp);
    }
}
