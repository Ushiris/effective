using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    [SerializeField] PrefabDictionary prefabs;

    public GameObject GetGameObject(NameDefinition.EffectName name)
    {
        return prefabs.GetTable()[name];
    } 

    [System.Serializable]
    public class PrefabDictionary : Serialize.TableBase<NameDefinition.EffectName, GameObject, Name2Prefab> { }

    [System.Serializable]
    public class Name2Prefab : Serialize.KeyAndValue<NameDefinition.EffectName, GameObject>
    {
        public Name2Prefab(NameDefinition.EffectName key, GameObject value) : base(key, value) { }
    }
}
