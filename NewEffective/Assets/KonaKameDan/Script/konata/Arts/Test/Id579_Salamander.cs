using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id579_Salamander : MonoBehaviour
{
    [SerializeField] GameObject fairy;
    [SerializeField] ParticleSystem boom;
    [SerializeField] GameObject explosion;
    [SerializeField] float lostTime = 10f;
    [SerializeField] float lapTime = 3f;

    StopWatch timer;
    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();
        Destroy(gameObject, lostTime);

        var objs = ArtsActiveObj.Id579_Salamander;
        Arts_Process.OldArtsDestroy(objs, artsStatus.myObj);

        Arts_Process.SetParticleHitPlay(boom.gameObject, explosion, transform, artsStatus);

        timer = gameObject.AddComponent<StopWatch>();
        timer.LapTime = lapTime;
        timer.LapEvent = () =>
        {
            boom.Play();
        };
    }
}
