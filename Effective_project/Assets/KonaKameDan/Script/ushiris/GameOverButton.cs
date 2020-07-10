using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    public void ToResult()
    {
        SceneManager.LoadScene("Result");
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("Start");
    }
}
