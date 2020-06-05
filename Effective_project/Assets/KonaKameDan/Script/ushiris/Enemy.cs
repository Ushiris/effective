using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    Life life;
    Slider slider;
    [SerializeField] GameObject bullet;

    private void Start()
    {
        life = gameObject.AddComponent<Life>();
        slider = GetComponentInChildren<Slider>();

        //sliderの初期化
        slider.minValue = 0;
        slider.maxValue = life.MaxHP;
        slider.value = life.MaxHP;

        //Lifeの初期化
        if (life.LifeSetup(1)) Debug.Log("Error init HP");
        life.AddLastword(Dead);
        life.AddDamageFunc(Damage);
        life.AddHealFunc(Heal);
        life.AddBeat(Attack);
    }

    void Attack()
    {
        GameObject bullet_i = Instantiate(bullet);
        Transform bullet_tr = bullet_i.transform;

        bullet_tr.parent = transform;
        bullet_tr.localPosition = Vector3.zero;
        bullet_tr.LookAt(GameObject.Find("Player").transform);
        bullet_i.GetComponent<Rigidbody>().AddForce(Vector3.up * 500);
    }

    void Dead()
    {
        Debug.Log("dead:" + name);
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