using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    float siz;
    bool isEnemyActive;
    Enemy[] enemyArr = new Enemy[kMaxCount];

    static readonly int kMaxCount = 5;

    public static bool isAreaEnabled = false;
    public static bool isSpawnEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        siz = transform.localScale.x / 2;

        GetComponent<MeshRenderer>().enabled = isAreaEnabled;
        GetComponent<Collider>().enabled = isSpawnEnabled;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        { 
            OnEnemyActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            OnEnemyActive(false);
        }
    }
    //ポジションをランダムに出す
    Vector3 SpawnPos(Vector3 pos)
    {
        int x = (int)Random.Range(-siz + pos.x, siz + pos.x);
        int z = (int)Random.Range(-siz + pos.z, siz + pos.z);
        var y = NewMapTerrainData.GetTerrain.terrainData.GetHeight(x, z);
        return new Vector3(x, y, z);
    }

    void OnEnemyActive(bool isActive)
    {
        var pos = transform.position;
        for (int i = 0; i < kMaxCount; i++)
        {
            if (isActive)
            {
                //エネミーを表示する
                if (enemyArr[i] == null)
                {
                    var enemy = EnemySpawnManager.GetEnemy();
                    enemy.gameObject.transform.position = SpawnPos(pos);
                    enemyArr[i] = enemy;
                }
            }
            else
            {
                //エネミーの表示を消す
                if (!enemyArr[i].IsInjured)
                {
                    EnemySpawnManager.SetEnemy(enemyArr[i]);
                    enemyArr[i] = null;
                }
            }
        }
    }
}
