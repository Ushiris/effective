using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameDefinition : MonoBehaviour
{
    /// <summary>
    /// リザルトシーンのシーン名
    /// </summary>
    public static readonly string SceneName_Result= "Result";

    /// <summary>
    /// メインシーンのシーン名
    /// </summary>
    public static readonly string SceneName_Main = "main";

    /// <summary>
    /// Shot=射撃,Slash=斬撃,Barrier=防御,Trap=設置,Spread=拡散,Homing=追尾,Drain=吸収,Explosion=爆発,Slow=遅延,Fly=飛翔
    /// </summary>
    public enum EffectName
    {
        Shot, Slash, Barrier, Trap, Spread, Homing, Drain, Explosion, Slow, Fly, Nothing
    }

    /// <summary>
    /// Effectの色分け用
    /// </summary>
    public enum EffectColor { Red, Blue, Green, Nothing }


    /// <summary>
    /// EffectNameをkeyに日本語の名前を引っ張ってくる
    /// </summary>
    public static Dictionary<EffectName, string> GetEffectJapanName { get; } = new Dictionary<EffectName, string>()
    {
        {EffectName.Shot,       "射撃" },
        {EffectName.Slash,      "斬撃" },
        {EffectName.Barrier,    "防御" },
        {EffectName.Trap,       "設置" },
        {EffectName.Spread,     "拡散" },
        {EffectName.Homing,     "追尾" },
        {EffectName.Drain,      "吸収" },
        {EffectName.Explosion,  "爆発" },
        {EffectName.Slow,       "遅延" },
        {EffectName.Fly,        "飛翔" },
        {EffectName.Nothing,    "なし" }
    };

    /// <summary>
    /// エフェクトの名前からエフェクトの色を持ってきます
    /// </summary>
    /// <param name="effectName"></param>
    /// <returns></returns>
    public static EffectColor GetEffectColor(EffectName effectName)
    {
        switch (effectName)
        {
            case EffectName.Shot:
            case EffectName.Slash:
            case EffectName.Explosion:
                return EffectColor.Red;

            case EffectName.Spread:
            case EffectName.Homing:
            case EffectName.Fly:
            case EffectName.Slow:
                return EffectColor.Blue;

            case EffectName.Barrier:
            case EffectName.Trap:
            case EffectName.Drain:
                return EffectColor.Green;;
            
            case EffectName.Nothing:
            default:
                return EffectColor.Nothing;
        }
    }
}
