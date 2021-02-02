using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class StageSelectUiView : MonoBehaviour
{
    [SerializeField] StageSelectUiAction stageSelectUiAction;
    [SerializeField] float mouseHoverSiz = 1.5f;
    [SerializeField] ImageAlpha whiteImage;

    NewMap.MapType mapType;
    Vector3 defaultSiz;
    RectTransform rectTransform;
    AsyncOperation asyncLoad;
    bool isSceneChange = false;

    public static bool isEndProcess { get; set; } = false;

    public static UnityEvent OnBeginSelectWindow = new UnityEvent();

    /// <summary>
    /// ポータルから他のシーンに飛んだ時に実行されるイベント
    /// </summary>
    public static UnityEvent OnAfterPortalChangeScene = new UnityEvent();

    /// <summary>
    /// ステージを選択したかどうか
    /// </summary>
    public bool GetOnPointerClick { get; private set; }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        defaultSiz = rectTransform.localScale;
        isEndProcess = false;
    }

    private void Update()
    {
        if (!isSceneChange) return;

        //シーンの移動
        //SceneManager.LoadScene(NameDefinition.SceneName_Main);
        asyncLoad.allowSceneActivation = true;
    }

    private void OnEnable()
    {
        OnBeginSelectWindow.Invoke();
    }

    /// <summary>
    /// 情報をセットする
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="mapType"></param>
    public void SetStageStatus(Texture texture, NewMap.MapType mapType)
    {
        stageSelectUiAction.OnStageImageChange(texture);
        this.mapType = mapType;
    }

    /// <summary>
    /// ポインターが触れた時
    /// </summary>
    public void OnPointerEnter()
    {
        if (stageSelectUiAction.isStartProcess() || isEndProcess) return;
        rectTransform.localScale *= mouseHoverSiz;
    }

    /// <summary>
    /// ポインターが離れたとき
    /// </summary>
    public void OnPointerExit()
    {
        rectTransform.localScale = defaultSiz;
    }

    /// <summary>
    /// クリックした時
    /// </summary>
    public void OnPointerClick()
    {
        NewMap.SetSelectMapType = mapType;
        DebugLogger.Log("MapName: " + mapType);
        whiteImage.IsAlpha = true;
        isEndProcess = true;
        OnPointerExit();
        MainGameManager.GetArtsReset = false;
        OnAfterPortalChangeScene.Invoke();
        //SceneManager.LoadScene(NameDefinition.SceneName_Main);
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        var sceneName = NameDefinition.SceneName_Main;
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        //速攻で遷移しないようにする
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            DebugLogger.Log(" time: " + asyncLoad.progress);
            yield return new WaitForEndOfFrame();
        }

        while (stageSelectUiAction.isEndProcess())
        {
            DebugLogger.Log("End: " + isEndProcess);
            yield return new WaitForEndOfFrame();
        }

        while (!isSceneChange)
        {
            Debug.Log("SceneChange: " + isSceneChange);
            isSceneChange = true;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1f);
        
        whiteImage.IsAlpha = true;
    }
}
