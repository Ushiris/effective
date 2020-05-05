using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    Life life;
    private void Start()
    {
        life = gameObject.AddComponent<Life>();
        life.addLastword(Dead);
    }

    void Dead()
    {
        Destroy(gameObject);
    }

    
}


public class EffectFusionMap:MonoBehaviour
{
    string effectName;
    List<EffectFusionMap> derivation;
}