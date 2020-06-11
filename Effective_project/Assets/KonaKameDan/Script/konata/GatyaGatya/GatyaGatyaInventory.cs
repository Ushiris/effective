using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エフェクトオブジェクトの出現確立
/// </summary>
public class GatyaGatyaInventory : MonoBehaviour
{
    [System.Serializable]
    public class ItemClass
    {
        public GameObject item;
        [Header("出現割合"),RangeAttribute(0, 10)] public float probability;
    }
    public List<ItemClass> EffectObj = new List<ItemClass>();

    List<int> probabilityTable = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// 出現割合からランダムテーブル作成
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    public static List<int> RandomTableInstant(List<ItemClass> items)
    {
        List<int> table = new List<int>();

        for (int i = 0; i < items.Count; i++)
        {
            for (int f = 0; f < items[i].probability; f++)
            {
                table.Add(i);
            }
        }

        return new List<int>(table);
    }

    /// <summary>
    /// ランダムにオブジェクトを返す
    /// </summary>
    /// <param name="randomTable"></param>
    /// <param name="items"></param>
    /// <returns></returns>
    public static GameObject RandomObj(List<int> randomTable, List<ItemClass> items)
    {
        int ran = Random.Range(0, randomTable.Count - 1);
        int num = randomTable[ran];
        return items[num].item;
    }

    /// <summary>
    /// ランダムに複数のオブジェクトを返す
    /// </summary>
    /// <param name="randomTable"></param>
    /// <param name="items"></param>
    /// <param name="maxCount"></param>
    /// <returns></returns>
    public static List<GameObject> RandomObjList(List<int> randomTable, List<ItemClass> items,int maxCount)
    {
        List<GameObject> objs = new List<GameObject>();

        for(int i=0;i< maxCount; i++)
        {
            objs.Add(RandomObj(randomTable, items));
        }

        return new List<GameObject>(objs);
    }
}
