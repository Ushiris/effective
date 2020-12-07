using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    AudioSource se;
    AudioSource se1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            se = SE_Manager.SePlay(SE_Manager.SE_NAME.Hit);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            se1 = SE_Manager.SePlay(SE_Manager.SE_NAME.Heel);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SE_Manager.SetFadeOut(this, se, 0.1f);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SE_Manager.SetFadeOut(this, se1, 0.1f);
        }
    }
}
