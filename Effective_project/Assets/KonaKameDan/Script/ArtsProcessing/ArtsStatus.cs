using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtsStatus : MonoBehaviour
{
    public enum ParticleType { Player,Enemy}

    /// <summary>
    /// 放った人が誰であるか
    /// </summary>
    public ParticleType type;

    /// <summary>
    /// 放った人のステータス
    /// </summary>
    public Status myStatus;

    /// <summary>
    /// 放った人のエフェクト所持数
    /// </summary>
    public MyEffectCount myEffectCount;
}
