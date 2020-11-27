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
    enum ICON { Left, Center, Right, End }
    enum NAME_FRAME { Outside, Center, Inside, End }

    [SerializeField] float interval = 0.3f;
    public bool displaySwitch = true;
    public int deckNum;

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
        new Obj{name="leftIconFrame"},
        new Obj{name="centerIconFrame"},
        new Obj{name="rightIconFrame"},    
    };

    [SerializeField, Header("アイコン")]
    List<Obj> icon = new List<Obj>()
    {
        new Obj{name="leftIcon"},
        new Obj{name="centerIcon"},
        new Obj{name="rightIcon"},
    };

    [SerializeField] TextMeshProUGUI artsNameText;

    ActiveImageDelay[] iconImageDelay = new ActiveImageDelay[(int)ICON.End];
    ActiveImageDelay[] iconFrameImageDelay = new ActiveImageDelay[(int)ICON.End];
    ActiveImageDelay[] nameFrameImageDelay = new ActiveImageDelay[(int)NAME_FRAME.End];
    ActiveTextMeshProDelay artsNameTextDelay;

    static readonly int kDisplayTurnMaxCount = 3;
    static readonly bool kIsIconSetPlaySe = false;
    static readonly bool kIsIconFrameSetPlaySe = true;
    static readonly bool kIsArtsNameFrameSetPlaySe = false;
    static readonly bool kIsArtsNameSetPlaySe = true;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = (int)ICON.End - 1; i >= 0; i--)
        {
            iconImageDelay[i] = icon[i].obj.GetComponent<ActiveImageDelay>();
            iconFrameImageDelay[i] = iconFrame[i].obj.GetComponent<ActiveImageDelay>();

            //エフェクトアイコンが出現するときのSEを流す処理の追加
            iconImageDelay[i].onPlaySe = () => { UI_Manager.EffectIconSetPlaySe(); };
            //エフェクトアイコンフレームが出現するときのSEを流す処理の追加
            iconFrameImageDelay[i].onPlaySe = () => { UI_Manager.EffectIconFrameSetPlaySe(); };
        }
        for (int i = 0; i < (int)NAME_FRAME.End; i++)
        {
            nameFrameImageDelay[i]= nameFrame[i].obj.GetComponent<ActiveImageDelay>();

            //アーツ名フレームが出現するときのSEを流す処理の追加
            nameFrameImageDelay[i].onPlaySe = () => { UI_Manager.EffectNameFrameSetPlaySe(); }
;        }

        artsNameTextDelay = artsNameText.GetComponent<ActiveTextMeshProDelay>();

        //アーツ名が出現するときにSEを流す処理の追加
        artsNameTextDelay.onPlaySe = () => { UI_Manager.ArtsNameSetPlaySe(); };


        //初期化
        IntervalDisplay(false, kDisplayTurnMaxCount);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Process(displaySwitch);
        if (displaySwitch) displaySwitch = false;
    }

    void Process(bool on)
    {
        //count = UI_Manager.GetEffectFusionUI_ChoiceNum.numList.Count;
        count = MyArtsDeck.GetArtsDeck[deckNum].effectList.Count;

        if (on)
        {

            //画面をリセット
            IntervalDisplay(false, kDisplayTurnMaxCount);
            MoveReset();

            //画像を切り替える
            for (int i = 0; i < count; i++)
            {
                //int num = UI_Manager.GetEffectFusionUI_ChoiceNum.numList[i];
                int num = MyArtsDeck.GetArtsDeck[deckNum].effectList[i];
                DebugLogger.Log(i + " アーツ: " + num);
                ImageChange(i, num);
            }

            //名称を切り替える
            //artsNameText.text = ArtsList.GetSelectArts.name;
            artsNameText.text = MyArtsDeck.GetArtsDeck[deckNum].name;

            //画像を表示する
            IntervalDisplay(true, count, interval);
        }
    }

    //表示＆表示にかかる時間設定
    void IntervalDisplay(bool on,int count, float intervalTime = 0)
    {
        if (count == 3 || count == kDisplayTurnMaxCount)
        {
            //for (int i = 0; i < displayTurn.Count; i++)
            //{
            //    displayTurn[i].GetComponent<ActiveImageDelay>().Delay(on, intervalTime * i);
            //}
            iconImageDelay[(int)ICON.Right].Delay(on, intervalTime * 0);
            iconImageDelay[(int)ICON.Center].Delay(on, intervalTime * 1);
            iconImageDelay[(int)ICON.Left].Delay(on, intervalTime * 2);

            iconFrameImageDelay[(int)ICON.Right].Delay(on, intervalTime * 0.2f);
            iconFrameImageDelay[(int)ICON.Center].Delay(on, intervalTime * 1.2f);
            iconFrameImageDelay[(int)ICON.Left].Delay(on, intervalTime * 2.2f);

            nameFrameImageDelay[(int)NAME_FRAME.Outside].Delay(on, intervalTime * 2.3f);
            nameFrameImageDelay[(int)NAME_FRAME.Center].Delay(on, intervalTime * 2.6f);
            nameFrameImageDelay[(int)NAME_FRAME.Inside].Delay(on, intervalTime * 2.9f);

            //文字の表示
            artsNameTextDelay.Delay(on, intervalTime * 4);
        }
        else if (count == 2)
        {
            iconImageDelay[(int)ICON.Center].Delay(on, intervalTime * 0);
            iconImageDelay[(int)ICON.Left].Delay(on, intervalTime * 1);

            iconFrameImageDelay[(int)ICON.Center].Delay(on, intervalTime * 0.2f);
            iconFrameImageDelay[(int)ICON.Left].Delay(on, intervalTime * 1.2f);

            nameFrameImageDelay[(int)NAME_FRAME.Outside].Delay(on, intervalTime * 1.3f);
            nameFrameImageDelay[(int)NAME_FRAME.Center].Delay(on, intervalTime * 1.6f);

            //文字の表示
            artsNameTextDelay.Delay(on, intervalTime * 2.5f);
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
