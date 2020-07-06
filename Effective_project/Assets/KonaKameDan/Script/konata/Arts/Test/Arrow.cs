using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float force = 2f;
    [SerializeField] float maxForce = 30f;
    [SerializeField] GameObject obj;

    [SerializeField] GameObject chargeParticleObj;
    [SerializeField] float count = 10;
    float particleCount;

    [SerializeField] float power;
    bool isStart;
    GameObject checkObj;
    GameObject chargeParticle;

    ParticleSystem ps;
    ParticleSystem.MainModule psmm;
    ParticleSystem.EmissionModule psem;

    // Start is called before the first frame update
    void Start()
    {
        ps = obj.GetComponent<ParticleSystem>();
        psmm = ps.main;
        psmm.startSpeed = 0f;

        chargeParticle = Instantiate(chargeParticleObj);
        psem = chargeParticle.GetComponent<ParticleSystem>().emission;
        psem.rateOverTime = new ParticleSystem.MinMaxCurve(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (maxForce > power)
            {
                power += force * Time.deltaTime;

                particleCount += count * Time.deltaTime;
                psem.rateOverTime = new ParticleSystem.MinMaxCurve(particleCount);
            }
            isStart = true;
        }
        else if (Input.GetMouseButtonUp(0) && isStart)
        {
            psmm.startSpeed = power;
            checkObj = Instantiate(obj);
            Destroy(chargeParticle);
        }

        if (checkObj != null)
        {
            Destroy(gameObject);
        }
    }
}
