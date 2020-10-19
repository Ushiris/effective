using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Artsをプール管理class
/// </summary>
public class StartUpParticle : MonoBehaviour
{
    [SerializeField] List<GameObject> artsPrefab = new List<GameObject>();

    static Dictionary<string, List<ArtsStatus>> artsPool = new Dictionary<string, List<ArtsStatus>>();

    static readonly int maxArtsPoolCount = 10;

    private void Start()
    {
        foreach(var prefab in artsPrefab)
        {
            var artsStatus = prefab.GetComponent<ArtsStatus>();
            if (artsStatus == null) prefab.AddComponent<ArtsStatus>();
            prefab.tag = "Arts";

            artsPool.Add(prefab.name, SetArtsStatus(prefab));
        }
        artsPrefab.Clear();
    }

    //辞書にArtsをセットする
    List<ArtsStatus> SetArtsStatus(GameObject prefab)
    {
        List<ArtsStatus> artsStatusList = new List<ArtsStatus>();
        for(int i = 0; i < maxArtsPoolCount; i++)
        {
            var obj = Instantiate(prefab, transform);
            artsStatusList.Add(obj.GetComponent<ArtsStatus>());
            obj.SetActive(false);
        }
        return new List<ArtsStatus>(artsStatusList);
    }

    /// <summary>
    /// Arts名からプールしてあるプレハブを持ってくる
    /// </summary>
    /// <param name="artsPrefabName"></param>
    /// <returns></returns>
    public static ArtsStatus GetArts(string artsPrefabName)
    {
        if (artsPool[artsPrefabName].Count == 0) return null;
        var artsStatus = artsPool[artsPrefabName][0];
        artsPool[artsPrefabName].RemoveAt(0);
        return artsStatus;
    }

    /// <summary>
    /// Artsをテーブルに返す
    /// </summary>
    /// <param name="artsPrefabName"></param>
    /// <param name="artsPrefab"></param>
    public static void SetArts(string artsPrefabName, ArtsStatus artsStatus)
    {
        artsPool[artsPrefabName].Add(artsStatus);
        artsStatus.gameObject.SetActive(false);
    }
}
