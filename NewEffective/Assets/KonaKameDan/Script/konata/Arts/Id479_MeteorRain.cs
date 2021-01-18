using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id479_MeteorRain : MonoBehaviour
{
    [SerializeField] GameObject starDustParticleObj;
    [SerializeField] float defaultDamage = 0.4f;

    [Header("隕石の生成位置関係")]
    [SerializeField] Vector3 instantSiz = new Vector3(6, 1, 6);
    [SerializeField] float instantSpace = 1.2f;
    [SerializeField] float h = 30;

    [Header("隕石の速度")]
    [SerializeField] float speed = 30f;

    [Header("生成タイミング")]
    [SerializeField] float interval = 0.1f;

    [Header("拡散のスタック数に応じてたされる数")]
    [SerializeField] float plusStarDustSpread = 0.1f;

    [Header("爆発のスタック数に応じてたされる数")]
    [SerializeField] float plusDamage = 0.05f;

    [Header("飛翔のスタック数に応じてたされる数")]
    [SerializeField] float plusStarDustFly = 0.1f;

    //エフェクトの所持数用
    int spreadCount;
    int explosionCount;
    int flyCount;
    float damage;

    [SerializeField] GameObject markObj;

    GameObject groupObj;

    List<ForwardMove> forwardMoves = new List<ForwardMove>();
    List<Vector3> boxPos = new List<Vector3>();

    StopWatch timer;
    ArtsStatus artsStatus;

    const float r = 45;
    const float fixX = -30;

    SE_Manager.Se3d se;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();
        Arts_Process.RollReset(gameObject);
        Arts_Process.GroundPosMatch(gameObject);

        //エフェクトの所持数を代入
        spreadCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Spread);
        explosionCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Explosion);
        flyCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Fly);

        //隕石を増やす式
        float plusStarDust = ((spreadCount * plusStarDustFly) + (flyCount * plusStarDustFly)) / 2;
        instantSiz += new Vector3(1, 0, 1) * Mathf.Floor(plusStarDust);

        //ダメージの計算
        damage = defaultDamage + (plusDamage * (float)explosionCount);

        //入れ物の生成
        groupObj = Instantiate(new GameObject("GroupObj"), transform);

        //立方体状に座標をsiz分並べる
        boxPos = Arts_Process.SetBoxInstantPos(instantSiz, instantSpace);

        //隕石の生成位置調整
        groupObj.transform.localRotation = Quaternion.Euler(0, 0, r);
        Vector3 pos = new Vector3(fixX, h, 8);
        groupObj.transform.localPosition = pos;

        //隕石が降るところにマークを置く
        var mark = Instantiate(markObj, transform);
        mark.transform.localScale = instantSiz * instantSpace;
        var y = mark.transform.position.y - NewMap.GetGroundPosMatch(transform.position) + 0.5f;
        mark.transform.localPosition = new Vector3(5, y, 13);

        //一定時間ごとに隕石を生成
        timer = gameObject.AddComponent<StopWatch>();
        timer.LapTime = interval;
        timer.LapEvent = () => { Instant(); };

        //SE
        Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id049_ArrowRain_first, transform.position, artsStatus);
        se = Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id479_MeteorRain_second, transform.position, artsStatus);
    }

    // Update is called once per frame
    void Update()
    {
        if (groupObj.transform.childCount == 0 && boxPos.Count == 0)
        {
            if (se != null) SE_Manager.ForcedPlayStop(se.se);
            Destroy(gameObject);
        }
    }

    void Instant()
    {
        if (boxPos.Count == 0) return;

        var ran = Random.Range(0, boxPos.Count);

        //隕石の生成
        var starDustParticle = Instantiate(starDustParticleObj, groupObj.transform);
        starDustParticle.transform.localPosition = boxPos[ran];
        boxPos.RemoveAt(ran);

        //ダメージ
        var starDustDamage = Arts_Process.SetParticleDamageProcess(starDustParticle);

        //ダメージ処理
        Arts_Process.Damage(starDustDamage, artsStatus, damage, true);

        //隕石を動かす
        var forwardMove = Arts_Process.SetForwardMove(starDustParticle, -speed);
        forwardMove.isStart = true;
    }
}
