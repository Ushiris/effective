using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    int HP = 1;

    private void Awake()
    {
        if (HP <= 0)
        {
            Debug.Log("Default HP is 0.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        Destroy(gameObject);
    }

    int damege(int fouce)
    {
        HP -= fouce;
        return fouce;
    }
}
