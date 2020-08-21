using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id12_ShieldBash : MonoBehaviour
{
    [SerializeField] GameObject shieldObj;
    [SerializeField] float force = 3f;
    [SerializeField] float moveTime = 0.5f;
    [SerializeField] float lostTime = 1f;

    bool moveOff = false;
    float power;

    StopWatch timer;
    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        //artsStatus = GetComponent<ArtsStatus>();

        //シールドの生成
        var shield = Instantiate(shieldObj, transform);

        timer = Arts_Process.TimeAction(gameObject, moveTime);
        timer.LapEvent = () => { moveOff = true; };
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveOff)
        {
            power += force * (Time.deltaTime * 0.5f);
            var pos = transform.position;
            pos.z += power;
            transform.position = pos;
        }

        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
