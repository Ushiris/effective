using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public Life life;
    [HideInInspector] public Slider slider;
    [SerializeField] GameObject bullet;
    public bool isBoss;
    static Vector3 hp_small = new Vector3(1, 1, 1);
    static Vector3 big = new Vector3(3, 2, 1);

    MyEffectCount bag;

    private void Start()
    {
        life = gameObject.AddComponent<Life>();
        slider = GetComponentInChildren<Slider>();
        if (GetComponent<MyEffectCount>() == null) bag = gameObject.AddComponent<MyEffectCount>();
        else bag = gameObject.GetComponent<MyEffectCount>();

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
        life.AddLastword(DropEffect);
        life.AddLastword(Dead);
        life.AddDamageFunc(Damage);
        life.AddHealFunc(Heal);
        life.AddBeat(Attack);
    }

    void Attack()
    {
        Vector3 pointB = GameObject.FindGameObjectWithTag("Player").transform.position;
        float angle = 45;
        Vector3 velocity;

        // 射出角をラジアンに変換
        float rad = angle * Mathf.PI / 180;

        // 水平方向の距離x
        float x = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(pointB.x, pointB.z));

        // 垂直方向の距離y
        float y = transform.position.y - pointB.y;

        // 斜方投射の公式を初速度について解く
        float speed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(x, 2) / (2 * Mathf.Pow(Mathf.Cos(rad), 2) * (x * Mathf.Tan(rad) + y)));

        if (float.IsNaN(speed))
        {
            // 条件を満たす初速を算出できなければVector3.zeroを返す
            velocity= Vector3.zero;
        }
        else
        {
            velocity= (new Vector3(pointB.x - transform.position.x, x * Mathf.Tan(rad), pointB.z - transform.position.z).normalized * speed);
        }

        if (velocity == Vector3.zero) return;

        GameObject bullet_i = Instantiate(bullet);
        Transform bullet_tr = bullet_i.transform;
        bullet_tr.position = transform.position;

        bullet_i.GetComponent<Rigidbody>().velocity = Vector3.zero;
        
        bullet_i.GetComponent<Rigidbody>().AddForce(velocity * bullet_i.GetComponent<Rigidbody>().mass, ForceMode.Impulse);
    }

    void DropEffect()
    {
        var EffectList = new List<NameDefinition.EffectName>();
        foreach (var item in bag.effectCount)
        {
            for (int i = 0; i < item.Value; i++) EffectList.Add(item.Key);
        }
        var rand = Random.Range(0, EffectList.Count);
        var name = EffectList[rand];

        var effect = Instantiate(Resources.Load("EffectObj/[" + name.ToString() + "]EffectObject")) as GameObject;
        effect.transform.position = gameObject.transform.position;
    }

    void Dead()
    {
        Debug.Log("dead:" + name);
        Destroy(gameObject);
    }

    void Damage(int true_damage)
    {
        slider.value -= true_damage;
    }

    void Heal(int true_heal)
    {
        slider.value += true_heal;
    }
}