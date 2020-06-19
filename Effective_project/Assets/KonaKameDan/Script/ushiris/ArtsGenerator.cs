using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ArtsGenerator : SingletonMonoBehaviour<ArtsGenerator>
{
    //アーツの実動作を取得します。
    public List<ArtsActionElements.ArtsAction> GetActions(string ID)
    {
        var functions = new List<ArtsActionElements.ArtsAction>();

        ArtsList.GetLookedForArts(ID).actionNames.ForEach(
            (str) =>
            {
                functions.Add(ArtsActionElements.Instance.Actions[str]);
            });

        return functions;
    }

    //artsを作成します。
    public GameObject GenerateArts(string ID)
    {
        var obj = new GameObject();
        var arts = obj.AddComponent<Arts>();

        arts.FireActions = GetActions(ID);

        return obj;
    }
}
