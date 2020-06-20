using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id045_Hounds : MonoBehaviour
{
    [SerializeField] GameObject homingParticle;
    [SerializeField] float force = 10f;

    GameObject target;
    GameObject homingParticleObj;
    new ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        homingParticleObj = Instantiate(homingParticle, transform);
        particleSystem = homingParticleObj.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        target = Arts_Process.GetEnemyTarget();
        if (target != null && homingParticleObj != null)
        {
            Arts_Process.HomingParticle(particleSystem, target, force);
        }

        if (transform.childCount == 0) Destroy(gameObject);
    }
}
