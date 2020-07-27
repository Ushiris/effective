using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public delegate void HeartBeat();
    public delegate void Dead();
    public delegate void DamageEvent(int num);

    public float MaxHP { get { return GetComponent<Status>().status[Status.Name.HP]; } }
    public float? HP { get; set; }
    public bool IsFreeze { get; private set; }
    public bool damageGuard = false;// 無敵化するか否か

    class BeatAction { public bool active; public HeartBeat act; public void Run() { if (active) act(); } };
    class DeadAction { public bool active; public Dead act; public void Run() { if (active) act(); } };
    class LifeAction { public bool active; public DamageEvent act; public void Run(int num) { if (active) act(num); } };

    StopWatch timer;
    List<BeatAction> beat = new List<BeatAction>();
    List<DeadAction> dead = new List<DeadAction>();
    List<LifeAction> damageEvent = new List<LifeAction>();
    List<LifeAction> healEvent = new List<LifeAction>();
    
    private void Start()
    {
        if (timer == null)
        {
            timer = gameObject.AddComponent<StopWatch>();
        }
        HP = MaxHP;

        beat.Add(new BeatAction { active = true, act = () => { CheckDead(); } });
        timer.LapEvent = () => { beat.ForEach((live) => { live.Run(); }); };
    }

    private bool CheckDead()
    {
        if (HP == null || !(HP <= 0)) return false;

        timer.SetActive(false);
        dead.ForEach((lastword) => { lastword.Run(); });

        return true;
    }

    public int AddLastword(Dead func, bool isDestroy = false)
    {
        dead.Add(new DeadAction { active = true, act = isDestroy ? () => { dead.Add(new DeadAction { active = true, act = func }); } : func });
        return dead.Count - 1;
    }

    public int AddBeat(HeartBeat func)
    {
        beat.Add(new BeatAction { active = true, act = func });
        return beat.Count - 1;
    }

    public int AddDamageFunc(DamageEvent func)
    {
        damageEvent.Add(new LifeAction { active = true, act = func });
        return damageEvent.Count - 1;
    }

    public int AddHealFunc(DamageEvent func)
    {
        healEvent.Insert(0, new LifeAction { active = true, act = func });
        return healEvent.Count - 1;
    }

    public enum Timing
    {
        beat,dead,damage,heal
    }

    public void ActiveEvent(Timing timing, int id, bool active = true)
    {
        switch (timing)
        {
            case Timing.beat:
                beat[id].active = active;
                break;
            case Timing.dead:
                dead[id].active = active;
                break;
            case Timing.damage:
                damageEvent[id].active = active;
                break;
            case Timing.heal:
                healEvent[id].active = active;
                break;
            default:
                break;
        }
    }

    public int Damage(int fouce)
    {
        if (HP == null) LifeSetup();

        int true_damege = fouce;
        HP -= true_damege;
        damageEvent.ForEach((damage) => { damage.Run(true_damege); });
        CheckDead();

        return true_damege;
    }

    public int Heal(int fouce)
    {
        if (HP == null) LifeSetup();

        int true_heal = (int)((HP + fouce > MaxHP) ? MaxHP - HP : fouce);
        HP += true_heal;
        healEvent.ForEach((heal) => { heal.Run(true_heal); });

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