using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtsActionElements : MonoBehaviour
{
    public delegate void ArtsAction(GameObject arts);
    public static Dictionary<string, ArtsAction> Actions { get; private set; }
    static bool isInit = false;

    //call once only
    public static void Init()
    {
        if (isInit) return;

        //init dictionary
        Actions.Add("Brank", Brank);

        isInit = true;
    }

    //brank function.
    public static void Brank(GameObject arts) { }

    //todo make functions
}
