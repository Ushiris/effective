using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Image2DAnimation : MonoBehaviour
{
    [SerializeField] RectTransform rect;

    bool isStartAction;
    bool isEndAction;
    Vector3 defaultPos;

    delegate void Action();
    Action StartAction;
    Action EndAction;
    //Action WaitingAction;

    //////////////////////////
    /* これより下は取得関係 */
    //////////////////////////

    /// <summary>
    /// 初めの演出が実行中ならばtrue
    /// </summary>
    protected bool GetStartAction => isStartAction;

    /// <summary>
    /// 終わりの演出が実行中ならばtrue
    /// </summary>
    protected bool GetEndAction => isEndAction;

    ////////////////////////////
    /* これより下は手動操作用 */
    ////////////////////////////

    /// <summary>
    /// 初めの演出のフラグを手動で操作する用
    /// </summary>
    /// <param name="isAction"></param>
    protected void SetStartAction(bool isAction)
    {
        isStartAction = isAction;
    }

    ////////////////////////////////////
    /* これより下は呼ぶ必要のあるもの */
    ////////////////////////////////////

    /// <summary>
    /// 初めに呼ぶこと
    /// </summary>
    protected void StartUp()
    {
        defaultPos = rect.position;
        ActionReset();
    }

    /// <summary>
    /// デリゲートの初期化
    /// </summary>
    protected void ActionReset()
    {
        StartAction = () => { };
        EndAction = () => { };
    }

    /// <summary>
    /// 初めの演出を実行するのであればこれをupdateで呼ぶこと
    /// </summary>
    protected void StartActionUpdate()
    {
        StartAction();
    }

    /// <summary>
    /// 終わりの演出を実行するのであればこれをupdateで呼ぶこと
    /// </summary>
    protected void EndActionUpdate()
    {
        EndAction();
    }

    //////////////////////
    /* これより下は処理 */
    //////////////////////

    /// <summary>
    /// 下らかUIが出現する処理
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="speed"></param>
    protected void AppearFromBelowUI_Acceleration(float speed, float fixH)
    {
        float defaultPosH = -((Screen.currentResolution.height / 2) + fixH);
        float afterPosH = defaultPos.y;
        float time = 0;
        isStartAction = true;

        rect.localPosition = new Vector3(rect.localPosition.x, defaultPosH, rect.localPosition.z);

        StartAction += () =>
        {
            if (!isStartAction)
            {
                return;
            }

            time += Time.unscaledDeltaTime;

            var pos = rect.localPosition;
            pos.y += (time * time) * speed;
            pos.y = Mathf.Clamp(pos.y, defaultPosH, afterPosH);
            rect.localPosition = pos;

            if (pos.y == afterPosH) isStartAction = false;
        };
    }

    /// <summary>
    /// ふあふあ上下に動かす
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="speed"></param>
    /// <param name="h"></param>
    protected void UpDownSinAction(float speed, float h)
    {
        var pos = rect.localPosition;
        pos.y = h * Mathf.Sin(Time.unscaledTime * speed);
        rect.localPosition = pos;
    }

    /// <summary>
    /// UIが上に行く
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="fixH"></param>
    protected void OffScreen_Up(float speed, float fixH)
    {
        float defaultPosH = defaultPos.y;
        float afterPosH = Screen.currentResolution.height + fixH;
        float time = 0;
        isEndAction = true;

        EndAction += () =>
        {
            if (!isEndAction) return;

            time += Time.unscaledDeltaTime;

            var pos = rect.localPosition;
            pos.y += time * speed;
            pos.y = Mathf.Clamp(pos.y, defaultPosH, afterPosH);
            rect.localPosition = pos;

            if (pos.y == afterPosH) isEndAction = false;
        };
    }
}
