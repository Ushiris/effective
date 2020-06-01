using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class KnataLib : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
//使用例
//[NamedArrayAttribute(new string[]
//{
//        "射撃","斬撃","防御","設置","拡散","追尾","吸収","爆発","遅延","飛翔"
//})]
//public List<Sprite> effectIconList = new List<Sprite>();

public class NamedArrayAttribute : PropertyAttribute
{
    public readonly string[] names;
    public NamedArrayAttribute(string[] names) { this.names = names; }
}

[CustomPropertyDrawer(typeof(NamedArrayAttribute))]
public class NamedArrayDrawer : PropertyDrawer
{
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        try
        {
            int pos = int.Parse(property.propertyPath.Split('[', ']')[1]);
            EditorGUI.PropertyField(rect, property, new GUIContent(((NamedArrayAttribute)attribute).names[pos]));

        }
        catch
        {
            EditorGUI.PropertyField(rect, property, label);
        }
    }
}
