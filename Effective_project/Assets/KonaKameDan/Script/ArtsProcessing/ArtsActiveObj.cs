using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存在を登録する
/// </summary>
public class ArtsActiveObj : MonoBehaviour
{
    public static List<GameObject> Id025_PrimitiveShield = new List<GameObject>();
    public static List<GameObject> Id25_UnbreakableShield = new List<GameObject>();
    public static List<GameObject> Id59_Funnel = new List<GameObject>();
    public static List<GameObject> Id259_FlyCurse = new List<GameObject>();
    public static List<GameObject> Id24_EMP = new List<GameObject>();
    public static List<GameObject> Id249_Icarus = new List<GameObject>();
    public static List<GameObject> Id245_EMPCube = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Id025_PrimitiveShield.Clear();
        Id25_UnbreakableShield.Clear();
        Id59_Funnel.Clear();
        Id259_FlyCurse.Clear();
        Id24_EMP.Clear();
        Id249_Icarus.Clear();
        Id245_EMPCube.Clear();
    }
}
