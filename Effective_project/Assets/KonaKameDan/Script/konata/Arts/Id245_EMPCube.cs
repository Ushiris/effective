using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id245_EMPCube : MonoBehaviour
{
    [SerializeField] GameObject id24_EMP_Obj;
    [SerializeField] GameObject empCubeObj;
    [SerializeField] float timeSpace = 30f;
    [SerializeField] int maxCount = 3;
    [SerializeField] float cubeRotationSpeed;
    [SerializeField] Vector3 instantPos = new Vector3(-1, 1, 0);

    int count;
    Vector3 rot;
    GameObject empCube;

    StopWatch timer;
    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //オブジェクトが登録されている場合このオブジェクトを消す
        var objs = ArtsActiveObj.Id245_EMPCube;
        if (Arts_Process.GetMyActiveArts(objs, artsStatus.myObj))
        {
            Destroy(gameObject);
        }

        //生成位置の調整
        transform.localPosition = instantPos;

        empCube = Instantiate(empCubeObj, transform);

        //ランダムに回転の速さを変える
        rot = new Vector3(Random.Range(0, 2f), Random.Range(0, 2f), Random.Range(0, 2f));

        //初めの一回
        Instantiate(id24_EMP_Obj, transform);

        //定期的に呼び出す
        timer = Arts_Process.TimeAction(gameObject, timeSpace);
        timer.LapEvent = () => { InstantEMP(); };
    }

    void Update()
    {
        //empCubeの回転
        var move = rot * cubeRotationSpeed * Time.deltaTime;
        empCube.transform.Rotate(move, Space.World);
    }

    //定期的にEMPを撃つ
    void InstantEMP()
    {
        Instantiate(id24_EMP_Obj, transform);

        if (count == maxCount)
        {
            ArtsActiveObj.Id245_EMPCube.Remove(artsStatus.myObj);
            Destroy(gameObject);
        }

        count++;
    }
}
