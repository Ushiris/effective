using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 選択している場所にアーツを登録用のスクリプトを動かす
/// </summary>
public class EntryArtsUI : MonoBehaviour
{
    List<UI_ArtsMaterialization> artsUI_Plate;
    [SerializeField] GameObject artsUI_Obj;
    bool isStart;

    // Start is called before the first frame update
    void Start()
    {
        artsUI_Plate = new List<UI_ArtsMaterialization>();
        foreach (Transform childObj in artsUI_Obj.transform)
        {
            GameObject obj = childObj.gameObject.transform.GetChild(0).gameObject;
            artsUI_Plate.Add(obj.GetComponent<UI_ArtsMaterialization>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (UI_Manager.GetIsEffectFusionUI_ChoiceActive)
        {
            if (UI_Manager.GetEffectFusionUI_ChoiceNum.numList.Count > 1)
            {
                int num = UI_Manager.GetChoiceArtsDeckNum;
                artsUI_Plate[num].displaySwitch = OnTrigger();
            }
        }
    }

    bool OnTrigger()
    {
        return UI_Manager.ArtsEntryTrigger();
    }
}
