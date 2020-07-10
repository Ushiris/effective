using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id49_Impact : MonoBehaviour
{
    [SerializeField] float radius = 5f;
    [SerializeField] float explosionForce = 10f;
    [SerializeField] float uppersModifier = 8f;

    [SerializeField] GameObject impactObj;
    [SerializeField] Vector3 impactMaxSiz = new Vector3(5, 5, 5);
    [SerializeField] float impactSizUpSpeed = 50f;
    GameObject impact;
    ObjSizChange isMagicCircleSiz;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        impact = Instantiate(impactObj, transform);
        Arts_Process.SetAddObjSizChange(
                    impact,
                    Vector3.zero, impactMaxSiz, Vector3.one,
                    impactSizUpSpeed,
                    ObjSizChange.SizChangeMode.ScaleUp);


        Arts_Process.Impact(transform.position, radius, "Enemy", explosionForce, uppersModifier);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject);
    }
}
