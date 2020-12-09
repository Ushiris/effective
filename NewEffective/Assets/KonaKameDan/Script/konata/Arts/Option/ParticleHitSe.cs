﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHitSe : MonoBehaviour
{
    [SerializeField] SE_Manager.SE_NAME seName;

    public bool isPlayerAndEnemyHit = false;
    public AudioSource GetSe => se;

    AudioSource se;
    List<string> hitTagName = new List<string>()
    {
        "Default","Map","Ground","PostProcessing"
    };

    private void Start()
    {
        if (!isPlayerAndEnemyHit) return;
        hitTagName.Add("Player");
        hitTagName.Add("Enemy");
    }

    /// <summary>
    /// 鳴らすSEを決める
    /// </summary>
    /// <param name="name"></param>
    public void SetSeName(SE_Manager.SE_NAME name)
    {
        seName = name;
    }

    /// <summary>
    /// ヒットするTagを追加する
    /// </summary>
    /// <param name="name"></param>
    public void AddHitTagName(string name)
    {
        hitTagName.Add(name);
    }

    void OnParticleCollision(GameObject obj)
    {
        if (hitTagName.Contains(obj.tag))
        {
            se = SE_Manager.SePlay(seName);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (hitTagName.Contains(other.gameObject.tag))
        {
            se = SE_Manager.SePlay(seName);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hitTagName.Contains(collision.gameObject.tag))
        {
            se = SE_Manager.SePlay(seName);
        }
    }
}
