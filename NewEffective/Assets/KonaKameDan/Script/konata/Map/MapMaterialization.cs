using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// マップデータをもとにオブジェクトを設置する
/// </summary>
public class MapMaterialization : MonoBehaviour
{
    public static UnityEvent OnMapGenerated = new UnityEvent();
    //生成
    static GameObject InstantRoom(GameObject instantObj, float x, float z, Transform parent, float y = 0)
    {
        GameObject cube = Instantiate(instantObj, new Vector3(x, y, z), new Quaternion());
        cube.transform.SetParent(parent);
        return cube;
    }

    /// <summary>
    /// マップの具現化
    /// </summary>
    /// <param name="mapData">マップデータ</param>
    /// <param name="wallObj">壁オブジェクト</param>
    /// <param name="eventObj">イベントオブジェクト</param>
    /// <param name="parent">親に</param>
    public static void ObjSet(Map.ObjType[,] mapData, GameObject wallObj, Dictionary<Map.ObjType, GameObject> eventObj, List<GameObject> effectItems, Transform parent)
    {
        int w = mapData.GetLength((int)Map.MapDataArrLength.Width);
        int d = mapData.GetLength((int)Map.MapDataArrLength.Depth);
        int effectItemCount = 0;

        //オブジェクト設置
        for (int x = 1; x < w - 1; x++)
        {
            for (int z = 1; z < d - 1; z++)
            {
                if (mapData[x, z] == Map.ObjType.Wall)
                {
                    //周りをキューブで囲むように設置
                    if (isEventCheck(x, z))
                    {
                        InstantRoom(wallObj, x, z, parent);
                    }
                }
                else
                {
                    //マップデータとイベントが一致している場合作成
                    if (eventObj.ContainsKey(mapData[x, z]))
                    {
                        eventObj[mapData[x, z]] = InstantRoom(eventObj[mapData[x, z]], x, z, parent);

                    }
                    else if (mapData[x, z] == Map.ObjType.EffectItem)
                    {
                        InstantRoom(effectItems[effectItemCount], x, z, parent);
                        effectItemCount++;
                    }
                }

                int num = (int)mapData[x, z];
            }
        }

        bool isEventCheck(int x, int z)
        {
            bool b = false;
            for (int i = 0; i <= (int)Map.ObjType.ProhibitedArea; i++)
            {
                if (mapData[x - 1, z] == (Map.ObjType)i ||
                    mapData[x + 1, z] == (Map.ObjType)i ||
                    mapData[x, z - 1] == (Map.ObjType)i ||
                    mapData[x, z + 1] == (Map.ObjType)i)
                {
                    b = true;
                }
                if (i == 0) i = (int)Map.ObjType.Wall;
            }
            return b;
        }

        OnMapGenerated.Invoke();
    }

    /// <summary>
    /// 地面と囲いの作成
    /// </summary>
    /// <param name="w">マップの幅</param>
    /// <param name="d">マップの奥行</param>
    /// <param name="frameObj">設置するオブジェクト</param>
    /// <param name="parent">親に</param>
    public static void InstantFrame(int w, int d, GameObject frameObj, Transform parent)
    {
        //地面の生成
        ObjInstant(new Vector3(w / 2, -1, d / 2), new Vector3(w, 1, d));

        //外枠作成
        ObjInstant(new Vector3(0.5f, 0, d / 2), new Vector3(1, 1, d));
        ObjInstant(new Vector3(w - 0.5f, 0, d / 2), new Vector3(1, 1, d));
        ObjInstant(new Vector3(w / 2, 0, 0.5f), new Vector3(w, 1, 1));
        ObjInstant(new Vector3(w / 2, 0, d - 0.5f), new Vector3(w, 1, 1));

        //側の作成
        void ObjInstant(Vector3 pos, Vector3 siz)
        {
            GameObject ground = Instantiate(frameObj, parent);
            float fix = frameObj.transform.localScale.y / 2;
            ground.transform.localPosition = new Vector3(pos.x - fix, pos.y, pos.z - fix);
            ground.transform.localScale = siz;
        }
    }

    /// <summary>
    /// 特定のオブジェクトのところに生成
    /// </summary>
    /// <param name="instantObj">生成したいオブジェクト</param>
    /// <param name="pos">参照場所</param>
    public static void InstantObj(GameObject instantObj, GameObject obj, GameObject parent = null)
    {
        if (instantObj != null)
        {
            GameObject instant = Instantiate(instantObj, obj.transform.position, new Quaternion());
            if (parent != null) instant.transform.SetParent(parent.transform.parent);
        }
    }

    /// <summary>
    /// 草の位置データを具現化
    /// </summary>
    /// <param name="grassData">草のデータ</param>
    /// <param name="grassObj">草オブジェクト</param>
    /// <param name="parent">親</param>
    /// <param name="fixY">高さの調整</param>
    /// <param name="space">草同士の幅</param>
    public static void GrassSet(float[,] grassData, GameObject[] grassObj, Transform parent, float fixY, int space)
    {
        int w = grassData.GetLength((int)Map.MapDataArrLength.Width);
        int d = grassData.GetLength((int)Map.MapDataArrLength.Depth);

        //オブジェクト設置
        for (int x = 1; x < w - 1; x += space)
        {
            for (int z = 1; z < d - 1; z += space)
            {
                //オブジェクトの生成
                GameObject obj = grassObj[Random.Range(0, grassObj.Length - 1)];
                float y = grassData[x, z] + fixY;
                GameObject instant = InstantRoom(obj, x, z, parent, y);

                //ランダムに向きを変える
                float rotY = Random.Range(0, 360);
                instant.transform.eulerAngles = new Vector3(0, rotY, 0);
            }
        }
    }
}
