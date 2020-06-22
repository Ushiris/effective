using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private bool startButton;

    private void Start()
    {
        startButton = false;
    }
    private void Update()
    {
        if(startButton)
            SceneManager.LoadScene("main");

    }
    public void OnClick()
    {
        startButton = true;
    }
}
