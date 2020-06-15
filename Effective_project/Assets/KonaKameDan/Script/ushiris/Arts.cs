using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arts : MonoBehaviour
{
    public List<ArtsActionElements.ArtsAction> FireActions { get; set; }

    public void Fire()
    {
        FireActions.ForEach((func) => { func(gameObject); });
    }
}
