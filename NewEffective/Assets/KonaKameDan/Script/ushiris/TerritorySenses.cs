using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TerritorySenses : MonoBehaviour
{
    Canvas bossUI;
    Slider slider;
    GameObject BossName;
    public static UnityEvent 
        OnTerrtoryEnter = new UnityEvent(),
        OnTerrtoryExit = new UnityEvent();
    static int bossCount = 0;
    UnityEvent OnStartFinish = new UnityEvent();
    bool isStartFinished = false;

    private void Start()
    {
        //gameObject.tag = "BossZone";

        bossUI = new GameObject().AddComponent<Canvas>();
        bossUI.renderMode = RenderMode.ScreenSpaceOverlay;
        bossUI.sortingOrder = -100;
        var setting=bossUI.gameObject.AddComponent<CanvasScaler>();
        setting.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        setting.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
        setting.referenceResolution = new Vector3(720, 1280);
        setting.referencePixelsPerUnit = 100;
        BossName = Instantiate(Resources.Load("UI/BossName")) as GameObject;

        BossName.GetComponent<RectTransform>().SetParent(bossUI.GetComponent<RectTransform>());
        BossName.transform.localPosition = new Vector3(-50, 250);

        Enemy enemy = transform.parent.gameObject.GetComponent<Enemy>();
        slider = Instantiate(enemy.slider);
        BossName.GetComponent<BossNameUI>().Generate(slider, bossUI);
        BossName.SetActive(false);
        var UiSlider = BossName.GetComponentInChildren<Slider>();
        enemy.life.AddDamageFunc((damege) => UiSlider.value -= damege);
        enemy.life.AddLastword(
            () => {
                bossCount = 0;
                OnTerrtoryExit.Invoke();
                bossUI.gameObject.SetActive(false);
                Portal.OnPortalOpen();
                ResultScore.OnBossKillCountPlus();
            });

        SphereCollider collider= gameObject.AddComponent<SphereCollider>();
        collider.transform.parent = gameObject.transform;
        collider.transform.localPosition = Vector3.zero;
        collider.radius = 50;
        collider.isTrigger = true;

        StageSelectUiView.OnAfterPortalChangeScene.AddListener(() => bossCount = 0);
        StageSelectUiView.OnBeginSelectWindow.AddListener(() =>
        {
            bossUI.gameObject.SetActive(false);
            StageSelectUiView.OnBeginSelectWindow.RemoveAllListeners();
        });

        isStartFinished = true;
        OnStartFinish.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        bossCount++;
        if (bossCount == 1)
        {
            OnTerrtoryEnter.Invoke();
            BossName.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (bossCount > 0) bossCount--;
        if (bossCount == 0)
        {
            OnTerrtoryExit.Invoke();
            BossName.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        bossCount = 0;
    }

    public void SetName(string new_name)
    {
        string trueName = new_name.Replace("(clone)", "");
        if (isStartFinished) BossName.GetComponent<BossNameUI>().SetName(trueName);
        else OnStartFinish.AddListener(() => BossName.GetComponent<BossNameUI>().SetName(trueName));
    }
}
