﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectUI : MonoBehaviour
{
    [SerializeField] Texture2D texture;
    [SerializeField] float siz;
    [SerializeField] float speed = 5f;
    [SerializeField] Material material;

    float gage = kMaxGage;
    Image image;
    Vector2 defaultSiz;
    RectTransform rectTransform;

    [SerializeField] string SetSceneNameToChangeScene = NameDefinition.SceneName_Result;

    static readonly float kMaxGage = 13f;
    static readonly float kMinGame = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
        image.raycastTarget = false;
        material = image.material = new Material(material);
        material.SetTexture("Texture2D_A204B0BF", texture);
        image.material = material;
        rectTransform = GetComponent<RectTransform>();
        defaultSiz = rectTransform.sizeDelta;
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (gage > kMinGame)
        {
            gage -= Time.unscaledDeltaTime * speed;
            gage = Mathf.Clamp(gage, kMinGame, kMaxGage);
            material.SetFloat("Vector1_714F6F72", gage);
            image.material = material;
        }
        else if(!image.raycastTarget)
        {
            image.raycastTarget = true;
        }
    }

    //表示時
    private void OnEnable()
    {
        image.raycastTarget = false;
        gage = kMaxGage;
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    //非表示時
    private void OnDisable()
    {
        material.SetFloat("Vector1_714F6F72", kMaxGage);
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    /// <summary>
    /// ポインターが触れた時
    /// </summary>
    public void OnPointerEnter()
    {
        rectTransform.sizeDelta *= siz;
    }

    /// <summary>
    /// ポインターが離れたとき
    /// </summary>
    public void OnPointerExit()
    {
        rectTransform.sizeDelta = defaultSiz;
    }

    /// <summary>
    /// クリックした時
    /// </summary>
    public void OnPointerClick()
    {
        if (SetSceneNameToChangeScene == NameDefinition.SceneName_Result)
        {
            MainGameManager.GetArtsReset = true;
        }
        else
        {
            MainGameManager.GetArtsReset = false;
        }

        SceneManager.LoadScene(SetSceneNameToChangeScene);
    }
}