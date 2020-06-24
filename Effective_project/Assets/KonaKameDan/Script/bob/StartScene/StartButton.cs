using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField] bool artsReset = true;
    private bool startButton;

    private void Start()
    {
        startButton = false;
    }
    private void Update()
    {
        if (startButton)
        {
            MainGameManager.GetArtsReset = artsReset;
            SceneManager.LoadScene("main");
        }

    }
    public void OnClick()
    {
        startButton = true;
    }
}
