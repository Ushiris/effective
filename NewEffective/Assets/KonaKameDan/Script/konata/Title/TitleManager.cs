using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : Image2DAnimation
{
    [SerializeField] float sizSpeed = 5f;
    [SerializeField] float fixSiz = 3f;
    [SerializeField] float limitSiz = 3f;
    [SerializeField] VideoControl videoControl;

    // Update is called once per frame
    void Update()
    {
        SizSinAction(sizSpeed, fixSiz, limitSiz);

        if (Input.GetMouseButtonDown(0) && !videoControl.isVideoPlay)
        {
            SE_Manager.SePlay(SE_Manager.SE_NAME.Menu_Decide);
            SceneManager.LoadScene(NameDefinition.SceneName_TitleMenu);
        }
    }
}
