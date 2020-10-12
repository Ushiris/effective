using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpParticle : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabs = new List<GameObject>();

    public static Dictionary<string, GameObject> particles = new Dictionary<string, GameObject>();
    static GameObject group;

    private void Start()
    {
        group = new GameObject("ParticleList");
    }

    static public void ParticleStandby(string id)
    {
        Destroy(group);
        group = new GameObject("ParticleList");
        particles.Clear();

        switch (id)
        {
            case "079":
                


                break;

            default: break;
        }
    }

    [System.Serializable]
    public class PrefabDictionary : Serialize.TableBase<string, GameObject, Name2Prefab> { }

    [System.Serializable]
    public class Name2Prefab : Serialize.KeyAndValue<string, GameObject>
    {
        public Name2Prefab(string key, GameObject value) : base(key, value) { }
    }
}
