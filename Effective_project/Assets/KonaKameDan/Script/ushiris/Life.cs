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
        timer = gameObject.AddComponent<StopWatch>();
        timer.LapTime = 1f;
        timer.LapEvent = () => { beat.ForEach((live) => { live(); }); };

        //error
        if (HP <= 0)
        {
            Debug.Log("Default HP is 0.");
        }
    }

    private void Update()
    {
        if (HP <= 0)
        {
            timer.Pause(true);
            dead.ForEach((lastword) => { lastword(); });
        }
    }

    public void AddLastword(Dead func)
    {
        dead.Add(func);
    }

    public void AddBeat(HeartBeat func)
    {
        beat.Add(func);
    }

    public void AddDamageFunc(DamageEvent func)
    {
        damageEvent.Add(func);
    }

    public void AddHealFunc(DamageEvent func)
    {
        healEvent.Add(func);
    }

    public int Damage(int fouce)
    {
        int true_damege = fouce;
        HP -= true_damege;
        damageEvent.ForEach((damage) => { damage(true_damege); });
        return true_damege;
    }

    public int Heal(int fouce)
    {
        int true_heal = (HP + fouce > MaxHP) ? fouce - (MaxHP - HP) : fouce;
        HP += true_heal;
        healEvent.ForEach((heal) => { heal(true_heal); });
        return true_heal;
    }
}
