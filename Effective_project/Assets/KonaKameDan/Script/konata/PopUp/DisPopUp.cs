using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisPopUp : MonoBehaviour
{
    [SerializeField] float dis;
    [SerializeField] GameObject target;
    [SerializeField] GameObject my;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float f = Vector3.Distance(my.transform.position, target.transform.position);

        if (f > dis)
        {
            if (!my.activeSelf) my.SetActive(true);
        }
        else
        {
            if (my.activeSelf) my.SetActive(false);
        }
    }
}
