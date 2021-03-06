﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

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
        public int mapSizX, mapSizY;
        [HideInInspector] public Vector3 playerSpawnPoint;
        [HideInInspector] public Vector3 bossSpawnPoint;
        public int effectItemMaxCount;
        public List<GatyaGatyaInventory.ItemClass> effectItem = new List<GatyaGatyaInventory.ItemClass>();
    }

    [SerializeField] List<Status> statusList = new List<Status>();
    [SerializeField] GameObject treasureBoxObj;
    [SerializeField] GameObject playerObj;
    [SerializeField] GameObject bossObj;
    [SerializeField] GameObject portalObj;

    List<Vector3> eventPos = new List<Vector3>();

    static readonly bool kIsFieldEffectSet = false;

    //ナビゲーションメッシュ用
    public static UnityEvent OnMapGenerated = new UnityEvent();

    public static Vector3 GetPlayerRespawnPos { get; private set; }

    /// <summary>
    /// イベントを配置できる場所はtrue
    /// </summary>
    public static bool[,] GetEventPos { get; private set; }

    /// <summary>
    /// Mapに設定されているEffectを取得する
    /// </summary>
    public static List<GameObject> GetEffect { get; private set; } = new List<GameObject>();

    /// <summary>
    /// 次に選ばれるマップのTypeを決める
    /// </summary>
    /// <param name="mapType"></param>
    /// <returns></returns>
    public static MapType SetSelectMapType { get; set; } = MapType.Forest;

    // Start is called before the first frame update
    void Awake()
    {
        //変数セット
        MapType mapType = SetSelectMapType;
        int statusListNum = GetRandomSelectFromType(mapType);
        Status status = statusList[statusListNum];
        GameObject map = Instantiate(status.map, transform);
        NewMapTerrainData.SetTerrainData(map);

        //準備
        RandomSpawn(status);
        PointDelete(map);
        OnMapGenerated.Invoke();

        //Mapに設定されているエフェクトをセットする
        foreach(var item in statusList[statusListNum].effectItem)
        {
            if (GetEffect.Count != 0)
            {
                if (!GetEffect.Contains(item.item))
                {
                    GetEffect.Add(item.item);
                }
            }
            else
            {
                GetEffect.Add(item.item);
            }
        }

        //プレイヤーとボス配置
        Instantiate(playerObj, status.playerSpawnPoint, new Quaternion());
        Instantiate(bossObj, status.bossSpawnPoint, new Quaternion());

        //プレイヤーのスポーンポイント
        GetPlayerRespawnPos = status.playerSpawnPoint;

        //ポータル設置
        var randPortalPos = new Vector3(Random.Range(-5, 5), -4f, Random.Range(-5, 5));
        Instantiate(portalObj, status.bossSpawnPoint + randPortalPos, new Quaternion());

        //イベント配置
        MapEventPosArr(status.mapSizX, status.mapSizY, 10, 0, map);

        //エフェクトオブジェクト設置
        if (kIsFieldEffectSet)
        {
            var eObj = status.effectItem;
            var nums = GatyaGatyaInventory.RandomTableInstant(eObj);
            for (int i = 0; i < status.effectItemMaxCount; i++)
            {
                var obj = GatyaGatyaInventory.RandomObj(nums, eObj);
                obj = SetEvent(obj);
                obj.transform.parent = transform;
            }
        }
        else
        {
            for(int i = 0; i < 10;i++)
            {
                var obj = SetEvent(treasureBoxObj);
                obj.transform.parent = transform;
            }
        }
    }

    //イベント設置位置作成
    void MapEventPosArr(int xRange, int yRange, float maxH, float minH, GameObject mapObj)
    {
        var terrain = mapObj.GetComponent<Terrain>().terrainData;
        GetEventPos = new bool[xRange, yRange];

        for (int x = 0; x < xRange; x++)
        {
            for (int y = 0; y < yRange; y++)
            {
                var h = terrain.GetHeight(x, y);
                if (h >= minH && h < maxH)
                {
                    if (x % 10 == 0 && y % 10 == 0)
                    {
                        eventPos.Add(new Vector3(x, h + 0.5f, y));
                    }
                    GetEventPos[x, y] = true;
                }
                else GetEventPos[x, y] = false;
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

    //タイプと一致するものからランダムにマップ情報を取得する
    int GetRandomSelectFromType(MapType mapType)
    {
        List<int> count = new List<int>();

        for (int i = 0; i < statusList.Count; i++)
        {
            if (statusList[i].mapType == mapType)
            {
                count.Add(i);
            }
        }

        var ran = Random.Range(0, count.Count);
        return count[ran];
    }
}
