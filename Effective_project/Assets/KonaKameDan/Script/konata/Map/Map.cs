using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マップを管理
/// </summary>
public class Map : MonoBehaviour
{
    //オブジェクトの種類
    public enum ObjType
    {
        Nothing, Wall, Goal, Start, Boss, EffectItem, ProhibitedArea
    }
    //マップを二次元配列で作成しているため、わかりやすくするための物
    public enum MapDataArrLength { Width, Depth }

    [Header("生成する範囲")]
    [SerializeField] int w = 200;
    [SerializeField] int d = 200;

    [SerializeField] float h = 1.5f;
    [SerializeField] float siz = 5f;

    [Header("オブジェクトが埋まった時に道を作る")]
    [SerializeField] int roadSpace = 5;
    [SerializeField] int roadLength = 30;

    [Header("ノイズの細かさ")]
    [SerializeField] float chaos = 30f;

    [Header("ボスが生まれる範囲")]
    [SerializeField] int bossSpawnAreaRange = 5;

    [Header("オブジェクト生成ポジション")]
    [SerializeField] GameObject frameObjPoint;
    [SerializeField] GameObject wallObjPoint;
    [SerializeField] GameObject GoalObj;
    [SerializeField] GameObject plObj;

    [Header("ばらまくエフェクト")]
    [SerializeField] int effectInstantCount = 10;
    [SerializeField] List<GameObject> effectItem = new List<GameObject>();

    [System.Serializable]
    public class EventObj   //インスペクター上で操作ができるようにするための物
    {
        [HideInInspector] public string name;
        [HideInInspector] public ObjType objType;
        [HideInInspector] public Transform trans;
        public GameObject obj;
    }
    [Header("イベントオブジェクト生成ポジション"),SerializeField]
    List<EventObj> inspectorExclusive = new List<EventObj>() {
        new EventObj{name=ObjType.Start.ToString(),objType=ObjType.Start },
        new EventObj{name=ObjType.Goal.ToString(),objType=ObjType.Goal },
        new EventObj{name=ObjType.Boss.ToString(),objType=ObjType.Boss }
    };

    [Header("シード値")]
    [SerializeField] int seed;


    ObjType[,] mapData;
    Dictionary<ObjType, GameObject> eventObj = new Dictionary<ObjType, GameObject>();

    /// <summary>
    /// イベントを設置できる場所
    /// </summary>
    public static List<V2> RandomPutEventTable = new List<V2>();

    // Start is called before the first frame update
    void Awake()
    {
        //シード値固定用
        if (seed != 0) Random.seed = seed;

        //地形データ生成
        mapData = TerrainDataInstant.InstantMapChip(w, d, h, chaos);
        TerrainDataInstant.InstantProhibitedArea(mapData);

        //エフェクトオブジェクトの定義
        var obj = GetComponent<GatyaGatyaInventory>().EffectObj;
        List<int> nums = GatyaGatyaInventory.RandomTableInstant(obj);
        effectItem = GatyaGatyaInventory.RandomObjList(nums, obj, effectInstantCount);

        //イベントを登録
        MapEvent.InstantEvent(mapData, ObjType.Start);
        MapEvent.InstantEvent(mapData, ObjType.Goal);
        for (int i = 0; i < effectInstantCount; i++) MapEvent.InstantEvent(mapData, ObjType.EffectItem, false);

        //登録したイベントのデータを取得
        int xStart = MapEvent.eventPos[ObjType.Start].x;
        int zStart = MapEvent.eventPos[ObjType.Start].z;
        int xGoal = MapEvent.eventPos[ObjType.Goal].x;
        int zGoal = MapEvent.eventPos[ObjType.Goal].z;

        //イベントが壁に囲まれている場合壁を破壊する
        MapRoadInstant.IfWall_MakeRoad(mapData, xStart, zStart, roadLength, roadSpace);
        MapRoadInstant.IfWall_MakeRoad(mapData, xGoal, zGoal, roadLength, roadSpace);

        //ボスの配置
        MapEvent.NearEventInstant(mapData, xGoal, zGoal, ObjType.Boss, bossSpawnAreaRange);

        //マップデータをテキストに出力する
        //MapDebug.TextOutput(mapData, "Assets/MapData.txt");

        //オブジェクトを生成
        ListToDictionary();
        MapMaterialization.InstantFrame(w, d, frameObjPoint, transform);
        MapMaterialization.ObjSet(mapData, wallObjPoint, eventObj, effectItem, transform);

        //マップ拡大率
        transform.localScale = new Vector3(siz, siz, siz);

        //サイズを変更したくないものを生成
        MapMaterialization.InstantObj(GoalObj, eventObj[ObjType.Goal], eventObj[ObjType.Goal]);
        MapMaterialization.InstantObj(plObj, eventObj[ObjType.Start]);
    }

    //インスペクタ上に出したものをディクショナリに格納しなおす
    void ListToDictionary()
    {
        foreach(EventObj obj in inspectorExclusive)
        {
            eventObj.Add(obj.objType, obj.obj);
        }
    }
}

public class V2
{
    public int x;
    public int z;

    public V2(int x = 0, int z = 0)
    {
        this.x = x;
        this.z = z;
    }
}
