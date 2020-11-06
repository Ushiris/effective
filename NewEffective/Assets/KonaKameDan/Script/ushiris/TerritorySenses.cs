﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerritorySenses : MonoBehaviour
{
    Canvas bossUI;
    Slider slider;
    GameObject BossName;

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