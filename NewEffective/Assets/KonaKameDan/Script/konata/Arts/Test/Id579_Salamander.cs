using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id579_Salamander : MonoBehaviour
{
    [SerializeField] GameObject fairy;
    [SerializeField] ParticleSystem boom;
    [SerializeField] GameObject explosion;
    [SerializeField] int playCount = 3;
    [SerializeField] float lapTime = 3f;
    [SerializeField] float defaultDamage = 0.6f;

    [Header("追尾のスタック数に応じてたされる数")]
    [SerializeField] float minusTime = 0.01f;

    [Header("爆発のスタック数に応じてたされる数")]
    [SerializeField] float plusDamage = 0.03f;

    [Header("飛翔のスタック数に応じてたされる数")]
    [SerializeField] float plusCount = 0.05f;

    int count;
    StopWatch timer;
    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        var objs = ArtsActiveObj.Id579_Salamander;
        Arts_Process.OldArtsDestroy(objs, artsStatus.myObj);

        //エフェクトの所持数を代入
        var homingCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Homing);
        var explosionCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Explosion);
        var flyCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Fly);

        var particleHitPlay = Arts_Process.SetParticleHitPlay(boom.gameObject, explosion, transform, artsStatus);
        particleHitPlay.OnExplosion += () => { SE_Manager.SePlay(SE_Manager.SE_NAME.Id047_PingPong_third); };

        //ダメージ
        var damageProcess = Arts_Process.SetParticleDamageProcess(boom.gameObject);

        //ダメージの計算
        var damage = defaultDamage + (plusDamage * (float)explosionCount);

        //ダメージ処理
        Arts_Process.Damage(damageProcess, artsStatus, damage, true);

        //ステータス修正
        lapTime -= (float)homingCount * minusTime;
        var tmpCount = (float)flyCount * plusCount;
        playCount += (int)tmpCount;

        timer = gameObject.AddComponent<StopWatch>();
        timer.LapTime = lapTime;
        timer.LapEvent = () =>
        {
            if (count == playCount) Destroy(gameObject);
            boom.Play();
            count++;
        };
        // SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Id059_SummonPixie_first);
    }
}
