using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPop : MonoBehaviour
{
    Transform playerTransform;
    StopWatch timer;
    int findPointLoopLimiter = 5;
    float differPos=5;

    private void Awake()
    {
        timer = StopWatch.Summon(Random.Range(GameBaranceManager.EnemyPopFast, GameBaranceManager.EnemyPopFast),()=> { }, gameObject);
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

    Vector3? SpawnPointFinder(int loopCount = 0)
    {
        Vector3 desirePos = SpawnPointSelector();

        var terrain = NewMapTerrainData.GetTerrain;
        if (!IsOutHeight(terrain, desirePos)) return desirePos;
        else desirePos.x += differPos;
        if (IsOutHeight(terrain, desirePos)) desirePos.x -= 2 * differPos;
        else return desirePos;
        if (IsOutHeight(terrain, desirePos)) desirePos.z += differPos;
        else return desirePos;
        if (IsOutHeight(terrain, desirePos)) desirePos.z -= 2 * differPos;
        else return desirePos;

        if (loopCount >= findPointLoopLimiter) return null;
        else return SpawnPointFinder(++loopCount);
    }

    bool IsOutHeight(Terrain terrain,Vector3 desirePos)
    {
        return terrain.terrainData.GetInterpolatedHeight(desirePos.x / terrain.terrainData.size.x, desirePos.z / terrain.terrainData.size.z) >= 15;
    }
}
