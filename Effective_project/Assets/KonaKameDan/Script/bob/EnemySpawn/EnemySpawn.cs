using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    private EnemyOccurrence enemyOccurrence;
    private Vector3 SpawnPos;
    public float SpawnRange = 5.0f;// enemyスポーン範囲
    public float posY = 0.0f;// enemyスポーン高さ]
    GameObject playerPos;// playerのポジション

    private void Start()
    {
        enemyOccurrence = GetComponent<EnemyOccurrence>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerPos = PlayerManager.GetManager.GetPlObj;

            SpawnPos = new Vector3(Random.Range(-SpawnRange + playerPos.transform.position.x, SpawnRange + playerPos.transform.position.x),
                                   posY + playerPos.transform.position.y, 
                                   Random.Range(-SpawnRange + playerPos.transform.position.z, SpawnRange + playerPos.transform.position.z));

            enemyOccurrence.EnemyGenerate(SpawnPos);
        }
    }
}
