using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFind : MonoBehaviour
{
    Transform enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<enemy>() != null)
        {
            enemy = other.transform;
        }
    }
}
