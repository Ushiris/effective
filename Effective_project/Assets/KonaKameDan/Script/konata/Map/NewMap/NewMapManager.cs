using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public Vector3 playerSpawnPoint;
        public Vector3 bossSpawnPoint;
        public List<GameObject> effectItem = new List<GameObject>();
    }

    [SerializeField] List<Status> statusList = new List<Status>();
    [SerializeField] GameObject playerObj;
    [SerializeField] GameObject bossObj;

    // Start is called before the first frame update
    void Start()
    {
        //変数セット
        int statusListNum = Random.Range(0, statusList.Count);
        Status status = statusList[statusListNum];
        GameObject map = Instantiate(status.map);

        //準備
        RandomSpawn(status);
        PointDelete(map);

        //プレイヤーとボス配置
        Instantiate(playerObj, status.playerSpawnPoint, new Quaternion());
        Instantiate(bossObj, status.bossSpawnPoint, new Quaternion());
    }

    // Update is called once per frame
    void Update()
    {
        
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
                case "EnemtPoint": pPos.Add(childTransform.position); break;
                case "PlayerPoint": bPos.Add(childTransform.position); break;
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
            if (childTransform.tag == "EnemtPoint" || childTransform.tag == "PlayerPoint")
            {
                Destroy(childTransform.gameObject);
            }
        }
    }
}
