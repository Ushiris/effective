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
    public static List<GameObject> Id15_Fant = new List<GameObject>();
    public static List<GameObject> Id14_RollSlash = new List<GameObject>();
    public static List<GameObject> Id145_Murasame = new List<GameObject>();
    public static List<GameObject> Id012_Invisible = new List<GameObject>();
    public static List<GameObject> Id124_Rush = new List<GameObject>();
    public static List<GameObject> Id579_Salamander = new List<GameObject>();

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
        Id15_Fant.Clear();
        Id14_RollSlash.Clear();
        Id145_Murasame.Clear();
        Id012_Invisible.Clear();
        Id124_Rush.Clear();
        Id579_Salamander.Clear();
    }
}
