using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStage : MonoBehaviour
{
    [SerializeField] PrefabDictionary data;

    static Dictionary<NewMap.MapType, GameObject> staticData;

    public static bool isDebug { get; set; }

    static readonly NewMap.MapType defaultMap = NewMap.MapType.Grassland;

    private void Awake()
    {
        staticData = new Dictionary<NewMap.MapType, GameObject>(data.GetTable());
        NewMap.SetSelectMapType = defaultMap;
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (isDebug)
        {
            staticData = new Dictionary<NewMap.MapType, GameObject>(data.GetTable());
        }
    }
#endif

    /// <summary>
    /// テラインのデータを取得する
    /// </summary>
    /// <param name="mapType"></param>
    /// <returns></returns>
    public static GameObject GetTerrainData(NewMap.MapType mapType)
    {
        GameObject tmpItem = null;
        foreach (var key in staticData.Keys)
        {
            var item = staticData[key];
            if (mapType == key)
            {
                item.SetActive(true);
                tmpItem = item;
            }
            else
            {
                if (item != null && tmpItem != item) item.SetActive(false);
            }
        }
        return staticData[mapType];
    }

    [System.Serializable]
    public class PrefabDictionary : Serialize.TableBase<NewMap.MapType, GameObject, Name2Prefab> { }

    [System.Serializable]
    public class Name2Prefab : Serialize.KeyAndValue<NewMap.MapType, GameObject>
    {
        public Name2Prefab(NewMap.MapType key, GameObject value) : base(key, value) { }
    }
}
