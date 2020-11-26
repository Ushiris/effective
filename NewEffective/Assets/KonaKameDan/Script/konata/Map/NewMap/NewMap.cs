using Digger.Navigation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class NewMap : MonoBehaviour
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
        public int mapSizX, mapSizY, mapSizH;
        [HideInInspector] public Vector3 playerSpawnPoint;
        [HideInInspector] public Vector3 bossSpawnPoint;
        public List<GatyaGatyaInventory.ItemClass> effectItem = new List<GatyaGatyaInventory.ItemClass>();
    }

    [SerializeField] List<Status> statusList = new List<Status>();
    [SerializeField] int treasureBoxCount = 10;
    [SerializeField] GameObject treasureBoxObj;
    [SerializeField] GameObject playerObj;
    [SerializeField] GameObject bossObj;
    [SerializeField] GameObject portalObj;

    public delegate void StartUp();
    StartUp start;

    /// <summary>
    /// Map上に置かれているEvent間の幅
    /// </summary>
    public static readonly int kMapEventRange = 10;

    /// <summary>
    /// ナビメッシュを張った後の処理を実行したい場合これを使う
    /// (＋で追加して！上書きされるので)
    /// </summary>
    public static StartUp SetMapEventStartUp;

    /// <summary>
    /// Mapの限界高度取得
    /// </summary>
    public static float GetMapMaxHeight { get; private set; }

    /// <summary>
    /// 座標をキーにナビメッシュが存在する高さを取得する
    /// </summary>
    public static Dictionary<Vector3, float> GetNavMeshHeight { get; private set; } = new Dictionary<Vector3, float>();

    /// <summary>
    /// プレイヤーの初期リス取得
    /// </summary>
    public static Vector3 GetPlayerRespawnPos { get; private set; }

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

    private void Awake()
    {
        var navMeshSurface = GetComponent<NavMeshSurface>();
        var mapType = SetSelectMapType;
        var statusListNum = GetRandomSelectFromType(mapType);
        var status = statusList[statusListNum];
        var map = status.map;
        GetMapMaxHeight = status.mapSizH;

        map.SetActive(true);

        //プレイヤーのスポーンポイント
        GetPlayerRespawnPos = status.playerSpawnPoint;

        //Diggerに変えた場合消す
        NewMapTerrainData.SetTerrainData(map);

        //プレイヤーとボスの位置を決める
        var plAndBoosPos = RandomSpawn(map);
        status.playerSpawnPoint = plAndBoosPos.Item1;
        status.bossSpawnPoint = plAndBoosPos.Item2;
        PointDelete(map);

        //Mapに設定されているエフェクトをセットする
        foreach (var item in statusList[statusListNum].effectItem)
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

        //プレイヤー配置
        Instantiate(playerObj, status.playerSpawnPoint, new Quaternion());

        start = () =>
        {
            //ナビメッシュのベイク処理
            navMeshSurface.BuildNavMesh();

            //ボス配置
            Instantiate(bossObj, status.bossSpawnPoint, new Quaternion());

            //ポータル設置
            var randPortalPos = new Vector3(Random.Range(-5, 5), -4f, Random.Range(-5, 5));
            Instantiate(portalObj, status.bossSpawnPoint + randPortalPos, new Quaternion());

            //ナビメッシュのある位置にイベントを設置したい
            var navMeshPos = GetNavMeshHeightAndPos(status.mapSizX, status.mapSizY, 0, status.mapSizH);
            GetNavMeshHeight = navMeshPos.Item1;

            //宝箱を設置する
            for (int i = 0; i < treasureBoxCount; i++)
            {
                var obj = SetEvent(treasureBoxObj, navMeshPos.Item2);
                obj.transform.parent = transform;
            }
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        start();
        SetMapEventStartUp();
    }

    //プレイヤーとボスの生成位置を出す
    (Vector3, Vector3) RandomSpawn(GameObject mapObj)
    {
        List<Vector3> pPos = new List<Vector3>();
        List<Vector3> bPos = new List<Vector3>();

        //テーブル作成
        foreach (Transform childTransform in mapObj.transform)
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
        return (pPos[pNum], bPos[bNum]);
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

    //座標をキーとしたナビメッシュがある高さとナビメッシュがある座標を出す
    (Dictionary<Vector3, float>, List<Vector3>) GetNavMeshHeightAndPos(int xRange, int yRange, int minH, int maxH)
    {
        List<Vector3> navMeshPos = new List<Vector3>();
        Dictionary<Vector3, float> navMeshHeight = new Dictionary<Vector3, float>();

        for (int x = 0; x < xRange; x += kMapEventRange)
        {
            for (int y = 0; y < yRange; y += kMapEventRange)
            {

                for (int h = minH; h < maxH; h++)
                {
                    var pos = new Vector3(x, h, y);
                    if (NavMesh.SamplePosition(pos, out NavMeshHit hit, 1, NavMesh.AllAreas))
                    {
                        var hitPos = hit.position;
                        navMeshPos.Add(hitPos);
                        navMeshHeight.Add(pos, hitPos.y);
                    }
                }
            }
        }

        return (new Dictionary<Vector3, float>(navMeshHeight), new List<Vector3>(navMeshPos));
    }

    //イベント設置
    GameObject SetEvent(GameObject obj, List<Vector3> eventPos)
    {
        var rand = Random.Range(0, eventPos.Count - 1);
        var e = eventPos[rand];
        eventPos.RemoveAt(rand);
        return Instantiate(obj, e, Quaternion.identity);
    }
}
