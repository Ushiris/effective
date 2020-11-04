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
    public static Dictionary<NameDefinition.EffectName, string> effectDictionary = new Dictionary<NameDefinition.EffectName, string>() {
        {NameDefinition.EffectName.Shot,        "射撃" },
        {NameDefinition.EffectName.Slash,       "斬撃" },
        {NameDefinition.EffectName.Barrier,     "防御" },
        {NameDefinition.EffectName.Trap,        "設置" },
        {NameDefinition.EffectName.Spread,      "拡散" },
        {NameDefinition.EffectName.Homing,      "追尾" },
        {NameDefinition.EffectName.Drain,       "吸収" },
        {NameDefinition.EffectName.Explosion,   "爆発" },
        {NameDefinition.EffectName.Slow,        "遅延" },
        {NameDefinition.EffectName.Fly,         "飛翔" }
    };

    public NameDefinition.EffectName effectObjectType;
}
