using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuMouse : MonoBehaviour
{
    bool isSizChange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, 10.0f))
        {
            var s = hit.collider.gameObject.GetComponent<TitleMenuSelectIcon>();
            if (s != null)
            {
                if (!isSizChange)isSizChange = s.OnSizChange();

            }
            else
            {
                //if (isSizChange)
            }
        }
    }
}
