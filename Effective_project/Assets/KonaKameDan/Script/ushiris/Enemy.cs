using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Life))]
[RequireComponent(typeof(MyEffectCount))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Status))]
public class Enemy : MonoBehaviour
{
    [HideInInspector] public Life life;
    [HideInInspector] public Slider slider;
    [SerializeField] Status status;
    Rigidbody rb;
    IEnemyBrainBase brain;
    
    public bool isBoss;
    public bool isDeath { get; private set; }
    static Vector3 big = new Vector3(3, 2, 1);

    MyEffectCount bag;

    Status playerStatus;

    private void Start()
    {
        life = gameObject.AddComponent<Life>();
        slider = GetComponentInChildren<Slider>();
        if (GetComponent<MyEffectCount>() == null) bag = gameObject.AddComponent<MyEffectCount>();
        else bag = gameObject.GetComponent<MyEffectCount>();
        rb = GetComponent<Rigidbody>();

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
        if (life.LifeSetup(1)) DebugLogger.Log("Error init HP");
        life.AddLastword(DropEffect);
        life.AddLastword(() => { EnemySpawn.EnemyCount--; });
        life.AddLastword(Dead);
        life.AddDamageFunc(Damage);
        life.AddHealFunc(Heal);

        brain = GetComponent<IEnemyBrainBase>();

        //レベルのセット
        status = GetComponent<Status>();
        status.Lv = WorldLevel.GetWorldLevel;

        //プレイヤーのステータスを取得
        playerStatus = PlayerManager.GetManager.GetPlObj.GetComponent<Status>();

        if(!isBoss) gameObject.SetActive(false);
    }

    public void KnockBack()
    {
        brain.Stan(0.2f);
        rb.AddForce(-transform.forward * 10);
    }

    public void Stan(float time)
    {
        brain.Stan(time);
    }

    public void Blind(float time)
    {
        brain.Blind(time);
    }

    //オブジェクトがアクティブになった時
    private void OnEnable()
    {
        isDeath = false;
    }

    //オブジェクトが非表示になった時
    private void OnDisable()
    {
        EnemyFind.OnEnemyExit(gameObject);
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
        DebugLogger.Log("dead:" + name);
        //Destroy(gameObject);

        isDeath = true;
        gameObject.SetActive(false);
        status.Lv = WorldLevel.GetWorldLevel;
        life.HP = life.MaxHP;
        slider.maxValue = life.MaxHP;
        slider.value = life.MaxHP;

        //プレイヤーに経験値を渡す
        if (playerStatus == null) return;
        playerStatus.EXP += status.status[Status.Name.DROP_EXP];
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