using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2020/06/22 deleted 
/* 
public class ArtsAttack : MonoBehaviour
{
    [SerializeField] private Arts arts;
    [SerializeField] private ArtsGenerator artsGenerator;
    private GameObject artsAttack;
    void Start()
    {
        GameObject gameManager = GameObject.Find("GameManager");
        artsGenerator = gameManager.GetComponent<ArtsGenerator>();
        arts = gameManager.GetComponent<Arts>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            artsAttack = artsGenerator.GenerateArts(ArtsList.GetSelectArts.id);// 選択しているアーツのgameObjectを参照
            artsAttack.GetComponent<Arts>().Fire();
            //DebugLogger.Log("ここをクリックしてscript内のコメントを外さないとartsは出ません");
        }
    }
}
*/