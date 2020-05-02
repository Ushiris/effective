using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtsList : MonoBehaviour
{
    [SerializeField] int maxNum = 10;
    [SerializeField] int maxEffectNum = 3;

    public static string textLink = "Assets/Resources/ArtsList.txt";

    [System.Serializable]
    public class ArtsData
    {
        public string name;
        public List<int> effectList = new List<int>();
    }
    public List<ArtsData> artsDataList = new List<ArtsData>();

    public static ArtsList GetArtsList;

    // Start is called before the first frame update
    void Start()
    {
        GetArtsList = this;

        //組み合わせを生成する
        artsDataList = new List<ArtsData>(ArtsListInstant.InstantArtsList(maxNum, maxEffectNum));

        //テキストに書き出し
        ArtsListOutput.TextOutput(artsDataList);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
