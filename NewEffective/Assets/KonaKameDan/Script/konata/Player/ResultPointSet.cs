using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPointSet : MonoBehaviour
{
    [SerializeField] MyEffectCount effectBag;

    private void OnDestroy()
    {
        ResultPoint.SetPoint[ResultPoint.PointName.Shot] = effectBag.effectCount[NameDefinition.EffectName.Shot];
        ResultPoint.SetPoint[ResultPoint.PointName.Barrier] = effectBag.effectCount[NameDefinition.EffectName.Barrier];
        ResultPoint.SetPoint[ResultPoint.PointName.Spread] = effectBag.effectCount[NameDefinition.EffectName.Spread];
        ResultPoint.SetPoint[ResultPoint.PointName.Homing] = effectBag.effectCount[NameDefinition.EffectName.Homing];
        ResultPoint.SetPoint[ResultPoint.PointName.Explosion] = effectBag.effectCount[NameDefinition.EffectName.Explosion];
        ResultPoint.SetPoint[ResultPoint.PointName.Fly] = effectBag.effectCount[NameDefinition.EffectName.Fly];
    }
}
