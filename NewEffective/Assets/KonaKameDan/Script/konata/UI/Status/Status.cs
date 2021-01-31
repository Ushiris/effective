using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public enum Name {DROP_EXP, HP, STR, DEF , DEX,Last }

    [Header("レベルアップ時にステータスが上がる幅")]
    [SerializeField] ParticleSystem.MinMaxCurve lvCurve;
    public int Lv;
    int tmpLv;
    float fluctuation;

    [Header("LevelUpに必要な経験値数")]
    [SerializeField] ParticleSystem.MinMaxCurve expCurve;
    public float EXP;
    [SerializeField] bool isLevelUpEXP = false;

    [Header("〇〇秒後にレベルアップ")]
    [SerializeField] float levelUpTimeScale = 10;
    [SerializeField] bool islevelUpTimeScale = false;

    [Header("デフォルト")]
    [SerializeField] StatusClass[] defaultStatus = new StatusClass[]
    {
       new StatusClass(Name.DROP_EXP),
       new StatusClass(Name.HP),
       new StatusClass(Name.STR),
       new StatusClass(Name.DEF),
       new StatusClass(Name.DEX)
    };

    //[Header("変動値")]
    public Dictionary<Name, float> status { get; private set; }

    public float GetMoveSpeed { get; private set; } = 10;

    //ステータスに変動値を
    Dictionary<Name, float> statusEffect = new Dictionary<Name, float>();


    //[HideInInspector] public float MP;
    //[HideInInspector] public float ATK;
    //[HideInInspector] public float INT;
    //[HideInInspector] public float RES;
    //[HideInInspector] public float AGI;
    //[HideInInspector] public float LUK;
    //[HideInInspector] public float VIT;



    // Start is called before the first frame update
    void Awake()
    {
        status = new Dictionary<Name, float>();

        //名前の設定
        foreach (var def in defaultStatus)
        {
            status.Add(def.enumName, def.f);
            statusEffect.Add(def.enumName, 0);
            //DebugLogger.Log(status.Count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        LevelUp();
        if (islevelUpTimeScale) LvTime();
        if (isLevelUpEXP) LevelUp_EXP();
    }

    private void OnDisable()
    {
        //statusEffect.
    }

    void LevelUp_EXP()
    {
        var e = expCurve.Evaluate(Lv) * 100;
        if (EXP > e)
        {
            EXP = 0;
            Lv++;
        }
    }

    //レベルからステータス変動
    void LevelUp()
    {
        if (tmpLv != Lv)
        {
            fluctuation = lvCurve.Evaluate(Lv);

            foreach (var def in defaultStatus)
            {
                status[def.enumName] = def.f * fluctuation + statusEffect[def.enumName];
                DebugLogger.Log(gameObject.name+" "+def.name + " " + status[def.enumName]);
            }
            tmpLv = Lv;
        }
    }

    //特定の時間が経過するとレベルが上がる
    void LvTime()
    {
        if (levelUpTimeScale < Time.time)
        {
            Lv++;
            levelUpTimeScale *= 2;
        }
    }

    /// <summary>
    /// ステータスに変動値をセットできます
    /// </summary>
    public void SetStatusEssence(Name statusName,float essence)
    {
        statusEffect[statusName] = essence;
    }

    /// <summary>
    /// 移動速度にプラス値を
    /// </summary>
    /// <param name="plusSpeed"></param>
    public void SetMoveSpeed(float plusSpeed)
    {
        GetMoveSpeed = plusSpeed;
    }
}

[System.Serializable]
public class StatusClass
{
    [HideInInspector] public string name;
    [HideInInspector] public Status.Name enumName;
    public float f;

    public StatusClass(Status.Name name = Status.Name.Last, float f = 0f)
    {
        this.name = name.ToString();
        this.f = f;
        enumName = name;
    }
}
