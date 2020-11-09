using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusUp : MonoBehaviour
{
    [SerializeField] Status status;

    [SerializeField] float plusSpeed = 10;
    [SerializeField] int plusSpeedReachCount = 10;

    [SerializeField] int plusHP = 5000;
    [SerializeField] int plusHpReachCount = 10;

    [SerializeField] int plusSTR = 100;
    [SerializeField] int plusSTRReachCount = 10;

    float speed;
    int hp;
    int str;

    int redCount;
    int blueCount;
    int greenCount;

    // Start is called before the first frame update
    void Start()
    {
        speed = plusSpeed / plusSpeedReachCount;
        hp = plusHP / plusHpReachCount;
        str = plusSTR / plusSTRReachCount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsLimit()) return;

        var obj = collision.gameObject;
        var effectId = obj.GetComponent<EffectObjectID>();

        if (effectId == null) return;

        var effectColor = NameDefinition.GetEffectColor(effectId.effectObjectType);

        switch (effectColor)
        {
            case NameDefinition.EffectColor.Red: 
                if (redCount >= plusSTRReachCount) return;
                redCount++;
                status.SetPlusStatus(Status.Name.STR, str);
                DebugLogger.Log("RedEffect:" + redCount + " plussSTR:" + redCount * str);
                break;

            case NameDefinition.EffectColor.Blue:
                if (blueCount >= plusSpeedReachCount) return;
                blueCount++;
                status.SetPlusSpeed(speed);
                DebugLogger.Log("RedEffect:" + blueCount + " plusSpeed" + blueCount * speed);
                break;

            case NameDefinition.EffectColor.Green:
                if (greenCount >= plusHpReachCount) return;
                greenCount++;
                status.SetPlusStatus(Status.Name.HP, hp);
                DebugLogger.Log("RedEffect:" + greenCount + " plusHP" + greenCount * hp);
                break;

            case NameDefinition.EffectColor.Nothing:break;
            default: break;
        }
    }

    //全てが限界値を超えたらtrue
    bool IsLimit()
    {
        return redCount >= plusSTRReachCount &&
            blueCount >= plusSpeedReachCount &&
            greenCount >= plusHpReachCount;
    }
}
