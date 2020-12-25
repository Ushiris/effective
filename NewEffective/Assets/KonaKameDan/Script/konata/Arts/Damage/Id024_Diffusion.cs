using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id024_Diffusion : MonoBehaviour
{
    int count = 4;

    [SerializeField] GameObject beamParticleObj;
    [SerializeField] float radius = 2f;
    [SerializeField] float lostStartTime = 5;

    [Header("ダメージ")]
    [SerializeField] float defaultDamage = 0.8f;

    [Header("射撃のスタック数に応じてたされる数")]
    [SerializeField] float plusDamage = 0.01f;


    [Header("拡散")]
    [SerializeField] int defaultBullet = 4;

    [Header("拡散のスタック数に応じてたされる数")]
    [SerializeField] float addBullet = 2;

    ArtsStatus artsStatus;
    Beam beamControlScript;

    int shotCount;
    int barrierCount;
    int spreadCount;
    float damage;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();
        Arts_Process.RollReset(gameObject);

        //エフェクトの所持数を代入
        shotCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Shot);
        spreadCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Spread);

        //拡散数
        count = (int)(defaultBullet + (spreadCount * addBullet));

        //ダメージの計算
        damage = Arts_Process.GetDamage(defaultDamage, plusDamage, shotCount);

        //円状の座標取得
        var pos = Arts_Process.GetCirclePutPos(count, radius, 360);

        GameObject[] beam = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            //円状に配置する
            beam[i] = Instantiate(beamParticleObj, transform);
            beam[i].transform.localPosition = pos[i];
            beam[i].transform.rotation = Look(beam[i], gameObject);

            //Beamを消す時間のセット
            beamControlScript = beam[i].GetComponent<Beam>();
            beamControlScript.lostStartTime = lostStartTime;
            var beamCoreObj = beamControlScript.GetBeamObj.transform.GetChild(0);

            //ダメージ
            var hit = Arts_Process.SetParticleZoneDamageProcess(beamCoreObj.gameObject);
            //ダメージ処理
            Arts_Process.ZoneDamage(hit, artsStatus, damage, true);
        }
        //SE
        Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id024_Diffusion_first, transform.position, artsStatus);
    }

    // Update is called once per frame
    void Update()
    {
        if (beamControlScript.isGetEnd)
        {
            Destroy(gameObject, 1f);
        }
        else
        {
            transform.position = PlayerManager.GetManager.GetPlObj.transform.position;
            transform.rotation =
                Quaternion.Euler(0, 1, 0) * PlayerManager.GetManager.GetPlObj.transform.rotation;
        }
    }

    Quaternion Look(GameObject a, GameObject b)
    {
        var aim = a.transform.position - b.transform.position;
        var look = Quaternion.LookRotation(aim);
        return look * Quaternion.AngleAxis(90, Vector3.back);
    }
}
