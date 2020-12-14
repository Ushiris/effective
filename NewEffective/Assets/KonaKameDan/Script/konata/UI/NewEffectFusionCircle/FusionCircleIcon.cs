using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionCircleIcon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var t = GetComponent<RectTransform>();
        t.rotation = Quaternion.Euler(Vector3.zero);
    }
}
