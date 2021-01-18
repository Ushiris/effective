using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : Image2DAnimation
{
    [SerializeField] float sizSpeed = 5f;
    [SerializeField] float fixSiz = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SizSinAction(sizSpeed, fixSiz);

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(NameDefinition.SceneName_TitleMenu);
        }
    }
}
