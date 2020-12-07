using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Id247_StanGrenade : MonoBehaviour
{
    enum Process { Start, FadeIn, Delay, FadeOut, End }

    [SerializeField] Image image;
    [SerializeField] GameObject grenadeParticle;
    [SerializeField] GameObject electricalParticle;
    [SerializeField] float fadeOut = 1f;
    [SerializeField] float fadeIn = 3f;
    [SerializeField] float delay = 3f;
    [SerializeField] float enemyStanTime = 10f;

    bool isFadePlay;
    float alpha;
    float time;
    Process process = Process.Start;

    delegate void Action();
    Action OnEnemyStan;

    static readonly float kMinAlpha = 0;
    static readonly float kMaxAlpha = 5f;

    private void Start()
    {
        var c = image.color;
        c.a = kMinAlpha;
        image.color = c;

        OnEnemyStan = () => { };
    }

    // Update is called once per frame
    void Update()
    {
        //Particleが消えたら画像処理を入れる
        if (!grenadeParticle.activeSelf && process == Process.Start)
        {
            process = Process.FadeIn;
            OnEnemyStan();
        }

        //キャンバスの画像のAlpha値を変更する処理
        var color = image.color;
        switch (process)
        {
            case Process.FadeIn:
                isFadePlay = OnFadePlay(fadeIn, kMaxAlpha);
                if (!isFadePlay) process = Process.Delay;
                break;

            case Process.Delay:
                time += Time.deltaTime;
                if (time > delay) process = Process.FadeOut;
                break;

            case Process.FadeOut:
                isFadePlay = OnFadePlay(-fadeOut, kMinAlpha);
                if (!isFadePlay) process = Process.End;
                break;

            case Process.End:Destroy(gameObject); break;
            default: break;
        }
        color.a = alpha;
        image.color = color;
    }

    bool OnFadePlay(float speed, float goalNum)
    {
        alpha += speed * Time.deltaTime;
        alpha = Mathf.Clamp(alpha, kMinAlpha, kMaxAlpha);

        if (goalNum == alpha)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (process != Process.Start) return;
        var obj = other.gameObject;
        OnEnemyStan += () => { EnemyStan(obj); };
    }

    void EnemyStan(GameObject obj)
    {
        if (obj.tag == "Enemy")
        {
            var enemy = obj.GetComponent<Enemy>();
            enemy.Stan(enemyStanTime);
            var particle = Instantiate(electricalParticle, obj.transform);
            Destroy(particle, enemyStanTime);
        }
    }
}
