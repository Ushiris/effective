using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStage : MonoBehaviour
{

    [SerializeField] List<NewMap.Status> statusList = new List<NewMap.Status>();

    public static List<NewMap.Status> GetMapStatusList = new List<NewMap.Status>();

    private void Awake()
    {
        
    }
}
