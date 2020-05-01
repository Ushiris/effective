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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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
    }
}
