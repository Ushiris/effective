using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//add

/// <summary>
/// 世界の経過時間
/// </summary>
public class WorldTime : MonoBehaviour
{
    /// <summary>
    /// 計測時間を返す
    /// </summary>
    public static float GetWorldTime { get; private set; }

    static bool isTimeStop;

    private void Start()
    {
        if (MainGameManager.GetArtsReset) WorldTimeReset();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isTimeStop) GetWorldTime += Time.deltaTime;
    }

    /// <summary>
    /// 計測時間をリセットする
    /// </summary>
    public static void WorldTimeReset()
    {
        GetWorldTime = 0;
    }

    /// <summary>
    /// 計測時間を止めるまたは動かす
    /// </summary>
    /// <param name="on">true=止める、false=動かす</param>
    /// <returns>止めているかどうかを返す</returns>
    public static bool OnWorldTimeStop(bool on = true)
    {
        isTimeStop = on;
        return isTimeStop;
    }


}
