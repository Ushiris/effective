using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuSelectChange : ArrowImageMove
{
    public delegate void IsPointerClick();
    public IsPointerClick isPointerClick;

    bool isPlayMove;

    // Start is called before the first frame update
    void Start()
    {
        StartUp();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayMove)
        {
            OnVerticalMove();
        }
    }

    public void OnPointerEnter()
    {
        isPlayMove = true;
    }

    public void OnPointerExit()
    {
        OnDefaultPos();
        isPlayMove = false;
    }

    public void OnPointerClick()
    {
        isPointerClick();
    }
}
