using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アーツの生成を管理
/// </summary>
public class ArtsInstantManager : MonoBehaviour
{

    public PrefabDictionary prefabs;

    static ArtsInstantManager GetArtsInstantManager;

    // Start is called before the first frame update
    void Start()
    {
        GetArtsInstantManager = this;

        foreach(var id in prefabs.GetTable().Keys)
        {
            if (prefabs.GetTable()[id].GetComponent<ArtsStatus>() == null)
            {
                prefabs.GetTable()[id].AddComponent<ArtsStatus>();
            }
        }
    }

    //アーツを生成
    public static void InstantArts(GameObject artsPivot, string artsId)
    {
        var prefabs = GetArtsInstantManager.prefabs;
        switch (artsId)
        {
            case "045": InstantArts(); break;
            case "04": InstantArts(); break;
            case "05": InstantArts(); break;
            case "45": InstantArts(); break;
            case "09": InstantArts(); break;
            case "049": InstantArts(); break;
            case "59": InstantArts(); break;
            case "059": InstantArts(); break;
            case "459": InstantArts(); break;
            case "49": InstantArts(); break;
            case "02": InstantArts(); break;
            case "024": InstantArts(); break;
            case "025": InstantArts(); break;
            case "25": InstantArts(); break;
            case "29": InstantArts(); break;
            default: break;
        }

        void InstantArts()
        {
            //生成
            var obj = Instantiate(prefabs.GetTable()[artsId], artsPivot.transform);
            var artsStatus = obj.GetComponent<ArtsStatus>();

            //代入
            artsStatus.myEffectCount = artsPivot.GetComponentInParent<MyEffectCount>();
            artsStatus.myStatus = artsPivot.GetComponentInParent<Status>();

            if (artsPivot.transform.parent.gameObject.tag == "Player")
            {
                artsStatus.type = ArtsStatus.ParticleType.Player;
            }
            else if (artsPivot.transform.parent.gameObject.tag == "Enemy")
            {
                artsStatus.type = ArtsStatus.ParticleType.Enemy;
            }
        }
    }

    //デバッグと切り替える処理
    public static string SelectArts(string artsId, string debugNum = "0")
    {
        if (debugNum == "0" && artsId != null)
        {
            return artsId;
        }
        else return debugNum;
    }


    [System.Serializable]
    public class PrefabDictionary : Serialize.TableBase<string, GameObject, Name2Prefab> { }

    [System.Serializable]
    public class Name2Prefab : Serialize.KeyAndValue<string, GameObject>
    {
        public Name2Prefab(string key, GameObject value) : base(key, value) { }
    }
}
