using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectManager : MonoBehaviour
{
    [SerializeField] StageSelectUiView[] stageSelectUiViews;

    [System.Serializable]
    public class StageStatus
    {
        public Texture image;
    }

    [SerializeField] PrefabDictionary data;

    // Start is called before the first frame update
    void Start()
    {
        List<NewMap.MapType> ranTable = new List<NewMap.MapType>();
        foreach(var stage in data.GetTable())
        {
            ranTable.Add(stage.Key);
        }

        for (int i = 0; i < stageSelectUiViews.Length; i++)
        {
            var num = Random.Range(0, ranTable.Count);
            var ran = ranTable[num];
            var stage = data.GetTable()[ran];
            stageSelectUiViews[i].SetStageStatus(stage.image, ran);
            ranTable.RemoveAt(num);
        }
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    [System.Serializable]
    public class PrefabDictionary : Serialize.TableBase<NewMap.MapType, StageStatus, Name2Prefab> { }

    [System.Serializable]
    public class Name2Prefab : Serialize.KeyAndValue<NewMap.MapType, StageStatus>
    {
        public Name2Prefab(NewMap.MapType key, StageStatus value) : base(key, value) { }
    }
}
