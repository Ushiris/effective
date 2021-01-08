﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [Header("マウス感度")]
    public float mouseSensitivity = 1f;

    public static PlayerManager GetManager { private set; get; }

    public GameObject GetPlObj;
    int RegeneEventID;
    Life life;
    StopWatch regeneTimer;
    StopWatch traceTimer;
    public Vector3 tracePoint;

    // Start is called before the first frame update
    void Start()
    {
        GetPlObj = GameObject.FindGameObjectWithTag("Player");

        life = GetPlObj.GetComponent<Life>();
        life.LifeSetup(0.3f);
        RegeneEventID = life.AddBeat(() => { life.Heal((int)life.MaxHP / 100 + 1); });
        regeneTimer = StopWatch.Summon(3, StopWatch.voidAction, gameObject);
        regeneTimer.LapEvent = Regene;
        regeneTimer.SetActive(false);
        life.AddDamageFunc(OnDamege);
        life.AddLastword(OnPlayerDead);

        traceTimer = StopWatch.Summon(5.0f, () => tracePoint = GetPlObj.transform.position, GetPlObj);

        GetManager = this;
    }

    public bool IsRegene()
    {
        return GetPlObj.GetComponent<Life>().IsActiveEvent(Life.Timing.beat, RegeneEventID);
    }

    private void OnPlayerDead()
    {
        float timeScale = Time.timeScale;
        Time.timeScale = 0.3f;
        var fade = FadeOut.Summon();
        fade.fadeStartTime = 0.20f;

        var timer = StopWatch.Summon(fade.fadeTime + fade.fadeStartTime - 1, () => {
            Time.timeScale = timeScale;
            SceneManager.LoadScene("GameOver");
        }, GetPlObj);
    }

    private void OnDamege(int damage)
    {
        life.ActiveEvent(Life.Timing.beat, RegeneEventID, false);
        regeneTimer.ResetTimer();
        regeneTimer.SetActive(true);
    }

    private void Regene()
    {
        life.ActiveEvent(Life.Timing.beat, RegeneEventID);
        SE_Manager.SePlay(SE_Manager.SE_NAME.Heel);// SE_Heel
        regeneTimer.SetActive(false);
    }
}
