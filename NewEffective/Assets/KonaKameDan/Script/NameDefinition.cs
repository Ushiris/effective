using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameDefinition : MonoBehaviour
{
    /// <summary>
    /// リザルトシーンのシーン名
    /// </summary>
    public static readonly string SceneName_Result= "ResultMenu";

    /// <summary>
    /// メインシーンのシーン名
    /// </summary>
    public static readonly string SceneName_Main = "main";

    /// <summary>
    /// ゲームを止めるシーン名
    /// </summary>
    public static readonly string SceneName_End = "GameEndScene";

    /// <summary>
    /// タイトルメニューのシーン名
    /// </summary>
    public static readonly string SceneName_TitleMenu = "TitleMenu";

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
    /// Artsのタイプ分け用
    /// </summary>
    public enum ArtsType { Attack, Defense, Technic, Nothing }


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
            case EffectName.Fly:
            case EffectName.Slow:
                return EffectColor.Blue;

            case EffectName.Homing:
            case EffectName.Barrier:
            case EffectName.Trap:
            case EffectName.Drain:
                return EffectColor.Green;;
            
            case EffectName.Nothing:
            default:
                return EffectColor.Nothing;
        }
    }

    /// <summary>
    /// Artsのタイプを取得する
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static ArtsType GetArtsType(string id)
    {
        switch (id)
        {
            case "024":
            case "045":
            case "049":
            case "057":
            case "079":
            case "257":
            case "457":
            case "479":
            case "02":
            case "04":
            case "05":
            case "07":
                return ArtsType.Attack;
                break;

            case "025":
            case "029":
            case "245":
            case "247":
            case "249":
            case "259":
            case "24":
            case "25":
            case "29":
            case "47":
            case "49":
                return ArtsType.Defense;
                break;

            case "027":
            case "047":
            case "059":
            case "279":
            case "459":
            case "579":
            case "09":
            case "27":
            case "45":
            case "57":
            case "59":
            case "79":
                return ArtsType.Technic;
                break;

            default: return ArtsType.Nothing; break;
        }
    }
}
