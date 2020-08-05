using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id19_JumpSlash : MonoBehaviour
{
    [SerializeField] GameObject swordParticleObj;
    [SerializeField] Vector3 v0 = new Vector3(0, 5, 10);

    bool isDestroy;

    StopWatch timer;
    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        var rb = artsStatus.myObj.GetComponent<Rigidbody>();
        rb.AddRelativeForce(v0, ForceMode.VelocityChange);


        var timer = Arts_Process.TimeAction(gameObject, 0.1f * 9);
        timer.LapEvent = () => {  };
    }

    // Update is called once per frame
    void Update()
    {
        if (isDestroy)
        {
            if (transform.childCount == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void Slash()
    {
        Instantiate(swordParticleObj, transform);
        isDestroy = true;
    }
}
