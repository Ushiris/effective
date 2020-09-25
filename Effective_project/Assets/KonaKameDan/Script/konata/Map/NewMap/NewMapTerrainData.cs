using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMapTerrainData : MonoBehaviour
{
    public static Terrain GetTerrain { get; private set; }

    public static void SetTerrainData(GameObject map)
    {
        GetTerrain = map.GetComponent<Terrain>();
    }
}
