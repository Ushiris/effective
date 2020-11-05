using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// この数分だけマウススクロールした際に数を変える
/// </summary>
public class ArtsDeckChangeControll : MonoBehaviour
{
    public static int GetNum { get; private set; }

    float scroll;

    private void FixedUpdate()
    {
        scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0)
        {
            if (GetNum > 0)
            {
                GetNum--;
            }
        }
        if (scroll < 0)
        {
            if (GetNum < transform.childCount - 1)
            {
                GetNum++;
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
