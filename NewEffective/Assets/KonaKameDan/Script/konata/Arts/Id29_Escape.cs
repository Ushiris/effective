using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id29_Escape : MonoBehaviour
{
    [SerializeField] float force = 10f;

    // Start is called before the first frame update
    void Start()
    {
        //Vector3 vector = new Vector3(Input.GetAxis("Vertical"), 0f, Input.GetAxis("Horizontal"));
        var obj = PlayerManager.GetManager.GetPlObj;
        Arts_Process.RbMomentMove(obj, -force);

        //SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Id29_Escape_first);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject);
    }
}
