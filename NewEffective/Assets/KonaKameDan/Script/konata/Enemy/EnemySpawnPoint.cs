﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    bool isEnemyActive;
    List<Enemy> enemyList = new List<Enemy>();
    [SerializeField] List<Vector3> spawnPos = new List<Vector3>();

    static readonly int kDefaultCount = 9;
    static readonly int kEventCheckLoopCount = 30;

    public static bool isAreaEnabled = false;
    public static bool isSpawnEnabled = true;

    // Start is called before the first frame update
    void Awake()
    {
        int siz = (int)(transform.localScale.x / 2);

        GetComponent<MeshRenderer>().enabled = isAreaEnabled;
        var c = GetComponent<Collider>();
        c.enabled = isSpawnEnabled;

        //敵が出現するエリアを決める
        var pos = transform.position;

        //ナビメッシュがセッティングされた後実行される
        NewMap.SetMapEventStartUp += () =>
        {
            //敵を設置できるところ追加
            for (int x = -siz + (int)pos.x; x < siz + (int)pos.x; x++)
            {
                for (int z = -siz + (int)pos.z; z < siz + (int)pos.z; z++)
                {
                    if (x % NewMap.kMapEventRange == 0 && z % NewMap.kMapEventRange == 0)
                    {
                        for (int y = NewMap.GetUnderLimit; y < (int)NewMap.GetMapMaxHeight; y++)
                        {
                            var v3 = new Vector3(x, y, z);

                            //座標がコリジョン内であることとナビメッシュの上であること
                            if (c.ClosestPoint(v3) == v3 && NewMap.GetNavMeshHeight.ContainsKey(v3))
                            {
                                v3.y = NewMap.GetNavMeshHeight[v3] + 2;
                                spawnPos.Add(v3);
                            }
                        }
                    }
                }
            }
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var isSePlay = false;
            var maxCount = kDefaultCount + WorldLevel.GetWorldLevel;
            for (int i = enemyList.Count; i < maxCount; i++)
            {
                var enemy = EnemySpawnManager.GetEnemy();
                if (enemy == null) break;

                if (!isSePlay)
                {
                    SE_Manager.SePlay(SE_Manager.SE_NAME.EnemySpawn);
                    isSePlay = true;
                }

                var ranNum = Random.Range(0, spawnPos.Count);
                enemy.gameObject.transform.position = spawnPos[ranNum];
                enemyList.Add(enemy);
                enemy.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i].IsDeath)
                {
                    EnemySpawnManager.SetEnemy(enemyList[i]);
                    enemyList[i] = null;
                }
            }

            if (enemyList.Count != 0)
            {
                enemyList.RemoveAll(item => item == null);
            }
        }
    }

    void OnEnemyActive(bool isActive)
    {
        for (int i = 0; i < kDefaultCount; i++)
        {
            if (isActive)
            {
                //エネミーを表示する
                if (enemyList[i] == null)
                {
                    var enemy = EnemySpawnManager.GetEnemy();
                    if (enemy == null) break;
                    var ranNum = Random.Range(0, spawnPos.Count);
                    enemy.gameObject.transform.position = spawnPos[ranNum];
                    enemyList[i] = enemy;
                    enemy.gameObject.SetActive(true);
                }
            }
            else
            {
                //エネミーの表示を消す
                if (enemyList[i] != null)
                {
                    if (enemyList[i].IsDeath)
                    {
                        EnemySpawnManager.SetEnemy(enemyList[i]);
                        enemyList[i] = null;
                    }
                }
            }
        }
    }
}
