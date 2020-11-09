using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayBGM : MonoBehaviour
{
    [Header("キーボード[B]でBGMを初めから再生")]
    [SerializeField]private bool encoreBGM = true;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
            encoreBGM = true;

        if(encoreBGM)
        {
            //BGM
            //BGM_Manager.BgmPlay(BGM_Manager.BGM_NAME.Stage_1);
            encoreBGM = false;
        }
    }
}
