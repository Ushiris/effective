using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アーツリスト操作用
/// </summary>
public class ArtsList : MonoBehaviour
{
    //クラスの順番表
    public enum ArtsDataName
    {
        ID, Name, EffectList
    }

    [SerializeField] int maxNum = 10;
    [SerializeField] int maxEffectNum = 3;

    string textLink = "Assets/Resources/ArtsList.csv";
    string artsListTextName = "ArtsList";

    string actionNamesTextLink = "Assets/Resources/ActionName.csv";
    string actionNamesArtsListTextName = "ArtsListActionName";

    [System.Serializable]
    public class ArtsData
    {
        public string name;
        public string id;
        public List<int> effectList = new List<int>();  //最後にすること(順番)
        public List<string> actionNames = new List<string>();
    }
    public List<ArtsData> artsDataList = new List<ArtsData>();

    //渡すよう
    public static ArtsList GetArtsList;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private void Awake()
    {
        if (GetArtsList == null)
        {
            ArtsListTextInput(true);
            ArtsListSearchSetUp();
            GetArtsList = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //ArtsDataInstant();
        //TextOutput();


    }

    // Update is called once per frame

    //組み合わせを生成する
    void ArtsDataInstant()
    {
        artsDataList = new List<ArtsData>(ArtsListInstant.InstantArtsList(maxNum, maxEffectNum));
    }

    //テキストに書き出し
    void TextOutput()
    {
        ArtsListOutput.TextOutput(textLink, artsDataList);
    }

    //テキストの読み込み
    void ArtsListTextInput(bool newCreate)
    {
        TextInput.ArtsDataBaseInput(artsListTextName, actionNamesArtsListTextName, artsDataList, newCreate);
    }

    //検索用の起動
    void ArtsListSearchSetUp()
    {
        ArtsListSearch.SearchSetting(artsDataList);
    }

    /// <summary>
    /// 名前またはIDから条件に合ったアーツデータを返す
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="time"></param>
    /// <returns></returns>
    public static ArtsData GetLookedForArts<T>(IEnumerable<T> itme)
    {
        int num = ArtsListSearch.GetArtsNum(string.Join(null, itme));
        return GetArtsList.artsDataList[num];
    }

    /// <summary>
    /// 選択したものからアーツデータを持ってくる
    /// </summary>
    public static ArtsData GetSelectArts
    {
        get
        {
            string data = string.Join(null, UI_Manager.GetEffectFusionUI_ChoiceNum.numList);
            int num = ArtsListSearch.GetArtsNum(data);
            return GetArtsList.artsDataList[num];
        }
    }
}
