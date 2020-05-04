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
        Name, EffectList
    }

    [SerializeField] int maxNum = 10;
    [SerializeField] int maxEffectNum = 3;

    public static string textLink = "Assets/Resources/ArtsList.txt";
    string ArtsListTextName = "ArtsList";

    [System.Serializable]
    public class ArtsData
    {
        public string name;
        public List<int> effectList = new List<int>();  //最後にすること(順番)
    }
    public List<ArtsData> artsDataList = new List<ArtsData>();

    public static ArtsList GetArtsList;

    // Start is called before the first frame update
    void Start()
    {
        GetArtsList = this;


        ArtsListTextInput(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ArtsDataInstant()
    {
        //組み合わせを生成する
        artsDataList = new List<ArtsData>(ArtsListInstant.InstantArtsList(maxNum, maxEffectNum));
    }

    void TextOutput()
    {
        //テキストに書き出し
        ArtsListOutput.TextOutput(artsDataList);
    }

    //テキストの読み込み
    void ArtsListTextInput(bool newCreate)
    {
        TextInput.ArtsListTextInput(ArtsListTextName, artsDataList, newCreate);
    }
}
