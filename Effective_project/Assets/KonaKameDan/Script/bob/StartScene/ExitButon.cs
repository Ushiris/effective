using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButon : MonoBehaviour
{
    private bool exitButton;

    private void Start()
    {
        exitButton = false;
    }
    private void Update()
    {
        if (exitButton)
            UnityEditor.EditorApplication.isPlaying = false;

    }
    public void OnClick()
    {
        exitButton = true;
    }
}
