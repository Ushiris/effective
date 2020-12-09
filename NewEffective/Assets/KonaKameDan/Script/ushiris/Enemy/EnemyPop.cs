using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPop : MonoBehaviour
{
    Transform playerTransform;
    StopWatch timer;
    int findPointLoopLimiter = 5;
    float differPos=5;

    private void Awake()
    {
        timer = StopWatch.Summon(Random.Range(GameBaranceManager.EnemyPopFast, GameBaranceManager.EnemyPopFast),StopWatch.voidAction, gameObject);
        timer.LapEvent = () => { 
            EnemySpawn();
            timer.LapTime = Random.Range(GameBaranceManager.EnemyPopFast, GameBaranceManager.EnemyPopFast); 
        };
        timer.IsActive = false;
    }

    private void Start()
    {
        playerTransform = PlayerManager.GetManager.GetPlObj.transform;
        timer.IsActive = true;
    }

    void EnemySpawn()
    {
        var pos = SpawnPointFinder();
        if (pos == null) return;

        var enemy = EnemySpawnManager.GetEnemy();
        if (enemy == null) return;

        enemy.transform.position = (Vector3)pos;
        enemy.life.AddLastword(() => EnemySpawnManager.SetEnemy(enemy));
        enemy.gameObject.SetActive(true);
    }

    Vector3 SpawnPointSelector()
    {
        Vector3 desirePos = playerTransform.position;
        float angle = playerTransform.rotation.eulerAngles.y + Random.Range(-0.4f * Mathf.PI, 0.4f * Mathf.PI);
        float distance = Random.Range(GameBaranceManager.EnemyPopNear, GameBaranceManager.EnemyPopFar);
        desirePos.x += Mathf.Cos(angle) * distance;
        desirePos.z += Mathf.Sin(angle) * distance;

        return desirePos;
    }

    Vector3? SpawnPointFinder()
    {
        Vector3 desirePos = SpawnPointSelector();
        NavMeshHit edgePos;
        if (NavMesh.FindClosestEdge(desirePos, out edgePos, NavMesh.AllAreas))
        {
            return edgePos.position;
        }

        return null;
    }

    bool IsOutHeight(Terrain terrain,Vector3 desirePos)
    {
        return terrain.terrainData.GetInterpolatedHeight(desirePos.x / terrain.terrainData.size.x, desirePos.z / terrain.terrainData.size.z) >= 15;
    }
}
