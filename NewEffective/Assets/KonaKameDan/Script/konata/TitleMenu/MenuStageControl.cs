using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStageControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MenuStage.GetTerrainData(NewMap.SetSelectMapType);
    }
}
