using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject[] enemyArr;

    float siz;
    bool isEnemyActive;
    List<GameObject> enemyList = new List<GameObject>();

    static readonly int kMaxCount = 3;

    // Start is called before the first frame update
    void Start()
    {
        siz = transform.localScale.x / 2;
        for(int i=0;i< kMaxCount; i++)
        {
            var ranNum = Random.Range(0, enemyArr.Length);
            enemyArr[ranNum].SetActive(false);
            var obj = Instantiate(enemyArr[ranNum], SpawnPos(transform.position), Quaternion.identity);

            //世界のレベルをエネミーのステータスに入れる 
            var status = obj.GetComponent<Status>();
            status.Lv = WorldLevel.GetWorldLevel;

            enemyList.Add(obj);
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

    Vector3 SpawnPos(Vector3 pos)
    {
        int x = (int)Random.Range(-siz + pos.x, siz + pos.x);
        int z = (int)Random.Range(-siz + pos.z, siz + pos.z);
        var y = NewMapTerrainData.GetTerrain.terrainData.GetHeight(x, z);
        return new Vector3(x, y, z);
    }

    void OnEnemyActive(bool isActive)
    {
        for (int i = 0; i < kMaxCount; i++)
        {
            enemyList[i].SetActive(isActive);
        }
    }
}
