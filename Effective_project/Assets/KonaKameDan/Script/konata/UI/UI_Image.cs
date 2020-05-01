using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Image : MonoBehaviour
{
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
