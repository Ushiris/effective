using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayBGM : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //BGM
            BGM_Manager.BgmPlay(BGM_Manager.BGM_NAME.Stage_1);
        }
    }
}
