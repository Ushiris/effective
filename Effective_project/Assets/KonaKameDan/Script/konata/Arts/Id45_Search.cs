using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id45_Search : MonoBehaviour
{
    [Header("サーチシェーダー")]
    [SerializeField] Material material;
    [SerializeField] float speed = 30;

    [Header("UI")]
    [SerializeField] GameObject markerUI;
    [SerializeField] float uiDeleteTime = 5f;

    StopWatch timer;
    float dis;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = PlayerManager.GetManager.GetPlObj.transform.position;

        //UIの生成
        GameObject[] effectObj = GameObject.FindGameObjectsWithTag("EffectObject");
        GameObject uiCanvas = GameObject.FindGameObjectWithTag("UiCanvas");
        Arts_Process.SearchMarkUiInstant(effectObj, markerUI, uiCanvas.transform, uiDeleteTime);

        //シェーダーの初期化
        Arts_Process.SearchPosSet(material, pos);
        Arts_Process.SearchShaderReset(material);

        //SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Shot);
    }

    // Update is called once per frame
    void Update()
    {
        dis += speed * Time.deltaTime;
        Arts_Process.SearchShaderStart(material, dis);



        if (dis > 200f)
        {
            Arts_Process.SearchShaderStart(material, 0);
            Destroy(gameObject);
        }
    }
}
