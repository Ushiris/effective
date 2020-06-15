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

    public ParticleSystem GetParticle(string ID)
    {
        return ArtsParticleDictionary.Instance.particle.GetTable()[ID];
    }

    public GameObject GenerateArts(string ID)
    {
        var arts = new GameObject();
        arts.AddComponent<Arts>();


    }
}
