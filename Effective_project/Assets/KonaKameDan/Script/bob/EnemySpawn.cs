using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private EnemyOccurrence enemyOccurrence;
    private Vector3 SpawnPos;
    public int SpawnRange = 5;// enemyスポーン範囲
    public int posY = 1;// enemyスポーン高さ

    private void Start()
    {
        enemyOccurrence = GetComponent<EnemyOccurrence>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnPos = new Vector3(Random.Range(-SpawnRange, SpawnRange), posY, Random.Range(-SpawnRange, SpawnRange));
            enemyOccurrence.EnemyGenerate(SpawnPos);
        }
    }
}
