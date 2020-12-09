using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 流したい音をセットして、別のスクリプトで呼べるようにするやつ
/// </summary>
public class SE_Manager : MonoBehaviour
{
    [SerializeField] int audioInstantMaxCount = 10;
    [SerializeField] GameObject se3dObj;

    AudioSource[] seArr;

    /// <summary>
    /// SEの種類名
    /// </summary>
    public enum SE_NAME
    {
        Hit, Shot, CastArts, Heel, EffectPlate_EffectObject_Set, EffectPlate_ArtsName_Set, EffectPlate_Switching, Nothing,
        //アーツSE
        Id024_Diffusion_first, Id024_Diffusion_second, 
        Id025_PrimitiveShield_first, Id025_PrimitiveShield_third,
        Id029_JumpCube_first, Id029_JumpCube_third,
        Id045_Hounds_first, Id045_Hounds_second,
        Id047_PingPong_first, Id047_PingPong_second, Id047_PingPong_third,
        Id049_ArrowRain_first, Id049_ArrowRain_second,
        Id057_Managarmr_first,
        Id059_SummonPixie_first,
        Id079_Amaterasu_first,Id079_Amaterasu_second,
        Id245_EMPCube_first, Id245_EMPCube_second, Id245_EMPCube_third,
        Id249_Icarus_first, Id249_Icarus_second,
        Id257_Haiyoru_first, Id257_Haiyoru_second,
        Id279_ChargeDrive_first,
        Id457_BigBang_second,
        Id459_Direction_second,
        Id479_MeteorRain_second,
        Id04_ShotGun_first,
        Id07_RocketLauncher_first, Id07_RocketLauncher_second,
        Id09_Arrow_first, Id09_Arrow_second,
        Id25_UnbreakableShield_second,
        Id29_Escape_first,
        Id45_Search_first,
        Id49_Impact_first,
        Id59_Funnel_first, Id59_Funnel_third,
        Id79_Grenade_first,
        Id027_annihilationRay_first,
        Id247_StanGrenade_second,
        Id27_Suicide_first
    }

    [System.Serializable]
    public class SeData
    {
        public AudioClip audio;
        [Range(0, 1f)] public float seVolume = 0.1f;
    }

    public class Se3d
    {
        public GameObject obj;
        public AudioSource se;
    }
    Se3d[] se3D;

    public PrefabDictionary seData;

    static SE_Manager SE_Manager_;

    delegate void Action();
    static List<Action> OnFadeList = new List<Action>();

    // Start is called before the first frame update
    void Start()
    {
        //オーディオをアタッチする
        for (int i = 0; i < audioInstantMaxCount; i++)
        {
            gameObject.AddComponent<AudioSource>();
        }
        seArr = GetComponents<AudioSource>();

        SE_Manager_ = this;
    }

    //3DSEを使用する場合Startで宣言すること
    void StartUp3dSe(int maxCount)
    {
        var group = new GameObject("3dSeGroup");
        group.transform.parent = transform;
        se3D = new Se3d[maxCount];

        for (int i = 0; i < maxCount; i++)
        {
            var obj = Instantiate(se3dObj, group.transform);
            var se= obj.AddComponent<AudioSource>();

            se3D[i].obj = obj;
            se3D[i].se = se;
        }
    }

    /// <summary>
    /// SEの再生、どのSEを使うか選択して！
    /// </summary>
    /// <param name="seType"></param>
    public static AudioSource SePlay(SE_NAME seType)
    {
        //被って流せる音は最大10つまで
        //再生中でないオーディオを探す
        if (SE_Manager_ != null)
        {
            foreach (AudioSource se in SE_Manager_.seArr)
            {
                if (!se.isPlaying)
                {
                    se.loop = false;
                    var audioData = SE_Manager_.seData.GetTable()[seType];
                    se.volume = audioData.seVolume;
                    se.PlayOneShot(audioData.audio);
                    return se;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// 3DSE用
    /// オブジェクトの位置を変えることでその位置で鳴らすことができる
    /// </summary>
    /// <param name="seType"></param>
    /// <returns></returns>
    public static Se3d Se3dPlay(SE_NAME seType)
    {
        if (SE_Manager_ != null)
        {
            foreach (var se3d in SE_Manager_.se3D)
            {
                if (!se3d.se.isPlaying)
                {
                    se3d.se.loop = false;
                    var audioData = SE_Manager_.seData.GetTable()[seType];
                    se3d.se.volume = audioData.seVolume;
                    se3d.se.PlayOneShot(audioData.audio);
                    return se3d;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// SEを強制強的に止める
    /// </summary>
    /// <param name="se"></param>
    public static void ForcedPlayStop(AudioSource se)
    {
        if (se == null) return;
        se.Stop();
    }

    /// <summary>
    /// フェードアウト処理(呼び出すのは一度だけでよい)
    /// behaviourはthis すれば大丈夫
    /// </summary>
    /// <param name="behaviour"></param>
    /// <param name="se"></param>
    /// <param name="speed"></param>
    public static void SetFadeOut(MonoBehaviour behaviour, AudioSource se, float speed = 0.5f)
    {
        if (se == null) return;
        behaviour.StartCoroutine(OnFadeOut(se, speed));
    }

    /// <summary>
    /// SEをループする
    /// </summary>
    /// <param name="se"></param>
    public static void OnLoop(AudioSource se)
    {
        if (se == null) return;
        se.loop = true;
    }

    //フェードアウト処理
    static IEnumerator OnFadeOut(AudioSource se, float speed = 0.5f)
    {
        while (se.volume != 0f)
        {
            se.volume -= speed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        se.Stop();
    }

    [System.Serializable]
    public class PrefabDictionary : Serialize.TableBase<SE_NAME, SeData, Name2Prefab> { }

    [System.Serializable]
    public class Name2Prefab : Serialize.KeyAndValue<SE_NAME, SeData>
    {
        public Name2Prefab(SE_NAME key, SeData value) : base(key, value) { }
    }
}
