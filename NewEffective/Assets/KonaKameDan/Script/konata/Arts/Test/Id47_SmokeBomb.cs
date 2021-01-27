using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id47_SmokeBomb : MonoBehaviour
{
    [SerializeField] GameObject smokeBombParticle;
    [SerializeField] float defaultPlayTime = 10f;

    GameObject smokeBombObj;

    [Header("拡散のスタック数に応じてたされる数")]
    [SerializeField] float plusTime = 0.02f;

    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();
        transform.parent = null;
        smokeBombObj = Instantiate(smokeBombParticle, transform);

        //エフェクトの所持数を代入
        var spreadCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Spread);

        defaultPlayTime += (float)spreadCount * plusTime;

        var p = smokeBombObj.GetComponent<ParticleSystem>();
        var main = p.main;
        main.startLifetime = defaultPlayTime;

        //SE
        Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id257_Haiyoru_second, transform.position, artsStatus);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.childCount == 0) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Enemy") return;

        var enemy = other.gameObject.GetComponent<Enemy>();
        if(enemy!=null) enemy.Stan(4f);
    }
}
