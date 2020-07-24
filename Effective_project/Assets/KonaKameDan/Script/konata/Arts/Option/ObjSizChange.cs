using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSizChange : MonoBehaviour
{
    public enum SizChangeMode { ScaleUp, ScaleDown }

    public Vector3 defaultSiz = Vector3.zero;
    public Vector3 maxSiz;

    public float sizChangeSpeed = 0.1f;
    public SizChangeMode SetSizChangeMode;
    public bool GetSizFlag;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = defaultSiz;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetSizFlag)
        {
            switch (SetSizChangeMode)
            {
                case SizChangeMode.ScaleUp:
                    GetSizFlag = isTrigger(maxSiz);
                    break;

                case SizChangeMode.ScaleDown:
                    GetSizFlag = isTrigger(defaultSiz);
                    break;

                default: break;
            }
        }
    }

    bool isTrigger(Vector3 sizLimit)
    {
        transform.localScale =
            Vector3.Lerp(transform.localScale, sizLimit, sizChangeSpeed * Time.deltaTime);

        float dis = Vector3.Distance(transform.localScale, sizLimit);

        Debug.Log("距離: " + dis);
        if (dis < 0.1f)
        {
            return true;
        }
        else return false;
    }
}
