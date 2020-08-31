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

    [SerializeField] Status status = new Status();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //プレイヤーとボスの生成位置を出す
    void RandomSpawn()
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

    void PointDelete(GameObject mapObj)
    {
        foreach (Transform childTransform in mapObj.transform)
        {
            Destroy(childTransform.gameObject);
        }
    }
}
