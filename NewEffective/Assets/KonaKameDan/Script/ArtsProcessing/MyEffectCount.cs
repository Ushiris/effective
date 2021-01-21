using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所持しているエフェクトの格納
/// </summary>
public class MyEffectCount : MonoBehaviour
{
    public Dictionary<NameDefinition.EffectName, int> effectCount = new Dictionary<NameDefinition.EffectName, int>()
    {
        {NameDefinition.EffectName.Shot,        0 },
        {NameDefinition.EffectName.Slash,       0 },
        {NameDefinition.EffectName.Barrier,     0 },
        {NameDefinition.EffectName.Trap,        0 },
        {NameDefinition.EffectName.Spread,      0 },
        {NameDefinition.EffectName.Homing,      0 },
        {NameDefinition.EffectName.Drain,       0 },
        {NameDefinition.EffectName.Explosion,   0 },
        {NameDefinition.EffectName.Slow,        0 },
        {NameDefinition.EffectName.Fly,         0 },
    };

    public void DataReset()
    {
        effectCount = new Dictionary<NameDefinition.EffectName, int>()
        {
            {NameDefinition.EffectName.Shot,        0 },
            {NameDefinition.EffectName.Slash,       0 },
            {NameDefinition.EffectName.Barrier,     0 },
            {NameDefinition.EffectName.Trap,        0 },
            {NameDefinition.EffectName.Spread,      0 },
            {NameDefinition.EffectName.Homing,      0 },
            {NameDefinition.EffectName.Drain,       0 },
            {NameDefinition.EffectName.Explosion,   0 },
            {NameDefinition.EffectName.Slow,        0 },
            {NameDefinition.EffectName.Fly,         0 },
        };
    }

}
