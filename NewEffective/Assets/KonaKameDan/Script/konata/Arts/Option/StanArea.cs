using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanArea : MonoBehaviour
{
    public float stanTime = 8f;
    public GameObject electricalParticle;

    private void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject;
        EnemyStan(obj);
    }

    void EnemyStan(GameObject obj)
    {
        if (obj.tag == "Enemy")
        {
            var enemy = obj.GetComponent<Enemy>();
            if (enemy == null) return;
            enemy.Stan(stanTime);
            var particle = Instantiate(electricalParticle, obj.transform);
        }
    }
}
