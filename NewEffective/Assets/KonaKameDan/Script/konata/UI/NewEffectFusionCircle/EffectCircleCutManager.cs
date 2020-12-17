using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectCircleCutManager : MonoBehaviour
{
    [SerializeField] Text effectCount;

    public int effectId;
    public FusionCircleColorControl pieceColorControl;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        var effectName = (NameDefinition.EffectName)effectId;
        effectCount.text = "x" + EffectObjectAcquisition.GetEffectBag.effectCount[effectName];
    }
}
