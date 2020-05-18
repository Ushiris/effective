using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera : MonoBehaviour
{
    Transform cameraPos;

    // Start is called before the first frame update
    private void Awake()
    {
        cameraPos = GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cameraPos);
    }
}
