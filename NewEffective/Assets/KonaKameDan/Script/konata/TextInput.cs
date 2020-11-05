using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// テキストの読み込み
/// </summary>
public class TextInput : MonoBehaviour
{
    /// <summary>
    /// テキストからアーツデータを取得
    /// </summary>
    /// <param name="artsIDTextName">エフェクトの組み合わせが記録されているテキスト</param>
    /// <param name="artsActionTextName">アクションを記録されているテキスト</param>
    /// <param name="datas"></param>
    /// <param name="isCreate">Listが作られる前かどうか</param>
    public static void ArtsDataBaseInput(string artsIDTextName, string artsActionTextName, List<ArtsList.ArtsData> datas, bool isCreate)
    {
        ArtsListTextInput(artsIDTextName, datas, isCreate);
        ActionNamesTextInput(artsActionTextName, datas, isCreate);
    }

    public static void ActionNamesTextInput(string textName, List<ArtsList.ArtsData> datas, bool isCreate)
    {
        //テキストの読み込み
        TextAsset text = new TextAsset();
        text = Resources.Load(textName, typeof(TextAsset)) as TextAsset;

        //宣言
        string textAll = text.text;
        string[] textLoad = textAll.Split('\n');
        int count = 0;

        foreach (string str in textLoad)
        {
            if (str == "") break;

            //カンマで区切る
            string[] arr = str.Split(',');

            string s = arr[(int)ArtsList.ArtsDataName.ID].TrimStart('\'');

            //IDと一致するものをArtsListに格納する
            if (datas[count].id == s)
            {
                for (int i = (int)ArtsList.ArtsDataName.ID + 1; i < arr.Length; i++)
                {
                    if (arr[i].Trim().Length == 0) break; //何もない場合ループから抜け出す
                    if (isCreate)
                    {
                        datas[count].actionNames.Add("");
                    }

                    datas[count].actionNames[datas[count].actionNames.Count - 1] = arr[i];

                }
            }

            count++;
        }
    }

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
            datas[count].id = arr[(int)ArtsList.ArtsDataName.ID].TrimStart('\'');

            //名前の挿入
            datas[count].name = arr[(int)ArtsList.ArtsDataName.Name];

            //組み合わせの挿入
            for (int i = (int)ArtsList.ArtsDataName.EffectList; i < arr.Length; i++)
            {
                if (arr[i].Trim().Length == 0) break; //何もない場合ループから抜け出す
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
