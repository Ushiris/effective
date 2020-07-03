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
    }

    //アーツを生成
    public static void InstantArts(Transform artsPivot,string artsId)
    {
        var prefabs = GetArtsInstantManager.prefabs;
        switch (artsId)
        {
            case "045": Instantiate(prefabs.GetTable()["045"], artsPivot); break;
            case "04": Instantiate(prefabs.GetTable()["04"], artsPivot); break;
            case "05": Instantiate(prefabs.GetTable()["05"], artsPivot); break;
            case "45": Instantiate(prefabs.GetTable()["45"], artsPivot); break;
            default: break;
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
