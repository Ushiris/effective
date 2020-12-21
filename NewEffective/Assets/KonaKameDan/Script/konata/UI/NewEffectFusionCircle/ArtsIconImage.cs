using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtsIconImage : MonoBehaviour
{
    [System.Serializable]
    public class Icon
    {
        public Image image;
    }

    public PrefabDictionary data;

    [System.Serializable]
    public class PrefabDictionary : Serialize.TableBase<string, Icon, Name2Prefab> { }

    [System.Serializable]
    public class Name2Prefab : Serialize.KeyAndValue<string, Icon>
    {
        public Name2Prefab(string key, Icon value) : base(key, value) { }
    }
}
