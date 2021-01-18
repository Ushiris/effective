using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : Image2DAnimation
{
    [SerializeField] bool isDebugStage = true;
    [SerializeField] float sizSpeed = 5f;
    [SerializeField] float fixSiz = 3f;
    [SerializeField] float limitSiz = 3f;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        if (isDebugStage) NewMap.SetSelectMapType = NewMap.MapType.DebugMap;
#endif
    }

    // Update is called once per frame
    void Update()
    {
        SizSinAction(sizSpeed, fixSiz, limitSiz);

        if (Input.GetMouseButtonDown(0))
        {
            SE_Manager.SePlay(SE_Manager.SE_NAME.Menu_SceneChange);
            SceneManager.LoadScene(NameDefinition.SceneName_TitleMenu);
        }
    }
}
