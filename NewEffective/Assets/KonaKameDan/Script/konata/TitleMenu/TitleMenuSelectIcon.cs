using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuSelectIcon : MonoBehaviour
{
    [SerializeField] string changeSceneName = NameDefinition.SceneName_Main;

    Vector3 siz;

    static readonly float changeSiz = 1.3f;

    private void Start()
    {
        siz = transform.localScale;
    }

    public bool OnSizChange()
    {
        transform.localScale = siz * changeSiz;
        return true;
    }

    public bool OnDefaultSiz()
    {
        transform.localScale = siz;
        return false;
    }

    public void OnSceneChange()
    {
        SceneManager.LoadScene(changeSceneName);
    }
}
