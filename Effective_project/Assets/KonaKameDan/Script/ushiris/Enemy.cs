using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public Life life;
    [HideInInspector] public Slider slider;
    [SerializeField] GameObject bullet;
    [SerializeField] bool isBoss;
    static Vector3 hp_small=new Vector3(1,1,1);
    static Vector3 big=new Vector3(3,2,1);

    private void Start()
    {
        life = gameObject.AddComponent<Life>();
        slider = GetComponentInChildren<Slider>();

        //sliderの初期化
        slider.minValue = 0;
        slider.maxValue = life.MaxHP;
        slider.value = life.MaxHP;

        if (isBoss)
        {
            slider.GetComponentInParent<Canvas>().transform.localScale = big;
            GameObject Territory = new GameObject();
            Territory.transform.parent = gameObject.transform;
            Territory.AddComponent<TerritorySenses>();
        }

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
        bullet_tr.LookAt(GameObject.Find("Player") == null ? transform : GameObject.Find("Player").transform);
        bullet_i.GetComponent<Rigidbody>().AddForce(Vector3.up * 500);
    }

    void Dead()
    {
        Debug.Log("dead:" + name);
        Destroy(gameObject);
    }

    public int Damage(int true_damage)
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