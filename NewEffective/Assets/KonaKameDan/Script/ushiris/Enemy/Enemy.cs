using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Life))]
[RequireComponent(typeof(EnemyArtsPickUp))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Status))]
public class Enemy : MonoBehaviour
{
    [HideInInspector] public Life life;
    [HideInInspector] public Slider slider;
    [SerializeField] string enemyName = "";
    [SerializeField] Status status;
    [SerializeField] int effectDropCount = 1;
    public AimBot muzzle;
    Rigidbody rb;
    public bool isBoss;
    public bool IsDeath { get; private set; }
    public EnemyBrainBase Brain { get; private set; }

    static Vector3 big = new Vector3(3, 2, 1);

    EnemyArtsPickUp artsPickUp;
    Status playerStatus;

    static readonly float kEffectDropRange = 10;

    private void Awake()
    {
        life = gameObject.AddComponent<Life>();
        slider = GetComponentInChildren<Slider>();
        artsPickUp = GetComponent<EnemyArtsPickUp>();
        rb = GetComponent<Rigidbody>();
        Brain = GetComponent<EnemyBrainBase>();
        status = GetComponent<Status>();
    }

    private void Start()
    {
        playerStatus = playerStatus == null ? PlayerManager.GetManager.GetPlObj.GetComponent<Status>() : playerStatus;

        //sliderの初期化
        slider.minValue = 0;
        slider.maxValue = life.MaxHP;
        slider.value = life.MaxHP;

        if (enemyName == "") enemyName = gameObject.name;

        if (isBoss)
        {
            slider.GetComponentInParent<Canvas>().transform.localScale = big;
            GameObject Territory = new GameObject();
            Territory.tag = "BossZone";
            Territory.transform.parent = gameObject.transform;
            var sence = Territory.AddComponent<TerritorySenses>();
            sence.SetName(enemyName);
        }

        //Lifeの初期化
        if (life.LifeSetup(1)) DebugLogger.Log("Error init HP");
        life.AddLastword(DropEffect);
        life.AddLastword(() => { EnemySpawn.EnemyCount--; });
        life.AddLastword(Dead);
        life.AddDamageFunc(Damage);
        life.AddHealFunc(Heal);

        //レベルのセット
        status.Lv = WorldLevel.GetWorldLevel;
    }

    public void KnockBack(float power = 10, float stan_time = 0.4f)
    {
        rb.AddForce(-transform.forward * power);
        Stan(stan_time);
    }

    public void Stan(float time)
    {
        Brain.Stan(time);
    }

    public void Blind(float time)
    {
        Brain.Blind(time);
    }

    //オブジェクトがアクティブになった時
    private void OnEnable()
    {
        IsDeath = false;
    }

    //オブジェクトが非表示になった時
    private void OnDisable()
    {
        EnemyFind.OnEnemyExit(gameObject);
    }

    void DropEffect()
    {
        var fixPos = new Vector3(0, 1.5f, 0);
        for (int i = 0; i < effectDropCount; i++)
        {
            var name = artsPickUp.GetEffect[Random.Range(0, artsPickUp.GetEffect.Count - 1)];
            var effect = Instantiate(Resources.Load("EffectObj/[" + name.ToString() + "]EffectObject")) as GameObject;
            effect.transform.position = gameObject.transform.position + fixPos;
            effect.GetComponent<Rigidbody>().AddRelativeForce(Random.onUnitSphere * kEffectDropRange, ForceMode.VelocityChange);
        }
    }

    void Dead()
    {
        DebugLogger.Log("dead:" + name);
        //Destroy(gameObject);

        IsDeath = true;
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

    public void Aim()
    {
        muzzle.Aim();
    }
}