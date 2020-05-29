using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public delegate void HeartBeat();
    public delegate void Dead();
    public delegate int DamageEvent(int true_damage);

    public int? MaxHP { get; private set; }
    public int? HP { get; private set; }
    public bool IsFreeze { get; private set; }

    StopWatch timer;
    List<HeartBeat> beat = new List<HeartBeat>();
    List<Dead> dead = new List<Dead>();
    List<DamageEvent> damageEvent = new List<DamageEvent>();
    List<DamageEvent> healEvent = new List<DamageEvent>();
    
    private void Start()
    {
        timer = gameObject.AddComponent<StopWatch>();
        
        beat.Add(() => { CheckDead(); });
        timer.LapEvent = () => { beat.ForEach((live) => { live(); }); };
    }

    private bool CheckDead()
    {
        if (HP == null || !(HP <= 0)) return false;

        timer.Pause(true);
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

    public int? Damage(int fouce)
    {
        if (HP == null) return null;

        int true_damege = fouce;
        HP -= true_damege;
        damageEvent.ForEach((damage) => { damage(true_damege); });
        CheckDead();

        return true_damege;
    }

    public int? Heal(int fouce)
    {
        if (HP == null) LifeSetup(1,1,1);

        int true_heal = (int)((HP + fouce > MaxHP) ? fouce - (MaxHP - HP) : fouce);
        HP += true_heal;
        healEvent.ForEach((heal) => { heal(true_heal); });

        return true_heal;
    }

    //<summary>
    //trueが返ってきた場合はエラーです。エラーの内容はコンソールに出力されます。
    //</summary>
    public bool LifeSetup(int max_hp, int def_hp, float def_beat)
    {
        bool isError = false;

        MaxHP = max_hp;
        if (def_hp > MaxHP)
        {
            def_hp = (int)MaxHP;
            Debug.Log("warnning:def_hp > max_hp");

            isError = true;
        }

        if (def_beat < 0.01f)
        {
            Debug.Log("error:too fast (or minus) beat time.");
            isError = true;
        }

        timer.LapTime = def_beat;



        HP = def_hp;

        return isError;
    }

    public void Freeze(bool freeze)
    {
        IsFreeze = freeze;
        timer.Pause(freeze);
    }
}