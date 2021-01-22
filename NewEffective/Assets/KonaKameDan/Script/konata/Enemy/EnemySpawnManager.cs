using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] PrefabDictionary prefabs;

    static List<Enemy> enemyList = new List<Enemy>();

    static readonly int maxCount = 50;

    // Start is called before the first frame update
    void Start()
    {
        enemyList.Clear();

        var enemyNameArr = NewMap.GetEnemyType;
        int count;
        for (count = 0; count < enemyNameArr.Length; count++)
        {
            var enemyObj = prefabs.GetTable()[enemyNameArr[count]];
            var enemy = Instantiate(enemyObj, transform);
            enemyList.Add(enemy.GetComponent<Enemy>());
        }

        for (; count < maxCount; count++)
        {
            var ranNum = Random.Range(0, enemyNameArr.Length);
            var enemyObj = prefabs.GetTable()[enemyNameArr[ranNum]];
            var enemy = Instantiate(enemyObj, transform);
            enemyList.Add(enemy.GetComponent<Enemy>());
        }

        enemyList.ForEach(item =>
        {
            item.gameObject.SetActive(false);
        });
    }

    /// <summary>
    /// エネミーの情報を取得する
    /// </summary>
    /// <returns></returns>
    public static Enemy GetEnemy()
    {
        if (enemyList.Count == 0) return null;
        var ran = Random.Range(0, enemyList.Count);
        var enemy = enemyList[ran];
        enemyList.RemoveAt(ran);
        return enemy;
    }

    /// <summary>
    /// エネミーをテーブルに戻す
    /// </summary>
    /// <param name="enemy"></param>
    public static void SetEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        enemyList.Add(enemy);
    }

    [System.Serializable]
    public class PrefabDictionary : Serialize.TableBase<NameDefinition.EffectName, GameObject, Name2Prefab> { }

    [System.Serializable]
    public class Name2Prefab : Serialize.KeyAndValue<NameDefinition.EffectName, GameObject>
    {
        public Name2Prefab(NameDefinition.EffectName key, GameObject value) : base(key, value) { }
    }
}
