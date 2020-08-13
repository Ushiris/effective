using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id014_FuryAttack : MonoBehaviour
{
    [SerializeField] GameObject furyAttackParticleObj;

    // Start is called before the first frame update
    void Start()
    {
        //パーティクル生成
        Instantiate(furyAttackParticleObj, transform);
    }
}
