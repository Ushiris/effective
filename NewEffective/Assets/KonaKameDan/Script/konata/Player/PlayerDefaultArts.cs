﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefaultArts : MonoBehaviour
{
    [SerializeField] string artsId = "04";

    bool isArtsGet;

    // Update is called once per frame
    void Start()
    {
        if (isArtsGet || !MainGameManager.GetArtsReset) return;
        var myArtsDeck = PlayerManager.GetManager.myArtsDeck;
        myArtsDeck.OnArtsSelfSet(0, ArtsList.GetLookedForArts(artsId));
        isArtsGet = true;
    }
}
