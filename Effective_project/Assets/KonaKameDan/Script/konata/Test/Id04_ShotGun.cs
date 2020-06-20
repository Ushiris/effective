using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id04_ShotGun : MonoBehaviour
{
    [SerializeField] GameObject shotGunParticle;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(shotGunParticle, transform);
    }
}
