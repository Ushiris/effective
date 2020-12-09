using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id579_Salamander : MonoBehaviour
{
    [SerializeField] GameObject fairy;
    [SerializeField] ParticleSystem boom;
    [SerializeField] GameObject explosion;
    [SerializeField] int playCount = 3;
    [SerializeField] float lapTime = 3f;

    int count;
    StopWatch timer;
    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        var objs = ArtsActiveObj.Id579_Salamander;
        Arts_Process.OldArtsDestroy(objs, artsStatus.myObj);

        var particleHitPlay = Arts_Process.SetParticleHitPlay(boom.gameObject, explosion, transform, artsStatus);
        particleHitPlay.OnExplosion += () => { SE_Manager.SePlay(SE_Manager.SE_NAME.Id047_PingPong_third); };

        timer = gameObject.AddComponent<StopWatch>();
        timer.LapTime = lapTime;
        timer.LapEvent = () =>
        {
            if (count == playCount) Destroy(gameObject);
            boom.Play();
            count++;
        };
        // SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Id059_SummonPixie_first);
    }
}
