using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Id247_StanGrenade : MonoBehaviour
{
    enum Process { Start, FadeIn, Delay, FadeOut, End }

    [SerializeField] Image image;
    [SerializeField] GameObject grenadeParticle;
    [SerializeField] GameObject stanArea;
    [SerializeField] Vector3 v0 = new Vector3(0, 5, 7);
    [SerializeField] Vector3 stanAreaSiz = new Vector3(30, 30, 30);
    [SerializeField] float fadeOut = 1f;
    [SerializeField] float fadeIn = 3f;
    [SerializeField] float delay = 3f;
    [SerializeField] float enemyStanTime = 10f;

    [Header("防御のスタック数に応じてたされる数")]
    [SerializeField] float plusStanTime = 0.01f;

    [Header("拡散のスタック数に応じてたされる数")]
    [SerializeField] float plusPow = 0.5f;

    [Header("爆発のスタック数に応じてたされる数")]
    [SerializeField] float plusSiz = 0.05f;

    bool isFadePlay;
    float alpha;
    float time;
    Process process = Process.Start;

    ArtsStatus artsStatus;
    ParticleHitPos particleHitPos;

    static readonly float kMinAlpha = 0;
    static readonly float kMaxAlpha = 0.5f;

    private void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();
        transform.parent = null;

        var c = image.color;
        c.a = kMinAlpha;
        image.color = c;

        //エフェクトの所持数を代入
        var barrierCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Barrier);
        var spreadCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Spread);
        var explosionCount= Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Explosion);

        //エフェクト数値によるステータス変動計算
        enemyStanTime += (float)barrierCount * plusStanTime;
        v0.z += (float)spreadCount * plusPow;
        stanAreaSiz += Vector3.one * ((float)explosionCount * plusSiz);


        //投げる処理
        Rigidbody rb = grenadeParticle.GetComponent<Rigidbody>();
        rb.AddRelativeFor​​ce(v0, ForceMode.VelocityChange);

        //スタン処理
        particleHitPos= grenadeParticle.GetComponent<ParticleHitPos>();
        particleHitPos.OnPlay = () => 
        {
            stanArea.transform.position = particleHitPos.GetParticlePos;
            stanArea.transform.localScale = stanAreaSiz;
            stanArea.SetActive(true);
            grenadeParticle.SetActive(false);
        };


        // SE
        Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id79_Grenade_first, transform.position, artsStatus);
    }

    // Update is called once per frame
    void Update()
    {
        //Particleが消えたら画像処理を入れる
        if (!grenadeParticle.activeSelf && process == Process.Start)
        {
            process = Process.FadeIn;
            // SE
            Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id247_StanGrenade_second, transform.position, artsStatus);
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
}
