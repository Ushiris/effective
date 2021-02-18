using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusUp : MonoBehaviour
{
    [SerializeField] Status status;

    [SerializeField] float plusSpeed = 10;
    [SerializeField] int plusSpeedReachCount = 50;

    [SerializeField] int plusHP = 5000;
    [SerializeField] int plusHpReachCount = 50;

    [SerializeField] int plusSTR = 100;
    [SerializeField] int plusSTRReachCount = 50;

    float speed;
    int hp;
    int str;

    static int redCount;
    static int blueCount;
    static int greenCount;

    // Start is called before the first frame update
    void Start()
    {
        speed = plusSpeed / plusSpeedReachCount;
        hp = plusHP / plusHpReachCount;
        str = plusSTR / plusSTRReachCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsLimit()) return;

        var obj = other.gameObject;
        var effectId = obj.GetComponent<EffectObjectID>();

        if (effectId == null) return;

        var effectColor = NameDefinition.GetEffectColor(effectId.effectObjectType);

        switch (effectColor)
        {
            case NameDefinition.EffectColor.Red:
                if (redCount >= plusSTRReachCount) return;
                redCount++;
                status.SetStatusEssence(Status.Name.STR, str * redCount);
                DebugLogger.Log("RedEffect:" + redCount + " plussSTR:" + redCount * str);
                break;

            case NameDefinition.EffectColor.Blue:
                if (blueCount >= plusSpeedReachCount) return;
                blueCount++;
                status.SetMoveSpeed(speed * blueCount);
                DebugLogger.Log("BlueEffect:" + blueCount + " plusSpeed" + blueCount * speed);
                break;

            case NameDefinition.EffectColor.Green:
                if (greenCount >= plusHpReachCount) return;
                greenCount++;
                status.SetStatusEssence(Status.Name.HP, hp + greenCount);
                DebugLogger.Log("GreenEffect:" + greenCount + " plusHP" + greenCount * hp);
                break;

            case NameDefinition.EffectColor.Nothing: break;
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
