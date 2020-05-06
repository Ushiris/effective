using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArtsListSearch : MonoBehaviour
{
    new static List<string> nameList = new List<string>();
    static List<string> idList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

        if (-1 != num) return num;  //名前での検索
        else
        {
            num = Judge(item);      //IDでの検索
        }

        return num;

        int Judge(string str)
        {
            //文字のソート
            char[] strArr = str.ToCharArray();
            Array.Sort(strArr);
            str = string.Join(null, strArr);

            int count = 0;

            //リストの中身と一致するものがあれば、配列番号を返す
            foreach (var id in idList)
            {
                if (id == str)
                {
                    return count;
                }
                count++;
            }
            return -1;
        }
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
        foreach(var data in datas)
        {
            nameList.Add(data.name);

            //IDのソート
            char[] idArr = data.id.ToCharArray();
            Array.Sort(idArr);

            idList.Add(string.Join(null, idArr));
        }
    }
}
