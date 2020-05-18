using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapDataCreate : MonoBehaviour
{
    public enum Dir { Up, Down, Right, Left }

    static float seedX, seedZ;
    static int width, depth;

    class V2
    {
        public int x;
        public int z;

        public V2(int x = 0, int z = 0)
        {
            this.x = x;
            this.z = z;
        }
    }
    //ランダムにイベントを配置する用のランダムテーブル
    static List<V2> mapTable = new List<V2>();

    //イベントのポジション記憶
    static Dictionary<Map.ObjType, V2> eventPos = new Dictionary<Map.ObjType, V2>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// マップデータの作成
    /// </summary>
    /// <param name="w">幅</param>
    /// <param name="d">奥行き</param>
    /// <param name="h">ノイズの高さから壁作成</param>
    /// <param name="chaos">ノイズの細かさ</param>
    /// <returns></returns>
    public static Map.ObjType[,] InstantMapChip(int w, int d, float h, float chaos)
    {
        Map.ObjType[,] mapData = new Map.ObjType[w, d];

        width = w;
        depth = d;

        //シード値の生成
        seedX = Random.value * 100f;
        seedZ = Random.value * 100f;

        //パーリンノイズからマップ作成
        for (int x = 0; x < w; x++)
        {
            for (int z = 0; z < d; z++)
            {
                //壁でない場所を格納する
                if (x < w - 1 && w != 0 && z < d - 1 && z != 0)
                {
                    mapTable.Add(new V2());
                    mapTable[mapTable.Count - 1].x = x;
                    mapTable[mapTable.Count - 1].z = z;
                }

                //ノイズ発生
                float xSample = (x + seedX) / chaos;
                float zSample = (z + seedZ) / chaos;
                float noise = Mathf.PerlinNoise(xSample, zSample);
                float y = h * noise;

                //四捨五入
                y = Mathf.Round(y);

                if (y <= h * 0.1f)
                {
                    //壁であることを記録
                    mapData[x, z] = Map.ObjType.Wall;

                    //イベントを配置できる場所の記録用
                    if (x < w - 1 && w != 0 && z < d - 1 && z != 0)
                    {
                        mapTable.RemoveAt(mapTable.Count - 1);
                    }
                }
            }
        }

        //禁止エリア作成
        for (int x = 1; x < w - 1; x++)
        {
            for (int z = 1; z < d - 1; z++)
            {
                if (mapData[x, z] == Map.ObjType.Wall)
                {
                    if (mapData[x - 1, z] == Map.ObjType.Nothing ||
                       mapData[x + 1, z] == Map.ObjType.Nothing ||
                       mapData[x, z - 1] == Map.ObjType.Nothing ||
                       mapData[x, z + 1] == Map.ObjType.Nothing)
                    {
                        mapData[x, z] = Map.ObjType.ProhibitedArea;
                    }
                }

            }
        }

        return mapData;
    }

    /// <summary>
    /// イベントのポジションをマップデータ内に記録
    /// </summary>
    /// <param name="map">マップデータ</param>
    /// <param name="objType">イベントの種類</param>
    public static void InstantEvent(Map.ObjType[,] map, Map.ObjType objType)
    {
        int ran = Random.Range(0, mapTable.Count - 1);
        //マップデータにマークする
        map[mapTable[ran].x, mapTable[ran].z] = objType;
        //座標を格納
        eventPos.Add(objType, new V2(mapTable[ran].x, mapTable[ran].z));
        //イベントを配置したのでその場所を消す
        mapTable.RemoveAt(ran);
    }

    /// <summary>
    /// 壁で囲まれた場合道を作成
    /// </summary>
    /// <param name="map">マップデータ</param>
    /// <param name="objType">イベントの種類</param>
    /// <param name="dis">〇〇分4方向に探査を掛ける</param>
    /// <param name="roadSpace">道の広さ</param>
    public static void InstantRoad(Map.ObjType[,] map, Map.ObjType objType, int dis,int roadSpace)
    {
        const int nullNum = 1000;
        int x = eventPos[objType].x;
        int z = eventPos[objType].z;
        Dictionary<Dir, int> wallCheckDis = new Dictionary<Dir, int>()
        {
            { Dir.Up, 0 }, { Dir.Down, 0 },
            { Dir.Right, 0 }, { Dir.Left, 0 }
        };

        //壁までの距離を求める
        if (x - dis > 0)
        {
            wallCheckDis[Dir.Left] = CheckX(-1);
        }
        else wallCheckDis[Dir.Left] = nullNum;

        if (x + dis < width)
        {
            wallCheckDis[Dir.Right] = CheckX(1);
        }
        else wallCheckDis[Dir.Right] = nullNum;

        if (z - dis > 0)
        {
            wallCheckDis[Dir.Down] = CheckZ(-1);
        }
        else wallCheckDis[Dir.Down] = nullNum;

        if (z + dis < depth)
        {
            wallCheckDis[Dir.Up] = CheckZ(1);
        }
        else wallCheckDis[Dir.Up] = nullNum;

        //辞書内にある最も小さい値を取り出す
        int minValue = wallCheckDis.Values.Min();
        var pair = wallCheckDis.FirstOrDefault(c => c.Value == minValue);
        var key = pair.Key;

       //壁に阻まれた方向の数を数える
        int wallCount = 0;
        int nullCaunt = 0;
        foreach(var str in wallCheckDis)
        {
            //Debug.Log("key " + str.Key+" value "+str.Value);
            if (str.Value < dis ) wallCount++;
            if (str.Value == nullNum) nullCaunt++;
        }
        //Debug.Log("wallCount " + wallCount);
        //Debug.Log("nullCaunt " + nullCaunt);

        //3方向囲まれた場合一番近い壁を壊す
        if (wallCount + nullCaunt >= 3)
        {
            WallDestroy(map, eventPos[objType].x, eventPos[objType].z, pair.Key, pair.Value, roadSpace);
        }

        //X方向の探査
        int CheckX(int dir)
        {
            int loopCount = 0;
            int pos = eventPos[objType].x;
            bool check = false;

            for (int i = 0; i < dis; i++)
            {
                pos += dir;
                if (map[pos, eventPos[objType].z] == Map.ObjType.Wall)
                {
                    check = true;
                    break;
                }
                loopCount++;
            }

            //Debug.Log("X" + loopCount);

            if (check) return loopCount;
            else return dis;
        }

        //Z方向の探査
        int CheckZ(int dir)
        {
            int loopCount = 0;
            int pos = eventPos[objType].z;
            bool check = false;

            for (int i = 0; i < dis; i++)
            {
                pos += dir;
                if (map[eventPos[objType].x, pos] == Map.ObjType.Wall)
                {
                    check = true;
                    break;
                }
                loopCount++;
            }

            //Debug.Log("Z" + loopCount);

            if (check) return loopCount;
            else return dis;
        }
    }

    /// <summary>
    /// 1方向の壁を壊す
    /// </summary>
    /// <param name="map">マップデータ</param>
    /// <param name="x">壊す座標</param>
    /// <param name="z">壊す座標</param>
    /// <param name="dir">壊す方向</param>
    /// <param name="length">壊す距離</param>
    /// <param name="roadSpace">壊す幅</param>
    public static void WallDestroy(Map.ObjType[,] map, int x, int z, Dir dir, int length, int roadSpace)
    {
        V2 roadPos = new V2(x, z);
        int loopCount = 0;
        bool isRoadInstant = false;

        for (; ; )
        {
            switch (dir)
            {
                case Dir.Up: roadPos.z += 1; break;
                case Dir.Down: roadPos.z -= 1; break;
                case Dir.Right: roadPos.x += 1; break;
                case Dir.Left: roadPos.x -= 1; break;
                default: break;
            }
            //Debug.Log(roadPos.x + "  " + roadPos.z);

            if (roadPos.z == depth - 1 || roadPos.z == 1) break;
            if (roadPos.x == width - 1 || roadPos.x == 1) break;

            if (map[roadPos.x, roadPos.z] == Map.ObjType.Wall ||
                map[roadPos.x, roadPos.z] == Map.ObjType.ProhibitedArea)
            {
                int num = roadSpace / 2;
                for (int i = -num; i <= num; i++)
                {
                    if (dir == Dir.Up || dir == Dir.Down)
                    {
                        map[x + i, roadPos.z] = Map.ObjType.Nothing;
                    }
                    if (dir == Dir.Left || dir == Dir.Right)
                    {
                        map[roadPos.x, z + i] = Map.ObjType.Nothing;
                    }
                }
                isRoadInstant = true;
            }
            else if (isRoadInstant)
            {
                break;
            }
            Debug.Log(loopCount);
            loopCount++;
        }

    }
}
