using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id149_HolySword : MonoBehaviour
{
    [SerializeField] GameObject holySwordParticleObj;
    [SerializeField] float high = 5f;
    [SerializeField] float radius = 3f;
    [SerializeField] int maxCont = 6;

    // Start is called before the first frame update
    void Start()
    {

        //位置、回転の初期化
        transform.parent = null;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        var myPos = transform.position;
        myPos.y += high;
        transform.position = myPos;

        //円状の座標取得
        var pos = Arts_Process.GetCirclePutPos(maxCont, radius, 360);

        GameObject[] holySwordArr = new GameObject[maxCont];
        for(int i = 0; i < maxCont; i++)
        {
            //生成
            holySwordArr[i] = Instantiate(holySwordParticleObj, transform);
            holySwordArr[i].transform.localPosition = pos[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        //オブジェクトの破棄
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
