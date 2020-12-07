using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerActive : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined; //カーソルをウィンドウ内に
        Cursor.visible = true;
    }

    private void OnDestroy()
    {
        Cursor.visible = false;
    }
}
