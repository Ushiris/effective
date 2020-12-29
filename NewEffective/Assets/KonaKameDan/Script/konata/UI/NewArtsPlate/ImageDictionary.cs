using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageDictionary : MonoBehaviour
{
    [System.Serializable]
    public class MyImade
    {
        public Sprite image;
    }

    public PrefabDictionary data;
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    /// <summary>
    /// イメージを変更する
    /// </summary>
    /// <param name="key"></param>
    public void ChangeImage(string key)
    {
        if (data.GetTable().ContainsKey(key))
        {
            image.enabled = true;
            image.sprite = data.GetTable()[key].image;
        }
        else
        {
            image.enabled = false;
        }
    }

    [System.Serializable]
    public class PrefabDictionary : Serialize.TableBase<string, MyImade, Name2Prefab> { }

    [System.Serializable]
    public class Name2Prefab : Serialize.KeyAndValue<string, MyImade>
    {
        public Name2Prefab(string key, MyImade value) : base(key, value) { }
    }

}
