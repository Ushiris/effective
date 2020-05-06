using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// テキストの読み込み
/// </summary>
public class TextInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// テキストからアーツのデータを読み込む
    /// </summary>
    /// <param name="textName">読み込むテキストの名前</param>
    /// <param name="datas">アーツデータ</param>
    /// <param name="isCreate">リスト作成前に読み込むか、作った後か</param>
    public static void ArtsListTextInput(string textName, List<ArtsList.ArtsData> datas, bool isCreate)
    {

        //テキストの読み込み
        TextAsset text = new TextAsset();
        text = Resources.Load(textName, typeof(TextAsset)) as TextAsset;

        //宣言
        string textAll = text.text;
        string[] textLoad = textAll.Split('\n');
        int count = 0;

        //テキストを最後まで読み込むよう
        foreach (string str in textLoad)
        {
            if (str == "") break;

            //カンマで区切る
            string[] arr = str.Split(',');

            //新規作成用
            if (isCreate)
            {
                datas.Add(new ArtsList.ArtsData());
            }

            //IDの取得
            datas[count].id = arr[(int)ArtsList.ArtsDataName.ID].Remove(0, 1);

            //名前の挿入
            datas[count].name = arr[(int)ArtsList.ArtsDataName.Name];

            //組み合わせの挿入
            for (int i = (int)ArtsList.ArtsDataName.EffectList; i < arr.Length; i++)
            {
                if (isCreate)
                {
                    datas[count].effectList.Add(0);
                }

                datas[count].effectList[datas[count].effectList.Count - 1] = int.Parse(arr[i]);
            }

            count++;
        }
    }
}
