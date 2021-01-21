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

    private void Start()
    {
        //gameObject.tag = "BossZone";

        bossUI = Instantiate(new GameObject()).AddComponent<Canvas>();
        bossUI.gameObject.SetActive(false);
        bossUI.renderMode = RenderMode.ScreenSpaceOverlay;

        BossName = Instantiate(Resources.Load("UI/BossName"))as GameObject;
        BossName.GetComponent<RectTransform>().SetParent(bossUI.GetComponent<RectTransform>());
        BossName.transform.localPosition = new Vector3(0, 250);

        Enemy enemy = transform.parent.gameObject.GetComponent<Enemy>();
        slider = Instantiate(enemy.slider);
        slider.transform.SetParent(bossUI.transform);
        slider.transform.localPosition = new Vector3(0, 225, 0);
        slider.transform.localScale = new Vector3(0.5f, 8, 0.1f);
        slider.direction = Slider.Direction.RightToLeft;
        enemy.life.AddDamageFunc((num) => { slider.value -= num; });
        enemy.life.AddHealFunc((num) => { slider.value += num; });

        SphereCollider collider= gameObject.AddComponent<SphereCollider>();
        collider.transform.parent = gameObject.transform;
        collider.transform.localPosition = Vector3.zero;
        collider.radius = 50;
        collider.isTrigger = true;

        StageSelectUI.OnAfterPortalChangeScene.AddListener(() => bossCount = 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        bossCount++;
        if (bossCount == 1)
        {
            OnTerrtoryEnter.Invoke();
            bossUI.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (bossCount > 0) bossCount--;
        if (bossCount == 0)
        {
            OnTerrtoryExit.Invoke();
            bossUI.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        bossCount = 0;
    }
}
