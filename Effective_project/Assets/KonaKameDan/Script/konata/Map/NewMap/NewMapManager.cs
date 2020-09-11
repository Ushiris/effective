﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewMapManager : MonoBehaviour
{
    public enum MapType
    {
        Forest
    }

    [System.Serializable]
    public class Status
    {
        public GameObject map;
        public MapType mapType;
        [HideInInspector] public Vector3 playerSpawnPoint;
        [HideInInspector] public Vector3 bossSpawnPoint;
        public int effectItemMaxCount;
        public List<GatyaGatyaInventory.ItemClass> effectItem = new List<GatyaGatyaInventory.ItemClass>();
    }

    [SerializeField] List<Status> statusList = new List<Status>();
    [SerializeField] GameObject playerObj;
    [SerializeField] GameObject bossObj;
    [SerializeField] GameObject portalObj;

    List<Vector3> eventPos = new List<Vector3>();

    // Start is called before the first frame update
    void Awake()
    {
        //変数セット
        int statusListNum = Random.Range(0, statusList.Count);
        Status status = statusList[statusListNum];
        GameObject map = Instantiate(status.map, transform);

        //準備
        RandomSpawn(status);
        PointDelete(map);
        SetNavMesh(map);

        //プレイヤーとボス配置
        Instantiate(playerObj, status.playerSpawnPoint, new Quaternion());
        Instantiate(bossObj, status.bossSpawnPoint, new Quaternion());

        //ポータル設置
        var randPortalPos = new Vector3(Random.Range(-5, 5), -4f, Random.Range(-5, 5));
        Instantiate(portalObj, status.bossSpawnPoint + randPortalPos, new Quaternion());

        //イベント配置
        MapEventPosArr(500, 300, 10, 0, map);

        //エフェクトオブジェクト設置
        var eObj = status.effectItem;
        var nums = GatyaGatyaInventory.RandomTableInstant(eObj);
        for(int i = 0; i < status.effectItemMaxCount; i++)
        {
            var obj = GatyaGatyaInventory.RandomObj(nums, eObj);
            SetEvent(obj);
        }


    }

    //イベント設置位置作成
    const int fixPos=200;
    void MapEventPosArr(int xRange, int yRange, float maxH, float minH, GameObject mapObj)
    {
        var terrain = mapObj.GetComponent<Terrain>().terrainData;

        for (int x = fixPos; x < xRange; x += 10)
        {
            for (int y = 0; y < yRange; y += 10)
            {
                var h = terrain.GetHeight(x, y);
                if (h >= minH && h < maxH)
                {
                    eventPos.Add(new Vector3(x, h + 0.5f, y));
                }
            }
        }
    }

    //イベント設置
    GameObject SetEvent(GameObject obj)
    {
        var rand = Random.Range(0, eventPos.Count - 1);
        var e = eventPos[rand];
        eventPos.RemoveAt(rand);
        return Instantiate(obj, e, Quaternion.identity);   
    }

    //プレイヤーとボスの生成位置を出す
    void RandomSpawn(Status status)
    {
        List<Vector3> pPos = new List<Vector3>();
        List<Vector3> bPos = new List<Vector3>();
        var mapObj = status.map.transform;

        //テーブル作成
        foreach (Transform childTransform in mapObj)
        {
            switch (childTransform.tag)
            {
                case "BossPoint": bPos.Add(childTransform.position); break;
                case "PlayerPoint": pPos.Add(childTransform.position); break;
                default: break;
            }
        }

        var pNum = Random.Range(0, pPos.Count);
        var bNum = Random.Range(0, bPos.Count);

        //ポジションを格納
        status.playerSpawnPoint = pPos[pNum];
        status.bossSpawnPoint = bPos[bNum];
    }

    //余計なものを消す
    void PointDelete(GameObject mapObj)
    {
        foreach (Transform childTransform in mapObj.transform)
        {
            if (childTransform.tag == "BossPoint" || childTransform.tag == "PlayerPoint")
            {
                childTransform.gameObject.SetActive(false);
                Destroy(childTransform.gameObject);
            }
        }
    }

    void SetNavMesh(GameObject obj)
    {
        obj.AddComponent<NavMeshSurface>();
        obj.AddComponent<InstantNavMesh>();
    }
}