using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 選択している場所にアーツを登録用のスクリプトを動かす
/// </summary>
public class EntryArtsUI : MonoBehaviour
{
    [SerializeField] UI_ArtsMaterialization[] artsUI_Plate = new UI_ArtsMaterialization[3];

    // Start is called before the first frame update
    void Start()
    {

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
