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
            GameObject fade = FadeOut.Summon();
            var timer = StopWatch.Summon(fade.GetComponentInChildren<FadeOut>().fadeTime + 1, () => { SceneManager.LoadScene("GameOver"); },GetPlObj);
        }
        );

        GetManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
