using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{  
    [SerializeField] ParticleSystem.MinMaxCurve curve;
    [SerializeField] float lv;
    [SerializeField] float value;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        value = curve.Evaluate(lv);
    }


}
