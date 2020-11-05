using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id19_JumpSlash : MonoBehaviour
{
    [SerializeField] GameObject swordParticleObj;
    [SerializeField] Vector3 v0 = new Vector3(0, 5, 10);

    bool isDestroy;
    Rigidbody rb;

    StopWatch timer;
    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        rb = artsStatus.myObj.GetComponent<Rigidbody>();
        rb.AddRelativeForce(v0, ForceMode.VelocityChange);

        Slash();

        //var timer = Arts_Process.TimeAction(gameObject, 0.1f * 1);
        //timer.LapEvent = () => { Slash(); };
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0) Destroy(gameObject);
        rb.AddForce(new Vector3(0, -1, 0) * 30);
    }

    void Slash()
    {
        var obj = Instantiate(swordParticleObj, transform);
        obj.transform.localPosition = new Vector3(0, 0, 1f);
    }
}
