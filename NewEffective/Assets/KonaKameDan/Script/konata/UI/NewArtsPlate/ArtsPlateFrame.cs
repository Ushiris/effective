using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtsPlateFrame : MonoBehaviour
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
        if (key == "")
        {
            image.enabled = false;
            return;
        }

        var num = int.Parse(key);
        var type = NameDefinition.GetEffectColor((NameDefinition.EffectName)num);
        if (type != NameDefinition.EffectColor.Nothing)
        {
            image.enabled = true;
            image.sprite = data.GetTable()[type].image;
        }
        else
        {
            image.enabled = false;
        }
    }

    [System.Serializable]
    public class PrefabDictionary : Serialize.TableBase<NameDefinition.EffectColor, MyImade, Name2Prefab> { }

    [System.Serializable]
    public class Name2Prefab : Serialize.KeyAndValue<NameDefinition.EffectColor, MyImade>
    {
        public Name2Prefab(NameDefinition.EffectColor key, MyImade value) : base(key, value) { }
    }
}
