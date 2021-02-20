using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStage : MonoBehaviour
{
    [SerializeField] PrefabDictionary data;

    static Dictionary<NewMap.MapType, GameObject> staticData;
    static GameObject myObj;

    public static bool isDebug { get; set; }

    static readonly NewMap.MapType defaultMap = NewMap.MapType.Grassland;

    private void Awake()
    {
        staticData = new Dictionary<NewMap.MapType, GameObject>(data.GetTable());
        NewMap.SetSelectMapType = defaultMap;
        transform.parent = null;
        myObj = gameObject;
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

    void AllHide()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// テラインのデータを取得する
    /// </summary>
    /// <param name="mapType"></param>
    /// <returns></returns>
    public static GameObject GetTerrainData(NewMap.MapType mapType)
    {
        foreach (var key in staticData.Keys)
        {
            staticData[key].SetActive(false);
        }

        staticData[mapType].gameObject.SetActive(true);

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
