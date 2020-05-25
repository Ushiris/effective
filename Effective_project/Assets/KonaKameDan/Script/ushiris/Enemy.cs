using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    Life life;
    Slider slider;

    public int maxHP = 10;
    public int defHP = 10;

    private void Start()
    {
        life = gameObject.AddComponent<Life>();
        slider = GetComponentInChildren<Slider>();

        slider.minValue = 0;
        slider.maxValue = maxHP;
        slider.value = maxHP;

        if (life.LifeSetup(maxHP, defHP, 1)) Debug.Log("Error init HP");
        life.AddLastword(Dead);
        life.AddDamageFunc(Damage);
        life.AddHealFunc(Heal);
    }

    void Dead()
    {
        Debug.Log("destroy:" + name);
        Destroy(gameObject);
    }

    int Damage(int true_damage)
    {
        slider.value -= true_damage;
        return true_damage;
    }

    int Heal(int true_heal)
    {
        slider.value += true_heal;
        return true_heal;
    }
}