using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerritorySenses : MonoBehaviour
{
    Canvas bossUI;
    Slider slider;

    private void Start()
    {
        bossUI = Instantiate(new GameObject()).AddComponent<Canvas>();
        bossUI.gameObject.SetActive(false);
        bossUI.renderMode = RenderMode.ScreenSpaceOverlay;

        Text text = bossUI.gameObject.AddComponent<Text>();
        text.text = "Boss name";
        text.alignment = TextAnchor.MiddleCenter;
        text.resizeTextForBestFit = true;
        text.fontSize = 25;
        text.rectTransform.sizeDelta = new Vector2(500, 500);
        text.font = font;

        Enemy enemy = transform.parent.gameObject.GetComponent<Enemy>();
        slider = Instantiate(enemy.slider);
        slider.transform.parent = bossUI.transform;
        slider.transform.localPosition = new Vector3(0, 225, 0);
        slider.transform.localScale = new Vector3(0.2f, 8, 0.1f);
        enemy.life.AddDamageFunc((num) => { slider.value -= num; return num; });
        enemy.life.AddHealFunc((num) => { slider.value += num; return num; });

        SphereCollider collider= gameObject.AddComponent<SphereCollider>();
        collider.transform.parent = gameObject.transform;
        collider.transform.position = Vector3.zero;
        collider.radius = 5;
        collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        bossUI.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") return;
        bossUI.gameObject.SetActive(false);
    }
}
