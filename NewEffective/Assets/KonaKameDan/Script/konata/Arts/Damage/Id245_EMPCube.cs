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

    [Header("防御のスタック数に応じてたされる数")]
    [SerializeField] float frequency = 0.01f;

    [Header("追尾のスタック数に応じてたされる数")]
    [SerializeField] float maxCountPlus = 0.5f;

    int count;
    Vector3 rot;
    GameObject empCube;

    StopWatch timer;
    ArtsStatus artsStatus;

    int barrierCount;
    int homingCount;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //オブジェクトが登録されている場合このオブジェクトを消す
        var objs = ArtsActiveObj.Id245_EMPCube;
        Arts_Process.OldArtsDestroy(objs, artsStatus.myObj);

        //エフェクトの所持数を代入
        barrierCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Barrier);
        homingCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Homing);

        //頻度変更
        timeSpace -= frequency * (float)barrierCount;

        //回数上限変更
        maxCount += (int)Mathf.Floor(maxCountPlus * (float)homingCount);


        //生成位置の調整
        transform.localPosition = instantPos;

        empCube = Instantiate(empCubeObj, transform);

        //ランダムに回転の速さを変える
        rot = new Vector3(Random.Range(0, 2f), Random.Range(0, 2f), Random.Range(0, 2f));

        //初めの一回
        InstantEMP();

        //定期的に呼び出す
        timer = Arts_Process.TimeAction(gameObject, timeSpace);
        timer.LapEvent = () => { InstantEMP(); };

        //SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Id245_EMPCube_first);
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
        var emp = Instantiate(id24_EMP_Obj, transform);
        var empAs = emp.GetComponent<ArtsStatus>();
        empAs.newArtsStatus(artsStatus);
        DebugLogger.Log("タイプ "+count);

        if (count > maxCount)
        {
            ArtsActiveObj.Id245_EMPCube.Remove(artsStatus.myObj);
            Destroy(gameObject);

            //SE
            SE_Manager.SePlay(SE_Manager.SE_NAME.Id245_EMPCube_third);
        }
        else Destroy(gameObject);

        //SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Id245_EMPCube_second);

        count++;
    }
}
