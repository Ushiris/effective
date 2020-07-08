using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id49_Impact : MonoBehaviour
{
    [SerializeField] float radius = 5f;
    [SerializeField] float explosionForce = 10f;
    [SerializeField] float uppersModifier = 8f;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        Arts_Process.Impact(transform.position, radius, "Enemy", explosionForce, uppersModifier);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject);
    }
}
