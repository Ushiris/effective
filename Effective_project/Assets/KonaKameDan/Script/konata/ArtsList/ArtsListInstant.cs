using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// エフェクトの組み合わせ自動生成
/// </summary>
public class ArtsListInstant : MonoBehaviour
{

    /// <summary>
    /// エフェクトの組み合わせ自動生成
    /// </summary>
    /// <param name="maxNum">エフェクトの最大数</param>
    /// <param name="maxEffectNum">組み合わせの数</param>
    /// <returns></returns>
    public static List<ArtsList.ArtsData> InstantArtsList(int maxNum,int maxEffectNum)
    {
        List<ArtsList.ArtsData> artsDataList = new List<ArtsList.ArtsData>();
        int loopCount = 0;

        var nums = Enumerable.Range(0, maxNum).ToArray();
        int count = maxEffectNum;

        for (; ; )
        {
            if (count < 2) break;

            //コンビネーションの表を作成
            foreach (var n in Permutation.Enumerate(nums, count, false))
            {
                //リストを確保
                artsDataList.Add(new ArtsList.ArtsData());

                foreach (var x in n)
                {
                    //組み合わせ生成
                    artsDataList[loopCount].effectList.Add(x);

                    //IDの生成
                    artsDataList[loopCount].id += x;
                }

                //文字
                artsDataList[loopCount].name = RandomString.RandStr(5);

                loopCount++;
            }
            count--;
        }

        return new List<ArtsList.ArtsData>(artsDataList);
    }

}

/// <summary>
/// 自動文字生成
/// </summary>
public static class RandomString
{
    /// <summary>
    /// ランダムに文字を出す
    /// </summary>
    /// <param name="length">文の長さ</param>
    /// <returns></returns>
    public static string RandStr(int length)
    {
        const string PASSWORD_CHARS = "abcdefghijklmnopqrstuvwxyz";
        char[] cArr = new char[length];

        for (int i = 0; i < length; i++)
        {
            cArr[i] = PASSWORD_CHARS[Random.Range(0, PASSWORD_CHARS.Length)];
        }

        return new string(cArr);
    }
}

/// <summary>
/// パーミテーション、コンビネーション
/// </summary>
public static class Permutation
{
    /// <summary>
    /// コンビネーション、パーミテーション表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">アイテムの最大数</param>
    /// <param name="k">組み合わせの数</param>
    /// <param name="withRepetition">P:false,C:true</param>
    /// <returns></returns>
    public static IEnumerable<T[]> Enumerate<T>(IEnumerable<T> items, int k, bool withRepetition)
    {
        if (k == 1)
        {
            foreach (var item in items)
                yield return new T[] { item };
            yield break;
        }
        foreach (var item in items)
        {
            var leftside = new T[] { item };

            // item よりも前のものを除く （順列と組み合わせの違い)
            // 重複を許さないので、unusedから item そのものも取り除く
            var unused = withRepetition ? items : items.SkipWhile(e => !e.Equals(item)).Skip(1).ToList();

            foreach (var rightside in Enumerate(unused, k - 1, withRepetition))
            {
                yield return leftside.Concat(rightside).ToArray();
            }
        }
    }

    //コンビネーション計算
    public static int nCm(int n, int m)
    {
        if (m == 0) return 1;
        if (n == 0) return 0;
        return n * nCm(n - 1, m - 1) / m;
    }
}
