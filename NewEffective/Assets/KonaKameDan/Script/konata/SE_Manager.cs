using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 流したい音をセットして、別のスクリプトで呼べるようにするやつ
/// </summary>
public class SE_Manager : MonoBehaviour
{
    [SerializeField] int audioInstantMaxCount = 10;

    AudioSource[] seArr;

    /// <summary>
    /// SEの種類名
    /// </summary>
    public enum SE_NAME
    {
        Hit, Shot, CastArts, Heel, EffectPlate_EffectObject_Set, EffectPlate_ArtsName_Set, EffectPlate_Switching, Nothing
    }

    [System.Serializable]
    public class SeData
    {
        public AudioClip audio;
        [Range(0, 1f)] public float seVolume = 0.1f;
    }

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

        for (int i = 0; i < audioInstantMaxCount; i++) seArr[i].volume = 0.1f;

        SE_Manager_ = this;
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
