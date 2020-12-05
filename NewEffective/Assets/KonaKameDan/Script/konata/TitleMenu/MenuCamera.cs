using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    [SerializeField] Transform pivot;
    [SerializeField] float speed = 3f;

    int listNum = 0;
    int max;
    Transform moveTransform => transform;

    /// <summary>
    /// 現在の配列番号を取得
    /// </summary>
    protected int GetListNum => listNum;

    /// <summary>
    /// 配列の最大値をセットする
    /// </summary>
    /// <param name="num"></param>
    protected void SetMaxRange(int num)
    {
        max = num - 1;
    }

    protected void SetListNum(int num)
    {
        listNum = num;
    }

    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="actorPoint"></param>
    /// <returns></returns>
    protected Vector3 Move(Vector3 actorPoint)
    {
        transform.LookAt(pivot);
        return Vector3.Lerp(moveTransform.position, actorPoint, Time.deltaTime * speed);
    }

    /// <summary>
    /// 目標地点にたどり着いたか
    /// </summary>
    /// <param name="my"></param>
    /// <param name="actorPoint"></param>
    /// <returns></returns>
    protected bool EndMove(Vector3 actorPoint)
    {
        var dis = Vector3.Distance(moveTransform.position, actorPoint);
        return dis < 0.2f;
    }

    /// <summary>
    /// 次に進むために配列番号を進める
    /// </summary>
    protected void Next()
    {
        if (listNum < max) listNum++;
        else listNum = 0;
    }

    /// <summary>
    /// 前に戻るために配列番号を戻す
    /// </summary>
    /// <returns></returns>
    protected void Back()
    {
        if (listNum > 0) listNum--;
        else listNum = max;
    }

    /// <summary>
    /// キーボード操作によって移動させる
    /// </summary>
    protected void MoveControlKey()
    {
        if (IsListNumPlusMove()) Next();
        else if (IsListNumMinusMove()) Back();
    }

    protected bool IsListNumPlusMove()
    {
        return Input.GetKeyDown(KeyCode.LeftArrow);
    }

    protected bool IsListNumMinusMove()
    {
        return Input.GetKeyDown(KeyCode.RightArrow);
    }
}
