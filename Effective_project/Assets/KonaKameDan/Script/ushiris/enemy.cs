using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    Life life;
    Slider slider;

    private void Start()
    {
        life = gameObject.AddComponent<Life>();
        life.AddLastword(Dead);

        slider = GetComponentInChildren<Slider>();
        life.AddDamageFunc(Damage);
    }

    void Dead()
    {
        Destroy(gameObject);
    }

    int Damage(int true_damage)
    {
        slider.value -= life.HP / life.MaxHP;
        return true_damage;
    }
}