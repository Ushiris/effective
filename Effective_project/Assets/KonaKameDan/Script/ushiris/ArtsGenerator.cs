using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ArtsGenerator : MonoBehaviour
{
    //アーツの実動作を取得します。
    public List<ArtsActionElements.ArtsAction> GetActions(string ID)
    {
        var functions = new List<ArtsActionElements.ArtsAction>();

        ArtsList.GetLookedForArts(ID).actionNames.ForEach(
            (str) =>
            {
                functions.Add(ArtsActionElements.Actions[str]);
            });

        return functions;
    }
}
