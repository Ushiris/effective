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
        life.AddLastword(() => { EnemySpawn.EnemyCount--; });
        life.AddLastword(Dead);
        life.AddDamageFunc(Damage);
        life.AddHealFunc(Heal);
    }

    //オブジェクトが破棄された時
    private void OnDestroy()
    {
        var playerStatus= PlayerManager.GetManager.GetPlObj.GetComponent<Status>();
        var enemyStatus = GetComponent<Status>();

        //プレイヤーに経験知を渡す
        if (playerStatus != null && enemyStatus != null)
        {
            playerStatus.EXP += enemyStatus.status[Status.Name.DROP_EXP];
        }
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