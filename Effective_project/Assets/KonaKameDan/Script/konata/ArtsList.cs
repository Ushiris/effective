using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// エフェクトの組み合わせ自動生成
/// </summary>
public class ArtsList : MonoBehaviour
{
    [SerializeField] int maxNum = 10;
    [SerializeField] int maxEffectNum = 3;
    [SerializeField] List<int> numList = new List<int>();

    int loopCount, count;

    [System.Serializable]
    public class ArtsData
    {
        public string name;
        public List<int> effectList = new List<int>();
    }
    public List<ArtsData> artsDataList = new List<ArtsData>();

    // Start is called before the first frame update
    void Start()
    {
        var nums = Enumerable.Range(0, maxNum).ToArray();
        count = maxEffectNum;


        for (; ; )
        {
            if (count < 1) break;

            //コンビネーションの表を作成
            foreach (var n in Permutation.Enumerate(nums, count, false))
            {
                //リストを確保
                artsDataList.Add(new ArtsData());

                foreach (var x in n)
                {
                    //組み合わせ生成
                    artsDataList[loopCount].effectList.Add(x);
                }

                //文字
                artsDataList[loopCount].name = RandomString(5);

                loopCount++;
            }
            count--;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //コンビネーション計算
    int nCm(int n, int m)
    {
        if (m == 0) return 1;
        if (n == 0) return 0;
        return n * nCm(n - 1, m - 1) / m;
    }
    
    //自動文字生成
    string RandomString(int length)
    {
       const string PASSWORD_CHARS ="abcdefghijklmnopqrstuvwxyz";
        char[] cArr=new char[length];

        for (int i = 0; i < length; i++)
        {
            cArr[i] = PASSWORD_CHARS[Random.Range(0, PASSWORD_CHARS.Length)];
        }

        return new string(cArr);
    }

}

//コンビネーション表作成
public static class Permutation
{
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
}
