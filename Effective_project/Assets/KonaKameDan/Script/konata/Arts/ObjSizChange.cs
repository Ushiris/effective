using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSizChange : MonoBehaviour
{
    public enum SizChangeMode { ScaleUp, ScaleDown }

    public Vector3 defaultSiz = Vector3.zero;
    public Vector3 maxSiz;
    public Vector3 changeSizPos = Vector3.one;

    public float sizChangeSpeed = 0.1f;
    public SizChangeMode SetSizChangeMode;
    public bool GetSizFlag;

    Vector3 siz;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = defaultSiz;
        siz = transform.localScale;
        maxSiz *= 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetSizFlag)
        {
            switch (SetSizChangeMode)
            {
                case SizChangeMode.ScaleUp:
                    GetSizFlag = isTrigger(maxSiz, changeSizPos);
                    break;

                case SizChangeMode.ScaleDown:
                    GetSizFlag = isTrigger(defaultSiz, changeSizPos * -1);
                    break;

                default: break;
            }
        }
    }

    bool isTrigger(Vector3 sizLimit,Vector3 changeVector)
    {
        siz += changeVector * (sizChangeSpeed * Time.deltaTime);
        transform.localScale = siz;
        float dis = Vector3.Distance(siz * 100, sizLimit);
        if (dis / 100 < 0.1f)
        {
            return true;
        }
        else return false;
    }
}
