using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// テキスト書き出し
/// </summary>
public class ArtsListOutput : MonoBehaviour
{

    /// <summary>
    /// テキストに書き出す
    /// </summary>
    /// <param name="artsDataList"></param>
    public static void TextOutput(string textLink, List<ArtsList.ArtsData> artsDataList)
    {
        File.WriteAllLines(textLink, ArtsDataChangeStr(artsDataList));
    } 

    /// <summary>
    /// アーツリストをストリングにする
    /// </summary>
    /// <param name="artsDataList"></param>
    /// <returns></returns>
    static List<string> ArtsDataChangeStr(List<ArtsList.ArtsData> artsDataList)
    {
        List<string> strList = new List<string>();

        foreach (var a in artsDataList)
        {
            //テキストに書き込む順番
            string str = "'" + a.id + "," + a.name + "," + ChangeStrArr(a.effectList);


            str = str.Trim(',');

            strList.Add(str);
        }

        return new List<string>(strList);
    }

    /// <summary>
    /// 配列をstringに
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">stringにしたい配列</param>
    /// <returns></returns>
    static string ChangeStrArr<T>(IEnumerable<T> items)
    {
        string str = "";

        foreach (var a in items)
        {
            str += a + ",";
        }

        return str;
    }
}