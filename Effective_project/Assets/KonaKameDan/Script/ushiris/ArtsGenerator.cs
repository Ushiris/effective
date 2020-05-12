using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ArtsGenerator : MonoBehaviour
{
    public List<string> GetData()
    {
        return new List<string>();
    }

    public List<ArtsActionElements.ArtsAction> GetActions(List<string> data)
    {
        return new List<ArtsActionElements.ArtsAction>();
    }
}
