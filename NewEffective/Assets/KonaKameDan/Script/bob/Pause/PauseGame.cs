using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject pauseUIInstansce;
    float beforeTimeScale = 1.0f;
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
        pauseUIInstansce = Instantiate(pauseUI);
        beforeTimeScale = Time.timeScale;
        Time.timeScale = 0.0f;// pause
        Cursor.visible = true;// マウスカーソル出現！
    }
    public void NoPause()
    {
        Destroy(pauseUIInstansce);
        Time.timeScale = beforeTimeScale;

        if (!UI_Manager.GetIsEffectFusionUI_ChoiceActive)
        {
            Cursor.visible = false;// マウスカーソ削除！
        }
    }
}
