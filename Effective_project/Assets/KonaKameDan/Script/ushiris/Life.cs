using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public delegate void HeartBeat();
    public delegate void Dead();
    public delegate int DamageEvent(int num);

    public float MaxHP { get { return GetComponent<Status>().status[Status.Name.HP]; } }
    public float? HP { get; set; }
    public bool IsFreeze { get; private set; }

    StopWatch timer;
    List<HeartBeat> beat = new List<HeartBeat>();
    List<Dead> dead = new List<Dead>();
    List<DamageEvent> damageEvent = new List<DamageEvent>();
    List<DamageEvent> healEvent = new List<DamageEvent>();
    
    private void Start()
    {
        if (timer == null)
        {
            timer = gameObject.AddComponent<StopWatch>();
        }

        HP = MaxHP;

        beat.Add(() => { CheckDead(); });
        timer.LapEvent = () => { beat.ForEach((live) => { live(); }); };
    }

    private bool CheckDead()
    {
        if (HP == null || !(HP <= 0)) return false;

        timer.SetActive(false);
        dead.ForEach((lastword) => { lastword(); });

        return true;
    }

    public void AddLastword(Dead func)
    {
        dead.Insert(0, func);
    }

    public void AddBeat(HeartBeat func)
    {
        beat.Insert(0, func);
    }

    public void AddDamageFunc(DamageEvent func)
    {
        damageEvent.Insert(0, func);
    }

    public void AddHealFunc(DamageEvent func)
    {
        healEvent.Insert(0, func);
    }

    public int Damage(int fouce)
    {
        if (HP == null) LifeSetup();

        int true_damege = fouce;
        HP -= true_damege;
        damageEvent.ForEach((damage) => { damage(true_damege); });
        CheckDead();

        return true_damege;
    }

    public int Heal(int fouce)
    {
        if (HP == null) LifeSetup();

        int true_heal = (int)((HP + fouce > MaxHP) ? fouce - (MaxHP - HP) : fouce);
        HP += true_heal;
        healEvent.ForEach((heal) => { heal(true_heal); });

        return true_heal;
    }

    //<summary>
    //trueが返ってきた場合はエラーです。エラーの内容はコンソールに出力されます。
    //</summary>
    public bool LifeSetup(float def_beat,float def_HP=-1)
    {
        bool isError = false;
        if (def_HP == -1)
        {
            def_HP = MaxHP;
        }

        if (timer == null)
        {
            timer = gameObject.AddComponent<StopWatch>();
        }

        timer.LapTime = def_beat;
        HP = def_HP;

        if (def_HP > MaxHP)
        {
            def_HP = (int)MaxHP;
            Debug.Log("warnning:[" + gameObject.name + "] def_hp > max_hp");
            isError = true;
        }

        if (def_beat < 0.01f)
        {
            timer.LapTime = 0.01f;
            Debug.Log("error:[" + gameObject.name + "]too fast (or minus) beat time.");
            isError = true;
        }

        return isError;
    }

    private bool LifeSetup()
    {
        HP = 1;
        timer.LapTime = 1;

        return false;
    }

    public void Freeze(bool freeze)
    {
        IsFreeze = freeze;
        timer.SetActive(!freeze);
    }

    public float getHitPointSafety()
    {
        if (HP == null) LifeSetup();

        return (float)HP;
    }
}