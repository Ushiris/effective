using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ArtsGenerator : MonoBehaviour
{
    //get arts function name list
    public List<string> GetData(string ID)
    {
         Resources.Load("ArtsListActionName.csv");
        //error return
        return new List<string>();
    }

    //get arts fire actions
    public List<ArtsActionElements.ArtsAction> GetActions(List<string> data)
    {
        var functions = new List<ArtsActionElements.ArtsAction>();

        data.ForEach((str) => { functions.Add(ArtsActionElements.Actions[str]); });

        return functions;
    }
}
