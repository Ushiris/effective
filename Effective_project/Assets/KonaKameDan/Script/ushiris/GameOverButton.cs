using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
    }

    public void ToResult()
    {
        SceneManager.LoadScene("Result");
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("Start");
    }
}
