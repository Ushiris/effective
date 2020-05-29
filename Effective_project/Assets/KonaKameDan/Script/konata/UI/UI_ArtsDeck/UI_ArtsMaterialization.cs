using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ArtsMaterialization : MonoBehaviour
{
    enum ICON { Right, Center, Left }
    enum NAME_FRAME { Outside, Center, Inside }

    public bool displaySwitch;
    bool isReset = true;
    [SerializeField] float interval = 0.3f;


    [System.Serializable]
    public class Obj
    {
        [HideInInspector] public string name;
        public GameObject obj;
    }

    [SerializeField]
    List<Obj> nameFrame = new List<Obj>()
    {
        new Obj{name="outsideNameFrame"},
        new Obj{name="centerNameFrame"},
        new Obj{name="insideNameFrame"},
    };

    [SerializeField]
    List<Obj> iconFrame = new List<Obj>()
    {
        new Obj{name="rightIconFrame"},
        new Obj{name="centerIconFrame"},
        new Obj{name="leftIconFrame"},
    };

    [SerializeField]
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
    }

    // Update is called once per frame
    void Update()
    {
        int count = UI_Manager.GetEffectFusionUI_ChoiceNum.numList.Count;

        if (displaySwitch)
        {
            for (int i = 0; i < count; i++)
            {
                int num = UI_Manager.GetEffectFusionUI_ChoiceNum.numList[i];
                ImageChange(i, num);
            }

            //名称
            artsNameText.text = ArtsList.GetSelectArts.name;

            IntervalDisplay(true, count, interval);
            isReset = true;
        }
        else
        {
            if (isReset)
            {
                IntervalDisplay(false, displayTurn.Count);
                isReset = false;
            }
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

            artsNameText.GetComponent<ActiveTextMeshProDelay>().Delay(on, intervalTime * 6);
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
