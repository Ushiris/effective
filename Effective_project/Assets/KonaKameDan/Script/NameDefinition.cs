using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameDefinition : MonoBehaviour
{
    /// <summary>
    /// Shot=射撃,Slash=斬撃,Barrier=防御,Trap=設置,Spread=拡散,Homing=追尾,Drain=吸収,Explosion=爆発,Slow=遅延,Fly=飛翔
    /// </summary>
    public enum EffectName
    {
        Shot, Slash, Barrier, Trap, Spread, Homing, Drain, Explosion, Slow, Fly
    }
}
