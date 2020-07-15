﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtsStatus : MonoBehaviour
{
    public enum ParticleType { Player,Enemy}
    public enum ArtsType { Slash, Shot, Support }

    /// <summary>
    /// 放った人が誰であるか
    /// </summary>
    [HideInInspector] public ParticleType type;

    /// <summary>
    /// 放った人のステータス
    /// </summary>
    [HideInInspector] public Status myStatus;

    /// <summary>
    /// 放った人のエフェクト所持数
    /// </summary>
    [HideInInspector] public MyEffectCount myEffectCount;

    /// <summary>
    /// アーツが長距離か接近かサポートかのタイプ決め
    /// </summary>
    [HideInInspector] public ArtsType artsType = ArtsType.Shot;

    /// <summary>
    /// 親情報の格納
    /// </summary>
    [HideInInspector] public GameObject myObj;
}