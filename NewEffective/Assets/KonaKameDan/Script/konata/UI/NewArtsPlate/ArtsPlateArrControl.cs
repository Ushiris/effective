﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 複数あるアーツプレートに支持を送る
/// </summary>
public class ArtsPlateArrControl : MonoBehaviour
{
    //[SerializeField] int selectArtsPlate;
    //[SerializeField] string artsId; 
    [SerializeField] ArtsPlateManager[] artsPlateArr;

    string tmpArtsId;

    private void Start()
    {
        //アーツUI、シーンまたいだ時の保持用
        if (!MainGameManager.GetArtsReset)
        {
            if (MyArtsDeck.GetArtsDeck != null)
            {
                for (int i = 0; i < MyArtsDeck.GetArtsDeck.Count; i++)
                {
                    if (MyArtsDeck.GetArtsDeck[i].id != null)
                    {
                        DebugLogger.Log(MyArtsDeck.GetArtsDeck[i].name);
                        artsPlateArr[i].OnArtsPlateChange(MyArtsDeck.GetArtsDeck[i].id);
                    }
                }
            }
        }

        MyArtsDeck.SetArtsSelectAfterProcess += () =>
        {
            artsPlateArr[SelectArtsPlateNum()].OnArtsPlateChange(GetArtsId());
            tmpArtsId = GetArtsId();
        };
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if(tmpArtsId != GetArtsId())
    //    {
    //        artsPlateArr[SelectArtsPlateNum()].OnArtsPlateChange(GetArtsId());
    //        tmpArtsId = GetArtsId();
    //    }
    //}

    int SelectArtsPlateNum()
    {
        return UI_Manager.GetChoiceArtsDeckNum;//selectArtsPlate;
    }

    string GetArtsId()
    {
        return MyArtsDeck.GetSelectArtsDeck.id;//artsId;
    }
}
