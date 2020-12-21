using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    SE_Manager.Se3d se;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SEを流す処理
            se = SE_Manager.Se3dPlay(SE_Manager.SE_NAME.Hit);
        }

        //SEを移動させる処理
        if (se != null)
        {
            SE_Manager.Se3dMove(transform.position, se);
        }
    }
}
