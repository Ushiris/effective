using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id27_Suicide : MonoBehaviour
{
    [Header("現在HPの割合ダメージ")]
    [SerializeField] float selfHarmRate = 0.5f;
    [SerializeField] float defaultDamage = 2f;

    [Header("防御のスタック数に応じてたされる数")]
    [SerializeField] float minusDamage = 0.0005f;

    [Header("爆発のスタック数に応じてたされる数")]
    [SerializeField] float plusDamage = 0.1f;


    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //エフェクトの所持数を代入
        var barrierCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Barrier);
        var explosionCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Explosion);

        selfHarmRate -= ((float)barrierCount * minusDamage);

        //ダメージ
        var damageProcess = Arts_Process.SetParticleDamageProcess(gameObject);

        //ダメージの計算
        var damage = defaultDamage + (plusDamage * (float)explosionCount);

        //ダメージ処理
        Arts_Process.Damage(damageProcess, artsStatus, damage, true);

        //自傷ダメージ
        var life = artsStatus.myObj.GetComponent<Life>();
        var selfHarm = life.HP * selfHarmRate;
        life.Damage((int)selfHarm);
        Destroy(gameObject, 2f);
        //SE
        Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id27_Suicide_first, transform.position, artsStatus);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Arts_Process.GetEnemyTag(artsStatus))
        {
            //ダメージ処理
            DebugLogger.Log(other.gameObject.name + "に???ダメージ");
        }
    }
}
