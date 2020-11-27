using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookObj : MonoBehaviour
{
    [SerializeField] GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        var aim = target.transform.position - transform.position;
        var look = Quaternion.LookRotation(aim);
        transform.rotation = look * Quaternion.AngleAxis(90, Vector3.up);
    }
}
