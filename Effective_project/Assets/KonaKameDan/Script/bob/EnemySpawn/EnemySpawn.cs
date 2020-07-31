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
    StopWatch timer;
    public static int EnemyCount = 0;
    public static int EnemyLimit = 5;

    private void Start()
    {
        enemyOccurrence = GetComponent<EnemyOccurrence>();
        timer = gameObject.AddComponent<StopWatch>();
        timer.LapTime = 5;
        timer.LapEvent = Spawn;
    }

    void Spawn()
    {
        if (EnemyCount >= EnemyLimit) return;

        playerPos = PlayerManager.GetManager.GetPlObj;

        SpawnPos = new Vector3(Random.Range(-SpawnRange + playerPos.transform.position.x, SpawnRange + playerPos.transform.position.x),
                               posY + playerPos.transform.position.y,
                               Random.Range(-SpawnRange + playerPos.transform.position.z, SpawnRange + playerPos.transform.position.z));

        enemyOccurrence.EnemyGenerate(SpawnPos);
        EnemyCount++;
    }
}
