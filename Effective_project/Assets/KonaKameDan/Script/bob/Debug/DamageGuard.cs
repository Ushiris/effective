using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageGuard : MonoBehaviour
{
    [SerializeField] private Life life;
    public bool damageGuard;// trueで無敵化_ON
    void Start()
    {
        GameObject lifeObject = GameObject.FindWithTag("Player");
        life = lifeObject.GetComponent<Life>();
        life.damageGuard = this.damageGuard;// 無敵化するか否か
    }
}
