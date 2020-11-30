using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnStart : MonoBehaviour
{
    private bool returnStartScene;

    private void Start()
    {
        returnStartScene = false;
    }
    private void Update()
    {
        if (returnStartScene)
            SceneManager.LoadScene(NameDefinition.SceneName_TitleMenu);

        if (Input.GetKeyDown(KeyCode.Space))
            OnClick();

    }
    public void OnClick()
    {
        returnStartScene = true;
    }
}
