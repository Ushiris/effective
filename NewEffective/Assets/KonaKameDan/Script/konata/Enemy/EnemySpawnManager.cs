﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] Enemy[] enemyArr;

    static List<Enemy> enemyList = new List<Enemy>();

    static readonly int maxCount = 50;

    // Start is called before the first frame update
    void Start()
    {
        enemyList.Clear();

        int count;
        for (count = 0; count < enemyArr.Length; count++)
        {
            var enemy = Instantiate(enemyArr[count].gameObject, transform);
            enemyList.Add(enemy.GetComponent<Enemy>());
            
        }

        for (; count < maxCount; count++)
        {
            var ranNum = Random.Range(0, enemyArr.Length);
            var enemy = Instantiate(enemyArr[ranNum].gameObject, transform);
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
}
