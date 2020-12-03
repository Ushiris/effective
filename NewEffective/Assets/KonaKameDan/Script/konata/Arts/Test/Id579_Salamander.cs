using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id579_Salamander : MonoBehaviour
{
    [SerializeField] GameObject fairy;
    [SerializeField] GameObject id79_Grenade;
    [SerializeField] float lostTime = 10f;
    [SerializeField] float lapTime = 3f;

    StopWatch timer;
    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();
        Destroy(gameObject, lostTime);

        timer = gameObject.AddComponent<StopWatch>();
        timer.LapTime = lapTime;
        timer.LapEvent = () =>
        {
            Instantiate(id79_Grenade, fairy.transform);
        };
    }
}
