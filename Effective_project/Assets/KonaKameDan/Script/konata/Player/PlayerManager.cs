using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [Header("マウス感度")]
    public float mouseSensitivity = 1f;

    public static PlayerManager GetManager { private set; get; }

    public GameObject GetPlObj;

    // Start is called before the first frame update
    void Start()
    {
        GetPlObj = GameObject.FindGameObjectWithTag("Player");

        var life = GetPlObj.GetComponent<Life>();
        life.LifeSetup(0.3f);
        var regene_id = life.AddBeat(() => { life.Heal((int)life.MaxHP / 100); });
        var regene_timer = StopWatch.Summon(3, () => { life.ActiveEvent(Life.Timing.beat, regene_id); }, gameObject);
        life.AddDamageFunc((x) => {
            life.ActiveEvent(Life.Timing.beat, regene_id, false);
            regene_timer.ResetTimer();
        });

        life.AddLastword(() =>
        {
            float timeScale= Time.timeScale;
            Time.timeScale = 0.3f;
            var fade = FadeOut.Summon();
            fade.fadeStartTime = 0.20f;

            var timer = StopWatch.Summon(fade.fadeTime + fade.fadeStartTime - 1, () => {
                Time.timeScale = timeScale;
                SceneManager.LoadScene("GameOver");
            }, GetPlObj);
        });

        GetManager = this;
    }
}
