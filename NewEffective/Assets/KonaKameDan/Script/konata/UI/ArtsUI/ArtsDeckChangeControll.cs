using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// この数分だけマウススクロールした際に数を変える
/// </summary>
public class ArtsDeckChangeControll : MonoBehaviour
{
    public static int GetNum { get; private set; }

    delegate void OnPlaySe();
    OnPlaySe onPlaySe;

    float scroll;

    static readonly float kMouseScrollWheelJudgeLine = 0;

    private void Start()
    {
        onPlaySe = () => { UI_Manager.ArtsDeckChangePlaySe(); };
    }

    private void FixedUpdate()
    {
        scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > kMouseScrollWheelJudgeLine)
        {
            if (GetNum > 0)
            {
                GetNum--;
                onPlaySe();
            }
        }
        if (scroll < kMouseScrollWheelJudgeLine)
        {
            if (GetNum < transform.childCount - 1)
            {
                GetNum++;
                onPlaySe();
            }
        }

        SelectKey();
    }

    void SelectKey()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) GetNum = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) GetNum = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) GetNum = 2;
    }
}
