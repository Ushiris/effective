using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
        Time.timeScale = 1;
    }

    public void ToResult()
    {
        SceneManager.LoadScene(NameDefinition.SceneName_Result);
    }

    public void ToTitle()
    {
        SceneManager.LoadScene(NameDefinition.SceneName_TitleMenu);
    }
}
