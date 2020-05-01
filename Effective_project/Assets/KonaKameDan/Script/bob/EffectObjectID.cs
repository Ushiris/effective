using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EffectObjectID : MonoBehaviour
{
    public enum EffectObjectType
    {
        RED,
        BLUE,
        YELLOW
    }
    public static Dictionary<EffectObjectType, string> effectDictionary = new Dictionary<EffectObjectType, string>() {
        { EffectObjectType.BLUE,"青"},
        { EffectObjectType.RED,"赤"},
        {EffectObjectType.YELLOW,"黄" }
    };

    public EffectObjectType effectObjectType;
}
