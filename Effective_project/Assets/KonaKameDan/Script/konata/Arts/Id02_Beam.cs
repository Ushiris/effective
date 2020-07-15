using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id02_Beam : MonoBehaviour
{
    [SerializeField] GameObject beamParticleObj;
    [SerializeField] float force = 100f;
    [SerializeField] float lostTime = 3f;

    StopWatch timer;
    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        GameObject beam = Instantiate(beamParticleObj, transform);
        Arts_Process.SetParticleDamageProcess(beam);
        Arts_Process.RbMomentMove(beam, force);

        timer = Arts_Process.TimeAction(gameObject, lostTime);
        timer.LapEvent = () => { Destroy(gameObject); };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
