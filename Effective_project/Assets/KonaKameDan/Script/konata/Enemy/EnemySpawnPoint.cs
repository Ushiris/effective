using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    float siz;
    bool isEnemyActive;
    Enemy[] enemyArr = new Enemy[kMaxCount];
    List<Vector3> spawnPos = new List<Vector3>();

    static readonly int kMaxCount = 10;
    static readonly int kEventCheckLoopCount = 30;

    public static bool isAreaEnabled = false;
    public static bool isSpawnEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        siz = transform.localScale.x / 2;

        GetComponent<MeshRenderer>().enabled = isAreaEnabled;
        var c = GetComponent<Collider>();
        c.enabled = isSpawnEnabled;

        //敵が出現するエリアを決める
        var pos = transform.position;

        //敵を設置できるところ追加
        for (int x = FixPosX(-siz + pos.x); x < FixPosX(siz + pos.x); x++)
        {
            for (int z = FixPosZ(-siz + pos.z); z < FixPosZ(siz + pos.z); z++)
            {
                Vector3 v3 = new Vector3(x, 0, z);
                if (c.ClosestPoint(v3) == v3 && NewMapManager.GetEventPos[x, z])
                {
                    v3.y = NewMapTerrainData.GetTerrain.terrainData.GetHeight(x, z);
                    spawnPos.Add(v3);
                }
            }
        }
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
                    var ranNum = Random.Range(0, spawnPos.Count);
                    enemy.gameObject.transform.position = spawnPos[ranNum];
                    enemyArr[i] = enemy;
                }
            }
            else
            {
                //エネミーの表示を消す
                if (enemyArr[i].isDeath)
                {
                    EnemySpawnManager.SetEnemy(enemyArr[i]);
                    enemyArr[i] = null;
                }
            }
        }
    }

    int FixPosX(float x)
    {
        return  Mathf.Clamp((int)x, 0, NewMapManager.GetEventPos.GetLength(0) - 1);
    }

    int FixPosZ(float z)
    {
        return Mathf.Clamp((int)z, 0, NewMapManager.GetEventPos.GetLength(1) - 1);
    }
}
