using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id04_ShotGun : MonoBehaviour
{
    [SerializeField] GameObject shotGunParticle;
    [SerializeField] float defaultDamage = 0.2f;

    GameObject shotGunParticleObj;

    ParticleHit homingDamage;

    // Start is called before the first frame update
    void Start()
    {
        shotGunParticleObj = Instantiate(shotGunParticle, transform);

        //ダメージ
        Arts_Process.SetParticleDamageProcess(shotGunParticleObj);
        homingDamage = shotGunParticleObj.GetComponent<ParticleHit>();

        //SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Shot);
    }

    void LateUpdate()
    {
        //ダメージ処理
        Arts_Process.Damage(homingDamage, defaultDamage, true);

        //オブジェクトを消す
        if (transform.childCount == 0) Destroy(gameObject);
    }
}
