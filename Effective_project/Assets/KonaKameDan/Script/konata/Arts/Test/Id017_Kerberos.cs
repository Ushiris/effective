using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id017_Kerberos : MonoBehaviour
{
    [SerializeField] GameObject kerberos;

    // Start is called before the first frame update
    void Start()
    {
        var obj = Instantiate(kerberos, transform);
        Destroy(gameObject, 3f);
    }
}
