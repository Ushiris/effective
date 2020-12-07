using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuCamera : MenuCamera
{
    [SerializeField] GameObject selectMenuIconGroup;

    [SerializeField] TitleMenuSelectChange nextButton;
    [SerializeField] TitleMenuSelectChange backButton;

    public bool IsMoveEnd { get; private set; }
    List<GameObject> select = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        var s = selectMenuIconGroup.GetComponentsInChildren<TitleMenuSelectIcon>();
        foreach(var obj in s)
        {
            select.Add(obj.gameObject);
        }

        SetMaxRange(select.Count);

        //クリックに反応するようにする
        nextButton.isPointerClick = () => Next();
        backButton.isPointerClick = () => Back();
    }

    // Update is called once per frame
    void Update()
    {
        if (TitleMenuSelectIcon.IsSceneLoadProcess) return;

        //次に進むために配列番号を進める
        MoveControlKey();

        //移動
        var target = select[GetListNum].transform.position;
        transform.position = Move(target);

        //移動が終了したかどうか
        IsMoveEnd = EndMove(target);
    }
}
