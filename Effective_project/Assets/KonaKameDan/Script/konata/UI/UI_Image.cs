using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class UI_Image : MonoBehaviour
{
    [NamedArrayAttribute(new string[]
    {
        "射撃","斬撃","防御","設置","拡散","追尾","吸収","爆発","遅延","飛翔"
    })]
    public List<Sprite> effectIconList = new List<Sprite>();

    public enum EffectName
    {
        射撃, 斬撃, 防御, 設置, 拡散, 追尾, 吸収, 爆発, 遅延, 飛翔
    }

    [SerializeField] Sprite[] redIconFrame = new Sprite[3];
    [SerializeField] Sprite[] redNameFrame = new Sprite[3];

    [SerializeField] Sprite[] blueIconFrame = new Sprite[3];
    [SerializeField] Sprite[] blueNameFrame = new Sprite[3];

    [System.Serializable]
    public class ArtsDisplayUI
    {
        [HideInInspector]public string name;
        public Sprite effectIcon;
        public List<Sprite> iconFrame = new List<Sprite>();
        public List<Sprite> nameFrame = new List<Sprite>();
    }
    public List<ArtsDisplayUI> effectIcon = new List<ArtsDisplayUI>()
    {
        new ArtsDisplayUI{name="射撃"},
        new ArtsDisplayUI{name="斬撃"},
        new ArtsDisplayUI{name="防御"},
        new ArtsDisplayUI{name="設置"},
        new ArtsDisplayUI{name="拡散"},
        new ArtsDisplayUI{name="追尾"},
        new ArtsDisplayUI{name="吸収"},
        new ArtsDisplayUI{name="爆発"},
        new ArtsDisplayUI{name="遅延"},
        new ArtsDisplayUI{name="飛翔"}
    };
    //public static Dictionary<string, ArtsDisplayUI> GetEffectIcon { get; private set; }
    public static UI_Image GetUI_Image { get; private set; }

    private void Awake()
    {
        //GetEffectIcon = new Dictionary<string, ArtsDisplayUI>();

        //仮置き
        for (int i = 0; i < effectIcon.Count; i++)
        {
            for (int f = 0; f < 3; f++)
            {
                if (i == 0)
                {
                    effectIcon[i].iconFrame.Add(redIconFrame[f]);
                    effectIcon[i].nameFrame.Add(redNameFrame[f]);

                    //GetEffectIcon.Add(effectIcon[i].name, effectIcon[i]);
                }
                else
                {
                    effectIcon[i].iconFrame.Add(blueIconFrame[f]);
                    effectIcon[i].nameFrame.Add(blueNameFrame[f]);

                    //GetEffectIcon.Add(effectIcon[i].name, effectIcon[i]);
                }
            }
        }


        GetUI_Image = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}
