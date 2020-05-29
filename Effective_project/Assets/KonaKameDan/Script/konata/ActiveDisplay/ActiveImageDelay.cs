using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveImageDelay : MonoBehaviour
{
    float timer;
    bool on;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (on) timer += Time.deltaTime;
    }

    public void Delay(bool onTrigger, float interval = 0)
    {
        if (onTrigger)
        {
            if (timer > interval)
            {
                GetComponent<Image>().enabled = true;
            }

            on = true;
        }
        else
        {
            timer = 0;
            on = false;

            GetComponent<Image>().enabled = false;
        }
    }
}
