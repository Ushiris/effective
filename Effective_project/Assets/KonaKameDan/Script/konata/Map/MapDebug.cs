using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// マップのデバッグ用
/// </summary>
public class MapDebug : MonoBehaviour
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
    /// マップデータをテキストに書き出す
    /// </summary>
    /// <param name="mapData">マップデータ</param>
    /// <param name="textLink">保存場所</param>
    public static void TextOutput(Map.ObjType[,] mapData,string textLink)
    {
        int w = mapData.GetLength((int)Map.MapDataArrLength.Width);
        int d = mapData.GetLength((int)Map.MapDataArrLength.Depth);

        List<string> text = new List<string>();

        //オブジェクト設置
        for (int x = 1; x < w - 1; x++)
        {
            text.Add("");
            for (int z = 1; z < d - 1; z++)
            {
                int num = (int)mapData[x, z];
                text[text.Count - 1] += " " + num.ToString();
            }
        }

        File.WriteAllLines(textLink, text);
    }
}
