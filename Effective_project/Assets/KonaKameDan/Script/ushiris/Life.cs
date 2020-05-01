using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Life : MonoBehaviour
{
    public delegate void HeartBeat();
    public delegate void Dead();

    int HP = 1;

    List<HeartBeat> beat;
    List<Dead> dead;

    private void Awake()
    {
        //init life behaviour
        beat.Add(() => { });
        dead.Add(() => { });

        //error
        if (HP <= 0)
        {
            Debug.Log("Default HP is 0.");
        }
    }

    private void Update()
    {
        beat.ForEach((live) => { live(); });
        if (HP <= 0)
        {
            dead.ForEach((lastword) => { lastword(); });
        }
    }

    public void addLastword(Dead func)
    {
        dead.Add(func);
    }

    public void addBeat(HeartBeat func)
    {
        beat.Add(func);
    }

    public int Damage(int fouce)
    {
        int true_damege = fouce;
        HP -= true_damege;
        return true_damege;
    }
}
