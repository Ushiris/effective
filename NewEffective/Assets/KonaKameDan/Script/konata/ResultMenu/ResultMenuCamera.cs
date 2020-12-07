using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultMenuCamera : MenuCamera
{
    [SerializeField] TitleMenuSelectChange nextButton;
    [SerializeField] TitleMenuSelectChange backButton;
    [SerializeField] ResultMenuUIManager uIManager;

    public bool IsMoveEnd { get; private set; }

    // Start is called before the first frame update
    void Start()
    {

        SetMaxRange(uIManager.scoreUi.Length);

        //クリックに反応するようにする
        nextButton.isPointerClick = () => NextUi();
        backButton.isPointerClick = () => BackUi();

        uIManager.isNext = () => Next();
    }

    // Update is called once per frame
    void Update()
    {
        if (TitleMenuSelectIcon.IsSceneLoadProcess) return;

        //次に進むために配列番号を進める
        //MoveControlKey();

        //移動
        var obj = uIManager.scoreUi[GetListNum];
        var target = obj.transform.position;
        transform.position = Move(target);

        //移動が終了したかどうか
        IsMoveEnd = EndMove(target);
    }

    void NextUi()
    {
        Next();
        uIManager.ForcedScoreRollPlay(GetListNum);
    }

    void BackUi()
    {
        Back();
        uIManager.ForcedScoreRollPlay(GetListNum);
    }
}
