using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenuSelectChange : ArrowImageMove
{
    [SerializeField] Image image;

    public delegate void IsPointerClick();
    public IsPointerClick isPointerClick;

    bool isPlayMove;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        StartUp();
    }

    // Update is called once per frame
    void Update()
    {
        if (TitleMenuSelectIcon.IsSceneLoadProcess)
        {
            image.enabled = false;
            return;
        }
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
