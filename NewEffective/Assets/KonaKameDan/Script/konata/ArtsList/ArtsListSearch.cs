using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// アーツデータから目的の物を探す
/// </summary>
public class ArtsListSearch : MonoBehaviour
{
    static List<string> nameList = new List<string>();
    static List<string> idList = new List<string>();

    /// <summary>
    ///配列を入れその中に格納されているものから検索してArtsDataListの配列番号を返す
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    /// <returns></returns>
    public static int GetArtsNumListChangeStr<T>(IEnumerable<T> items)
    {      
        return GetArtsNum(ListChangeStr(items));
    }

    /// <summary>
    /// 名前またはIDを入力すると一致している場合ArtsDataListの配列番号を返す
    /// IDの場合、番号がバラバラでも検索をかけてくれる
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static int GetArtsNum(string item)
    {
        int num = nameList.IndexOf(item);

        //名前での検索
        if (-1 != num) return num;
        else
        {
            //IDでの検索
            num = idList.IndexOf(MySort.strSort(item));
            if (num == -1) Debug.LogError("サーチ先がありません");
        }

        return num;
    }

    /// <summary>
    /// 配列をstringに変換する
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">変換対象</param>
    /// <returns></returns>
    public static string ListChangeStr<T>(IEnumerable<T> items)
    {
        string str = "";

        foreach (var item in items)
        {
            str += item;
        }

        return str;
    }

    /// <summary>
    /// 名前とIDの検索用変数の作成
    /// </summary>
    /// <param name="datas"></param>
    public static void SearchSetting(List<ArtsList.ArtsData> datas)
    {
        //初期化
        nameList.Clear();
        idList.Clear();

        foreach (var data in datas)
        {
            nameList.Add(data.name);

            idList.Add(MySort.strSort(data.id));
        }
    }
}

public static class MySort
{
    /// <summary>
    /// stringのソート
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static string strSort(string item)
    {
        char[] c = item.ToCharArray();
        Array.Sort(c);
        return string.Join(null, c);
    }
}
