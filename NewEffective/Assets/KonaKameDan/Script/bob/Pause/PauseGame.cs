using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject pauseUIInstansce;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseUIInstansce == null)
            {
                if (Time.timeScale != 0) Pause();
            }
            else
            {
                NoPause();
            }
        }
    }
    public void Pause()
    {
        pauseUIInstansce = GameObject.Instantiate(pauseUI) as GameObject;
        Time.timeScale = 0.0f;// pause
        Cursor.visible = true;// マウスカーソル出現！
    }
    public void NoPause()
    {
        Destroy(pauseUIInstansce);
        Time.timeScale = 1.0f;

        if (!UI_Manager.GetIsEffectFusionUI_ChoiceActive)
        {
            Cursor.visible = false;// マウスカーソ削除！
        }
    }
}
