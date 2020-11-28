using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuMouse : MonoBehaviour
{
    [SerializeField] TitleMenuCamera titleMenuCamera;

    bool isSizChange;
    TitleMenuSelectIcon s;

    static readonly float kRayRange = 10f;

    // Update is called once per frame
    void Update()
    {
        if (TitleMenuSelectIcon.IsSceneLoadProcess || !titleMenuCamera.IsMoveEnd) return;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, kRayRange))
        {
            if (s == null)
            {
                s = hit.collider.gameObject.GetComponent<TitleMenuSelectIcon>();
            }
            else
            {
                //サイズを変える
                if (!isSizChange) isSizChange = s.OnSizChange();

                //シーンチェンジ処理
                if (Input.GetMouseButtonDown(0)) s.OnSceneChange();
            }
        }
        else
        {
            if (s != null)
            {
                //サイズをもとに戻す
                if (isSizChange)
                {
                    isSizChange = s.OnDefaultSiz();
                    s = null;
                }
            }
        }
    }
}
