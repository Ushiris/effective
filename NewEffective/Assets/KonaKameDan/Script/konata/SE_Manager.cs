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
        Hit, Shot, CastArts, Heel
    }

    //[System.Serializable]
    //public class SeData
    //{
    //    [HideInInspector] public string name;
    //    public AudioClip audio;
    //}
    //public List<SeData> seDataList = new List<SeData>()
    //{
    //    //リストの初期化
    //    new SeData{name=SE_NAME.Hit.ToString(),audio=null },
    //    new SeData{name=SE_NAME.Shot.ToString(),audio=null },
    //};

    public PrefabDictionary seData;

    static SE_Manager SE_Manager_;

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
    public static void SePlay(SE_NAME seType)
    {
        //被って流せる音は最大10つまで
        //再生中でないオーディオを探す
        if (SE_Manager_ != null)
        {
            foreach (AudioSource se in SE_Manager_.seArr)
            {
                if (!se.isPlaying)
                {
                    se.PlayOneShot(SE_Manager_.seData.GetTable()[seType]);
                    break;
                }
            }
        }
    }

    [System.Serializable]
    public class PrefabDictionary : Serialize.TableBase<SE_NAME, AudioClip, Name2Prefab> { }

    [System.Serializable]
    public class Name2Prefab : Serialize.KeyAndValue<SE_NAME, AudioClip>
    {
        public Name2Prefab(SE_NAME key, AudioClip value) : base(key, value) { }
    }
}
