using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class UI_Image : MonoBehaviour
{
    [NamedArrayAttribute(new string[]
    {
        "射撃","斬撃","防御","設置","拡散","追尾","吸収","爆発","遅延","飛翔"
    })]
    public List<Sprite> effectIconList = new List<Sprite>();

    public static UI_Image GetUI_Image { get; private set; }

    private void Awake()
    {
        GetUI_Image = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}
