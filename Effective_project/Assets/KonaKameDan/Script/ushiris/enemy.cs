using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Life life;
    private void Start()
    {
        life = gameObject.AddComponent<Life>();
        life.AddLastword(Dead);
    }

    void Dead()
    {
        Destroy(gameObject);
    }
}