using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// アーツを表示するUIの画像を切り替えるやつ
/// </summary>
public class UI_ArtsMaterialization : MonoBehaviour
{
    enum ICON { Right, Center, Left }
    enum NAME_FRAME { Outside, Center, Inside }

    [SerializeField] float interval = 0.3f;
    public bool displaySwitch;
    bool isReset = true;
    int count;


    [System.Serializable]
    public class Obj
    {
        [HideInInspector] public string name;
        public GameObject obj;
    }

    [SerializeField, Header("名前のところの線")]
    List<Obj> nameFrame = new List<Obj>()
    {
        new Obj{name="outsideNameFrame"},
        new Obj{name="centerNameFrame"},
        new Obj{name="insideNameFrame"},
    };

    [SerializeField, Header("アイコンのところの線")]
    List<Obj> iconFrame = new List<Obj>()
    {
        new Obj{name="rightIconFrame"},
        new Obj{name="centerIconFrame"},
        new Obj{name="leftIconFrame"},
    };

    [SerializeField, Header("アイコン")]
    List<Obj> icon = new List<Obj>()
    {
        new Obj{name="rightIcon"},
        new Obj{name="centerIcon"},
        new Obj{name="leftIcon"},
    };

    [SerializeField] TextMeshProUGUI artsNameText;

    List<GameObject> displayTurn = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //一括管理用
        foreach (var obj in icon) displayTurn.Add(obj.obj);
        foreach (var obj in iconFrame) displayTurn.Add(obj.obj);
        foreach (var obj in nameFrame) displayTurn.Add(obj.obj);

        //初期化
        IntervalDisplay(false, displayTurn.Count);
    }

    // Update is called once per frame
    void Update()
    {
        count = UI_Manager.GetEffectFusionUI_ChoiceNum.numList.Count;

        if (displaySwitch)
        {
            //画面をリセット
            IntervalDisplay(false, displayTurn.Count);
            MoveReset();

            //画像を切り替える
            for (int i = 0; i < count; i++)
            {
                int num = UI_Manager.GetEffectFusionUI_ChoiceNum.numList[i];
                ImageChange(i, num);
            }

            //名称を切り替える
            artsNameText.text = ArtsList.GetSelectArts.name;

            //画像を表示する
            IntervalDisplay(true, count, interval);
        }

    }

    //表示＆表示にかかる時間設定
    void IntervalDisplay(bool on,int count, float intervalTime = 0)
    {
        if (count == 3 || count == displayTurn.Count)
        {
            for (int i = 0; i < displayTurn.Count; i++)
            {
                displayTurn[i].GetComponent<ActiveImageDelay>().Delay(on, intervalTime * i);
            }

            //文字の表示
            artsNameText.GetComponent<ActiveTextMeshProDelay>().Delay(on, intervalTime * displayTurn.Count);
        }
        else if (count == 2)
        {
            icon[(int)ICON.Center].obj.GetComponent<ActiveImageDelay>().Delay(on, intervalTime * 0);
            icon[(int)ICON.Left].obj.GetComponent<ActiveImageDelay>().Delay(on, intervalTime * 1);
            iconFrame[(int)ICON.Center].obj.GetComponent<ActiveImageDelay>().Delay(on, intervalTime * 2);
            iconFrame[(int)ICON.Left].obj.GetComponent<ActiveImageDelay>().Delay(on, intervalTime * 3);
            nameFrame[(int)NAME_FRAME.Inside].obj.GetComponent<ActiveImageDelay>().Delay(on, intervalTime * 4);
            nameFrame[(int)NAME_FRAME.Center].obj.GetComponent<ActiveImageDelay>().Delay(on, intervalTime * 5);

            //文字の表示
            artsNameText.GetComponent<ActiveTextMeshProDelay>().Delay(on, intervalTime * 6);
        }
    }

    //ポジションの初期化
    void MoveReset()
    {
        foreach (var obj in icon)
        {
            obj.obj.GetComponent<EffectIconInstant>().PosReset();
        }
    }

    //画像差し替え
    void ImageChange(int i,int num)
    {
        nameFrame[i].obj.GetComponent<Image>().sprite =
               UI_Image.GetUI_Image.effectIcon[num].nameFrame[i];

        iconFrame[i].obj.GetComponent<Image>().sprite =
            UI_Image.GetUI_Image.effectIcon[num].iconFrame[i];

        icon[i].obj.GetComponent<Image>().sprite =
            UI_Image.GetUI_Image.effectIcon[num].effectIcon;
    }
}
