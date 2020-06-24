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
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
        }
    }
    public void OnClick()
    {
        exitButton = true;
    }
}
