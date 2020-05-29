using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyArtsDeck : MonoBehaviour
{
    public List<ArtsList.ArtsData> GetMyArtsDeck = new List<ArtsList.ArtsData>();

    bool selectArtsReset;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GetMyArtsDeck.Add(new ArtsList.ArtsData());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (UI_Manager.ArtsEntryTrigger() && UI_Manager.GetIsEffectFusionUI_ChoiceActive)
        {
            int num = UI_Manager.GetChoiceArtsDeckNum;
            GetMyArtsDeck[num] = ArtsList.GetSelectArts;
        }
    }
}
