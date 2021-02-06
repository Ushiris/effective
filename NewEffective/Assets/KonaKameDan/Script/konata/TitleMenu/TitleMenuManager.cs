using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuManager : MonoBehaviour
{
    static NewMap.MapType defaultMapType = NewMap.MapType.Nothing;
    // Start is called before the first frame update
    void Start()
    {
        if (defaultMapType == NewMap.MapType.Nothing)
        {
            defaultMapType = NewMap.SetSelectMapType;
        }
        else
        {
            NewMap.SetSelectMapType = defaultMapType;
        }
    }
}
