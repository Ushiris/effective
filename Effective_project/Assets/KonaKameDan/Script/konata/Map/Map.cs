using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Map : MonoBehaviour
{
    public enum ObjType
    {
        Nothing, Wall, Goal, Start, ProhibitedArea

    }

    [Header("生成する範囲")]
    [SerializeField] int w = 100;
    [SerializeField] int d = 100;

    [SerializeField] float h = 1;
    [SerializeField] float siz = 1f;

    [Header("オブジェクトが埋まった時に道を作る")]
    [SerializeField] int roadSpace = 3;
    [SerializeField] int roadLength = 20;

    [Header("ノイズの細かさ")]
    [SerializeField] float chaos = 8f;

    [Header("生成するオブジェクト"), NamedArrayAttribute(new string[]
    {
        "地面","壁","ゴール","スタート"
    })]
    [SerializeField] List<GameObject> obj = new List<GameObject>();

    [Header("シード値")]
    [SerializeField] int seed;

    ObjType[,] mapData;

    List<string> text = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        if (seed == 0) seed = Random.seed;
        else Random.seed = seed;

        //地面の生成
        ObjInstant(new Vector3(w / 2, -1, d / 2), new Vector3(w, 1, d));

        //外枠作成
        ObjInstant(new Vector3(0.5f, 0, d / 2), new Vector3(1, 1, d));
        ObjInstant(new Vector3(w - 0.5f, 0, d / 2), new Vector3(1, 1, d));
        ObjInstant(new Vector3(w / 2, 0, 0.5f), new Vector3(w, 1, 1));
        ObjInstant(new Vector3(w / 2, 0, d - 0.5f), new Vector3(w, 1, 1));

        mapData = MapDataCreate.InstantMapChip(w, d, h, chaos);
        MapDataCreate.InstantEvent(mapData, ObjType.Goal);
        MapDataCreate.InstantEvent(mapData, ObjType.Start);

        MapDataCreate.InstantRoad(mapData, ObjType.Goal, roadLength, roadSpace);
        MapDataCreate.InstantRoad(mapData, ObjType.Start, roadLength, roadSpace);


        ObjSet((int)ObjType.Goal, (int)ObjType.ProhibitedArea);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ObjSet(int objMin, int objMax)
    {
        //オブジェクト設置
        for (int x = 1; x < w - 1; x++)
        {
            text.Add("");
            for (int z = 1; z < d - 1; z++)
            {
                if (mapData[x, z] == ObjType.Wall)
                {
                    //周りをキューブで囲むように設置
                    if (On(x, z))
                    {
                        InstantRoom(obj[(int)ObjType.Wall], x, z);
                    }
                }
                else
                {
                    for (int i = objMin; i < objMax; i++)
                    {
                        if (mapData[x, z] == (ObjType)i)
                        {
                            InstantRoom(obj[i], x, z);
                        }
                    }
                }

                int num = (int)mapData[x, z];
                text[text.Count - 1] += " " + num.ToString();
            }
        }
        File.WriteAllLines("Assets/a.txt", text);
    }

    bool On(int x, int z)
    {
        bool b = false;
        for (int i = 0; i <= (int)ObjType.ProhibitedArea; i++)
        {
            if (mapData[x - 1, z] == (ObjType)i ||
                mapData[x + 1, z] == (ObjType)i ||
                mapData[x, z - 1] == (ObjType)i ||
                mapData[x, z + 1] == (ObjType)i)
            {
                b = true;
            }
            if (i == 0) i = (int)ObjType.Wall;
        }
        return b;
    }

    void InstantRoom(GameObject instantObj, float x, float z)
    {
        GameObject cube = Instantiate(instantObj, new Vector3(x, 0, z), new Quaternion());
        cube.transform.SetParent(transform);
    }

    //地面作成
    void ObjInstant(Vector3 pos, Vector3 siz)
    {
        GameObject ground = Instantiate(obj[0], transform);
        float fix = obj[(int)ObjType.Wall].transform.localScale.y / 2;
        ground.transform.localPosition = new Vector3(pos.x - fix, pos.y, pos.z - fix);
        ground.transform.localScale = siz;
    }
}
