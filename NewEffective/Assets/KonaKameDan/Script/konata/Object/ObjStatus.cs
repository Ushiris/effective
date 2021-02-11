﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// オブジェクトのステータスクラス
/// 継承して使うこと
/// </summary>
public class ObjStatus : MonoBehaviour
{
    [SerializeField] GameObject model;
    [SerializeField] Slider hpBar;
    [SerializeField] int maxHp;

    [SerializeField] GameObject breakParticle;

    int tmpHp;
    Life life;

    List<Rigidbody> effectArr = new List<Rigidbody>();
    new ParticleSystem particleSystem;
    new Collider collider;
    MeshRenderer mesh = new MeshRenderer();

    /// <summary>
    /// ゲットコンポーネントができないので、疑似Start関数
    /// </summary>
    /// <param name="my"></param>
    protected void SetUp(GameObject my)
    {
        life = my.GetComponent<Life>();
        life.HP = maxHp;
        hpBar.maxValue = maxHp;
        hpBar.value = maxHp;

        collider = my.GetComponent<Collider>();
        mesh = model.GetComponent<MeshRenderer>();
    }

    /// <summary>
    /// ライフからHPバーを操作
    /// </summary>
    protected void SetHpBarControl()
    {
        hpBar.value = life.HP ?? 0f;
    }

    /// <summary>
    /// HPがゼロかどうか
    /// </summary>
    /// <returns></returns>
    protected bool IsDeath()
    {
        if (life.HP <= 0) return true;
        else return false;
    }

    /// <summary>
    /// ドロップするEffectを設定
    /// </summary>    
    protected void SetEffect(int maxEffectCount)
    {
        for (int i = 0; i < maxEffectCount; i++)
        {
            var ran = Random.Range(0, NewMap.GetEffect.Count);
            var obj = Instantiate(NewMap.GetEffect[ran], transform);

            var rb = obj.GetComponent<Rigidbody>();
            effectArr.Add(rb);

            obj.SetActive(false);
        }
    }

    /// <summary>
    /// ドロップしたいアイテムをセットします
    /// </summary>
    /// <param name="item"></param>
    protected void SetSelfDropItem(GameObject[] item)
    {
        for (int i = 0; i < item.Length; i++)
        {
            var obj = Instantiate(item[i], transform);

            var rb = obj.GetComponent<Rigidbody>();
            effectArr.Add(rb);

            obj.SetActive(false);
        }
    }

    /// <summary>
    /// エフェクトオブジェクトをばらまく
    /// </summary>
    /// <param name="range"></param>
    protected void PlayEffectPurge(float range = 10)
    {
        for (int i = 0; i < effectArr.Count; i++)
        {
            effectArr[i].gameObject.SetActive(true);
            effectArr[i].transform.parent = null;
            effectArr[i].AddRelativeForce(Random.onUnitSphere * range, ForceMode.VelocityChange);
        }
    }

    /// <summary>
    /// 実体を表示するかどうか
    /// </summary>
    /// <param name="isActive"></param>
    protected void SetEntityEnable(bool isActive)
    {
        hpBar.gameObject.SetActive(isActive);
        collider.enabled = isActive;
        mesh.enabled = isActive;
    }

    /// <summary>
    /// オブジェクトが破壊されたときに出すParticle
    /// </summary>
    protected void PlayBreakParticle()
    {
        particleSystem.gameObject.transform.parent = null;
        particleSystem.Play();
    }

    /// <summary>
    /// オブジェクトが破壊されたときにParticleを出したいならばこれを宣言する必要がある
    /// </summary>
    protected void SetBreakParticle()
    {
        var particle = Instantiate(breakParticle, transform);
        particle.transform.position += new Vector3(0, 1.5f, 0);
        particleSystem = particle.GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// 無敵にするかどうか
    /// </summary>  
    protected void SetInvincible(bool isActive)
    {
        hpBar.enabled = isActive ? false : true;
        life.damageGuard = isActive;
    }
}
