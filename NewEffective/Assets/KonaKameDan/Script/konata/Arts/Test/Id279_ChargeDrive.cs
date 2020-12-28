using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id279_ChargeDrive : MonoBehaviour
{
    [SerializeField] float force = 200f;
    [SerializeField] float defaultDamage = 0.2f;

    [Header("爆発のスタック数に応じてたされる数")]
    [SerializeField] float plusDamage = 0.1f;

    [Header("飛翔のスタック数に応じてたされる数")]
    [SerializeField] float plusPow = 1f;

    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();
        transform.localPosition = new Vector3(0, 0, 0.5f);

        var explosionCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Explosion);
        var flyCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Fly);

        force += (float)flyCount * plusPow;

        //ダメージ
        var damageProcess = Arts_Process.SetParticleDamageProcess(gameObject);

        //ダメージの計算
        var damage = defaultDamage + (plusDamage * (float)explosionCount);

        //ダメージ処理
        Arts_Process.Damage(damageProcess, artsStatus, damage, true);

        PlayerMotion.OnPlayerRunningMotion = true;
        var obj = artsStatus.myObj;
        Arts_Process.RbMomentMove(obj, force);
        Destroy(gameObject, 0.5f);

        //SE
        Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id279_ChargeDrive_first, transform.position, artsStatus);
    }

    private void OnDestroy()
    {
        PlayerMotion.OnPlayerRunningMotion = false;
    }
}
