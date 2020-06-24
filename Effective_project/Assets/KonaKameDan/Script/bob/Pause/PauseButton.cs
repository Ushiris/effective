using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    private bool titleButton;
    private bool ExitButton;
    [SerializeField] private PauseGame pauseGame;

    private void Start()
    {
        titleButton = false;
        ExitButton = false;
        GameObject gameManager = GameObject.Find("GameManager");
        pauseGame = gameManager.GetComponent<PauseGame>();
    }
    private void Update()
    {
        if (titleButton)
        {
            SceneManager.LoadScene("Start");
        }

        if (ExitButton)
        {
            pauseGame.NoPause();
        }

    }
    public void OnClickTitle()
    {
        titleButton = true;
    }
    public void OnClickExit()
    {
        ExitButton = true;
    }
}
