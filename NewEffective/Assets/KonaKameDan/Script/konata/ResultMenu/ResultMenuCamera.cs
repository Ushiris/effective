using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultMenuCamera : MenuCamera
{
    [SerializeField] TitleMenuSelectChange nextButton;
    [SerializeField] TitleMenuSelectChange backButton;
    //[SerializeField] ResultMenuUIManager uIManager;
    [SerializeField] ResultScorePresenter newUI_Manager;

    public bool IsMoveEnd { get; private set; }

    // Start is called before the first frame update
    void Start()
    {

        //SetMaxRange(uIManager.scoreUi.Length);
        SetMaxRange(newUI_Manager.GetScorePanelMaxCount);

        //クリックに反応するようにする
        nextButton.isPointerClick = () => NextUi();
        backButton.isPointerClick = () => BackUi();

        //uIManager.isNext = () => Next();
        newUI_Manager.OnNext = () => Next();
    }

    // Update is called once per frame
    void Update()
    {
        if (TitleMenuSelectIcon.IsSceneLoadProcess && IsMoveEnd) return;

        //次に進むために配列番号を進める
        //MoveControlKey();

        //移動
        //var obj = uIManager.scoreUi[GetListNum];
        var obj = newUI_Manager.GetScorePanelTransform(GetListNum);
        var target = obj.transform.position;
        transform.position = Move(target);

        //移動が終了したかどうか
        IsMoveEnd = EndMove(target);
    }

    void NextUi()
    {
        if (TitleMenuSelectIcon.IsSceneLoadProcess) return;
        Next();
        //uIManager.ForcedScoreRollPlay(GetListNum);
        newUI_Manager.ForcedScoreRollPlay(GetListNum);
    }

    void BackUi()
    {
        if (TitleMenuSelectIcon.IsSceneLoadProcess) return;
        Back();
        //uIManager.ForcedScoreRollPlay(GetListNum);
        newUI_Manager.ForcedScoreRollPlay(GetListNum);
    }
}
