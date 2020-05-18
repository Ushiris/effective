﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public delegate void HeartBeat();
    public delegate void Dead();
    public delegate int DamageEvent(int true_damage);

    public int MaxHP { get; private set; }
    public int HP { get; private set; }

    StopWatch timer;

    List<HeartBeat> beat=new List<HeartBeat>();
    List<Dead> dead = new List<Dead>();
    List<DamageEvent> damageEvent = new List<DamageEvent>();
    List<DamageEvent> healEvent = new List<DamageEvent>();

    private void Awake()
    {
        HP = int.MaxValue;
        MaxHP = int.MaxValue;
        timer = gameObject.AddComponent<StopWatch>();
        timer.LapTime = 1f;
        beat.Add(() => { CheckDead(); });
        timer.LapEvent = () => { beat.ForEach((live) => { live(); }); };

        //error
        if (HP <= 0)
        {
            Debug.Log("Default HP is 0.");
        }
    }

    private bool CheckDead()
    {
        if (!(HP <= 0)) return false;

        timer.Pause(true);
        dead.ForEach((lastword) => { lastword(); });

        return true;
    }

    public void AddLastword(Dead func)
    {
        dead.Insert(0,func);
    }

    public void AddBeat(HeartBeat func)
    {
        beat.Insert(0,func);
    }

    public void AddDamageFunc(DamageEvent func)
    {
        damageEvent.Insert(0,func);
    }

    public void AddHealFunc(DamageEvent func)
    {
        healEvent.Insert(0,func);
    }

    public int Damage(int fouce)
    {
        int true_damege = fouce;
        HP -= true_damege;
        damageEvent.ForEach((damage) => { damage(true_damege); });
        CheckDead();

        return true_damege;
    }

    public int Heal(int fouce)
    {
        int true_heal = (HP + fouce > MaxHP) ? fouce - (MaxHP - HP) : fouce;
        HP += true_heal;
        healEvent.ForEach((heal) => { heal(true_heal); });

        return true_heal;
    }

    //return true is error
    public bool LifeSetup(int max_hp,int def_hp)
    {
        MaxHP = max_hp;

        if (def_hp > MaxHP)
        {
            def_hp = MaxHP;
            return true;
        }

        return false;
    }
}
