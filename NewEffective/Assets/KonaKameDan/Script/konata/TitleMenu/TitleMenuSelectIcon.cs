using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuSelectIcon : MonoBehaviour
{
    [SerializeField] string changeSceneName = NameDefinition.SceneName_Main;
    [SerializeField] GameObject titleTextObj;
    [SerializeField] GameObject loadingObj;
    [SerializeField] GameObject loadingAnimationObj;

    public static bool IsSceneLoadProcess { get; private set; } = false;
    public static bool IsNextStageMove { get; private set; } = false;
    public static bool IsSceneChange { get; set; } = false;

    Vector3 siz;

    static readonly float changeSiz = 1.3f;

    private void Start()
    {
        IsSceneLoadProcess = false;
        IsNextStageMove = false;
        IsSceneChange = false;
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
        titleTextObj.SetActive(false);
        loadingObj.SetActive(true);
        loadingAnimationObj.SetActive(true);
        IsSceneLoadProcess = true;
        //SceneManager.LoadScene(changeSceneName);
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(changeSceneName);

        //速攻で遷移しないようにする
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            Debug.Log("flg: " + asyncLoad.isDone + " time: " + asyncLoad.progress);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(5f);
        IsNextStageMove = true;

        while (!IsSceneChange)
        {
            yield return new WaitForEndOfFrame();
        }

        //シーンの移動
        asyncLoad.allowSceneActivation = true;

    }
}
