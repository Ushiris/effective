using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBoss : MonoBehaviour
{
    [SerializeField] GameObject artsPivot;
    [SerializeField] EnemyMove enemyMove;
    [SerializeField] EnemyAngle enemyAngle;
    [SerializeField] Transform centerPos;
    [SerializeField] Transform playerPos;
    [SerializeField] float fixSpeed = 1;

    //[SerializeField] GameObject obj;

    bool isPlay = false;

    delegate void Action();
    Action[] iA = new Action[3];

    static readonly string kArtsId_Hounds = "045";              //ハウンズ
    static readonly string kArtsId_ShotGun = "04";              //ショットガン
    static readonly string kArtsId_Arrow = "09";                //弓矢
    static readonly string kArtsId_Funnel = "59";               //召喚[フェアリー]
    static readonly string kArtsId_Diffusion = "024";           //ディフュージョン
    static readonly string kArtsId_UnbreakableShield = "25";    //破られぬ盾
    static readonly string kArtsId_Escape = "29";               //回避
    static readonly string kArtsId_AnnihilationRay = "027";     //殲滅光線
    static readonly string kArtsId_Amaterasu = "079";           //アマテラス
    static readonly string kArtsId_MeteorRain = "479";          //メテオレイン
    static readonly string kArtsId_RocketLauncher = "07";       //ロケラン
    static readonly string kArtsId_Bomber = "57";               //ボマー
    static readonly string kArtsId_Haiyoru = "257";             //這い寄る者
    static readonly string kArtsId_SmokeBomb = "47";            //スモークグレネード
    static readonly string kArtsId_ChargeDrive = "279";         //チャージドライブ


    // Start is called before the first frame update
    void Start()
    {
        playerPos = NewMap.GetPlayerObj.transform;
        enemyMove.SetPos(playerPos, centerPos);
        enemyAngle.SetPos(playerPos, centerPos);

        iA[0] = () => { StartCoroutine(Pattern_A()); };
        iA[1] = () => { StartCoroutine(Pattern_B()); };
        iA[2] = () => { StartCoroutine(Pattern_C()); };
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlay)
        {
            var ran = Random.Range(0, iA.Length);
            iA[ran]();
        }
    }

    IEnumerator Pattern_A()
    {
        EnemyMove.Move moveMode;
        int count = 5;

        isPlay = true;
        enemyAngle.SetAngleType(EnemyAngle.Angle.Player, 50);

        for (int i = 0; i < 3; i++)
        {
            if (i % 2 == 0) moveMode = EnemyMove.Move.PlayerLeftCircle;
            else moveMode = EnemyMove.Move.PlayerRightCircle;

            //Playerから距離を離す
            Escape_Player();
            yield return new WaitForSeconds(0.7f);

            //ハウンズ
            enemyMove.SetMoveType(moveMode, 50 * fixSpeed);
            StartCoroutine(Hounds(count));

            //Playerから距離を離す
            Escape_Player();
            yield return new WaitForSeconds(0.7f);

            //ロケラン
            enemyMove.SetMoveType(moveMode, 70 * fixSpeed);
            StartCoroutine(RocketLauncher(count));

            yield return new WaitForSeconds(1.5f);
        }

        //チャージドライブ3回
        for (int i = 0; i < 3; i++)
        {
            StartCoroutine(ChargeDrive());
            yield return new WaitForSeconds(1f);
        }

        //ディフュージョンまたは殲滅光線
        var ran = Random.Range(0, 1);
        if (ran == 0) StartCoroutine(Diffusion());
        else StartCoroutine(AnnihilationRay(count));

        isPlay = false;
    }

    IEnumerator Pattern_B()
    {
        EnemyMove.Move moveMode;
        isPlay = true;

        enemyAngle.SetAngleType(EnemyAngle.Angle.Player, 50);

        moveMode = EnemyMove.Move.CenterLeftCircle;

        //Playerから距離を離す
        Escape_Player();
        yield return new WaitForSeconds(0.7f);

        //弓矢を撃つ
        for (int i = 0; i < 4; i++)
        {
            enemyMove.SetMoveType(moveMode, 50 * fixSpeed);
            ArtsInstant(kArtsId_Arrow);
            yield return new WaitForSeconds(0.5f);
        }

        //チャージドライブ
        StartCoroutine(ChargeDrive());

        //ショットガン
        for (int i = 0; i < 3; i++)
        {
            moveMode = EnemyMove.Move.GoToPlayer;
            enemyMove.SetMoveType(moveMode, 70 * fixSpeed);

            yield return new WaitUntil(() => enemyMove.GetPlayerDis < 5f);

            enemyAngle.SetAngleType(EnemyAngle.Angle.Player, 100);
            moveMode = EnemyMove.Move.Idol;
            enemyMove.SetMoveType(moveMode, 0 * fixSpeed);

            for (int f = 0; f < 3; f++)
            {
                ArtsInstant(kArtsId_ShotGun);
                yield return new WaitForSeconds(0.1f);
            }
        }

        //アマテラス
        StartCoroutine(Amaterasu(5));

        isPlay = false;
    }

    IEnumerator Pattern_C()
    {
        EnemyMove.Move moveMode;
        isPlay = true;

        moveMode = EnemyMove.Move.PlayerRightCircle;

        enemyAngle.SetAngleType(EnemyAngle.Angle.Player, 50);
        enemyMove.SetMoveType(moveMode, 50 * fixSpeed);

        //Playerから距離を離す
        Escape_Player();
        yield return new WaitForSeconds(0.7f);

        //盾、ボマー、突進
        ArtsInstant(kArtsId_UnbreakableShield);
        for (int i = 0; i < 3; i++)
        {
            ArtsInstant(kArtsId_Bomber);
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(ChargeDrive());

        enemyAngle.SetAngleType(EnemyAngle.Angle.Player, 50);

        //スモーク＆メテオレイン
        ArtsInstant(kArtsId_SmokeBomb);
        StartCoroutine(MeteorRain());
        yield return new WaitForSeconds(0.5f);

        //妖精召喚
        ArtsInstant(kArtsId_Funnel);

        enemyAngle.SetAngleType(EnemyAngle.Angle.Player, 50);
        enemyMove.SetMoveType(moveMode, 70 * fixSpeed);

        //這いよるもの
        ArtsInstant(kArtsId_Haiyoru);

        isPlay = false;
    }

    void Escape_Player()
    {
        if (enemyMove.GetPlayerDis > 10f)
        {
            ArtsInstant(kArtsId_Escape);
        }
    }

    IEnumerator Hounds(int count)
    {
        var arts = kArtsId_Hounds;

        for (int i = 0; i < count; i++)
        {
            ArtsInstant(arts);
            yield return new WaitForSeconds(0.7f);
        }
    }

    IEnumerator ChargeDrive()
    {
        var arts = kArtsId_ChargeDrive;
        enemyAngle.SetAngleType(EnemyAngle.Angle.Idol, 0);

        enemyAngle.ForcedRotation(playerPos, 45);
        ArtsInstant(arts);
        yield return new WaitForSeconds(0.5f);

        enemyAngle.ForcedRotation(playerPos, -90);
        ArtsInstant(arts);
        yield return new WaitForSeconds(0.5f);

        enemyAngle.ForcedRotation(playerPos, 90);
        ArtsInstant(arts);
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator Diffusion()
    {
        enemyAngle.SetAngleType(EnemyAngle.Angle.Center, 50);
        enemyMove.SetMoveType(EnemyMove.Move.GoToCenter, 20 * fixSpeed);
        yield return new WaitUntil(() => enemyMove.GetCenterDis < 0.5f);
        enemyMove.SetMoveType(EnemyMove.Move.Idol, 0);
        ArtsInstant(kArtsId_Diffusion);
        yield return new WaitForSeconds(2f);
    }

    IEnumerator Amaterasu(int count)
    {
        enemyMove.SetMoveType(EnemyMove.Move.GoToCenter, 20 * fixSpeed);
        yield return new WaitUntil(() => enemyMove.GetCenterDis < 0.5f);
        enemyMove.SetMoveType(EnemyMove.Move.Idol, 0 * fixSpeed);

        ArtsInstant(kArtsId_SmokeBomb);

        var r = 360 / count;
        for (int i = 0; i < count; i++)
        {
            enemyAngle.ForcedRotation(centerPos, r * i);
            ArtsInstant(kArtsId_Amaterasu);
        }

        yield return new WaitForSeconds(3f);
    }

    IEnumerator AnnihilationRay(int count)
    {
        enemyMove.SetMoveType(EnemyMove.Move.GoToCenter, 20 * fixSpeed);
        yield return new WaitUntil(() => enemyMove.GetCenterDis < 0.5f);
        enemyMove.SetMoveType(EnemyMove.Move.Idol, 0 * fixSpeed);

        var r = 360 / count;
        for (int i = 0; i < count; i++)
        {
            enemyAngle.ForcedRotation(centerPos, r * i);
            ArtsInstant(kArtsId_AnnihilationRay);
            yield return new WaitForSeconds(1.3f);
        }
    }

    IEnumerator RocketLauncher(int count)
    {
        var r = 360 / count;
        for (int i = 0; i < count; i++)
        {
            enemyAngle.ForcedRotation(centerPos, r * i);
            ArtsInstant(kArtsId_RocketLauncher);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator MeteorRain()
    {
        enemyAngle.SetAngleType(EnemyAngle.Angle.Player, 50);
        ArtsInstant(kArtsId_Escape);
        yield return new WaitForSeconds(0.3f);
        ArtsInstant(kArtsId_MeteorRain);
    }


    void ArtsInstant(string id)
    {
        ArtsInstantManager.InstantArts(artsPivot, id);
        //var o = Instantiate(obj, artsPivot.transform);
        //o.transform.parent = null;
    }
}
