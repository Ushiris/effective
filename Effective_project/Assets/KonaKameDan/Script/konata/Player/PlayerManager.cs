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

        GetPlObj.GetComponent<Life>().AddLastword(() =>
        {
            float timeScale= Time.timeScale;
            Time.timeScale = 0.3f;
            var fade = FadeOut.Summon();
            fade.fadeStartTime = 0.20f;

            var timer = StopWatch.Summon(fade.fadeTime + fade.fadeStartTime - 1, () => { SceneManager.LoadScene("GameOver"); Time.timeScale = timeScale; }, GetPlObj);
        }
        );

        GetManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
