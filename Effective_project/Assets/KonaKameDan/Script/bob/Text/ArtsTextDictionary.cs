﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// artsの使用方法の説明文
/// </summary>
public class ArtsTextDictionary : MonoBehaviour
{
    public Dictionary<string, string> ArtsText = new Dictionary<string, string>()
    {
     /* {"artsのID", "説明文"}, */
        // 斬撃＋？
        {"01", "左クリックで斬撃が飛んでいく!"},// スーラッシュ
        {"02", "左クリックで敵を貫通するビーム発射!"},// ビーム
        {"03", ""},// 
        {"04", "左クリックで近距離に大ダメージ!"},// ショットガン
        {"05", "左クリックで追尾弾を発射!"},// ホーミング
        {"06", ""},// 
        {"07", ""},// ロケットランチャー
        {"08", ""},// チャージガン
        {"09", "左クリック長押しで飛距離が伸びる矢を発射!"},// 弓矢
        // 射撃＋？
        {"12", "シールドバッシュ"},// シールドバッシュ
        {"13", ""},// 
        {"14", "左クリックで自分の周りに攻撃!"},// 回転切り
        {"15", "左クリックで敵に近づき切りつける!"},// ファント
        {"16", ""},// 
        {"17", ""},// 
        {"18", "ため切り"},// ため切り
        {"19", "ジャンプ切り"},// ジャンプ切り
        // 防御＋？
        {"23", ""},// 
        {"24", "左クリックで敵の弾を消す衝撃波を発生!"},// EMP
        {"25", "左クリックで自分の周りに３つ盾を出現させる!"},// 破られぬ盾
        {"26", ""},// 
        {"27", ""},// 
        {"28", "パーソナルシールド"},// パーソナルシールド
        {"29", "左クリックで後方に回避!"},// 回避
        // 設置＋？
        {"34", ""},// 
        {"35", ""},// 
        {"36", ""},// 
        {"37", "センサーマイン"},// センサーマイン
        {"38", "グラビティ"},// グラビティ
        {"39", "ジャンプパット"},// ジャンプパット
        // 拡散＋？
        {"45", "左クリック!エフェクトの位置がわかる!"},// 探知
        {"46", ""},// 
        {"47", ""},// 
        {"48", "ザ・ワールド"},// ワールド
        {"49", "左クリックで周りの敵をふっとばす!"},// インパクト
        // 追尾＋？
        {"56", ""},// 
        {"57", ""},// 
        {"58", "スロー"},// スロー
        {"59", "左クリックでついてくる天使を召喚!近くに敵がいると攻撃する!"},// 天使降臨
        // 吸収＋？
        {"67", ""},// 
        {"68", "フリーズ"},// フリーズ
        {"69", "カウンター"},// カウンター
        // 爆発＋？
        {"78", "C4"},// C4
        {"79", "グレネード"},// グレネード
        // 遅延＋？
        {"89", "ノックバック"},// ノックバック
        ///////////////////////////////////////////////////////////////
        // 射撃＋斬撃＋？
        {"012", "インビジブル"},// インビジブル
        {"013", ""},// 
        {"014", "乱れ突き"},// 乱れ突き
        {"015", "召喚「ヴァルキリー」"},// 召喚「ヴァルキリー」
        {"016", ""},// 
        {"017", "pケルベロス"},// pケルベロス
        {"018", ""},// 
        {"019", "カマイタチ"},// カマイタチ
        // 射撃＋防御＋？
        {"023", ""},// 
        {"024", "左クリックで自分の周りに貫通するビーム発射!"},// ディフュージョン
        {"025", "左クリックで前方に敵を押し出す盾を発射!"},// 原初の盾
        {"026", ""},// 
        {"027", ""},// 
        {"028", ""},// 
        {"029", "左クリックで軌道が見え、もう一度左クリックすると投擲!着地地点に瞬間移動!"},// ジャンプキューブ
        // 射撃＋設置＋？
        {"034", ""},// 
        {"035", ""},// 
        {"036", ""},// 
        {"037", ""},// 
        {"038", ""},// 
        {"039", ""},// 
        // 射撃＋拡散＋？
        {"045", "左クリック!追尾弾を発射!"},// ハウンズ
        {"046", ""},// 
        {"047", ""},// 
        {"048", ""},// 
        {"049", "左クリック長押しで狙いをつけて、離すと範囲に矢が落ちてくる!"},// アローレイン
        // 射撃＋追尾＋？
        {"056", ""},// 
        {"057", ""},// 
        {"058", ""},// 
        {"059", "近くに敵がいるときに左クリック!敵を追いかけ攻撃する!"},// 召喚「ピクシー」
        // 射撃＋吸収＋？
        {"067", ""},// 
        {"068", ""},// 
        {"069", ""},// 
        // 射撃＋爆発＋？
        {"078", ""},// 
        {"079", ""},// 
        // 射撃＋遅延＋？
        {"089", ""},// 
        ///////////////////////////////////////////////////////////////
        // 斬撃＋防御＋？
        {"123", ""},// 
        {"124", "ラッシュ"},// ラッシュ
        {"125", "ソーディダンス"},// ソーディダンス
        {"126", ""},// 
        {"127", ""},// 
        {"128", ""},// 
        {"129", "ツバメ返し"},// ツバメ返し
        // 斬撃＋設置＋？
        {"134", ""},// 
        {"135", ""},// 
        {"136", ""},// 
        {"137", ""},// 
        {"138", ""},// 
        {"139", ""},// 
        // 斬撃＋拡散＋？
        {"145", "左クリックで敵に一瞬で近づき連続で切りつける!"},// ムラサメ
        {"146", ""},// 
        {"147", ""},// 
        {"148", ""},// 
        {"149", "六聖剣"},// 六聖剣
        // 斬撃＋追尾＋？
        {"156", ""},// 
        {"157", ""},// 
        {"158", ""},// 
        {"159", "左クリックで周りの敵全員を切りつける!"},// ハヤブサ
        // 斬撃＋吸収＋？
        {"167", ""},// 
        {"168", ""},// 
        {"169", ""},// 
        // 斬撃＋爆発＋？
        {"178", ""},// 
        {"179", ""},// 
        // 斬撃＋遅延＋？
        {"189", ""},// 
        ///////////////////////////////////////////////////////////////
        // 防御＋設置＋？
        {"234", ""},// 
        {"235", ""},// 
        {"236", ""},// 
        {"237", ""},// 
        {"238", ""},// 
        {"239", ""},// 
        // 防御＋拡散＋？
        {"245", "左クリックで敵の弾を消す衝撃波を出すキューブが出現!"},// EMPキューブ
        {"246", ""},// 
        {"247", ""},// 
        {"248", ""},// 
        {"249", "左クリックで周りで浮いている敵を地面に落とす!"},// イカロス
        // 防御＋追尾＋？
        {"256", ""},// 
        {"257", ""},// 
        {"258", ""},// 
        {"259", "左クリックで自分の周りに飛んでるものを消す空間を発生!"},// 飛翔の呪い
        // 防御＋吸収＋？
        {"267", ""},// 
        {"268", ""},// 
        {"269", ""},// 
        // 防御＋爆発＋？
        {"278", ""},// 
        {"279", ""},// 
        // 防御＋遅延＋？
        {"289", ""},// 
        ///////////////////////////////////////////////////////////////
        // 設置＋拡散＋？
        {"345", ""},// 
        {"346", ""},// 
        {"347", ""},// 
        {"348", ""},// 
        {"349", ""},// 
        // 設置＋追尾＋？
        {"356", ""},// 
        {"357", ""},// 
        {"358", ""},// 
        {"359", ""},// 
        // 設置＋吸収＋？
        {"367", ""},// 
        {"368", ""},// 
        {"369", ""},// 
        // 設置＋爆発＋？
        {"378", ""},// 
        {"379", ""},// 
        // 設置＋遅延＋？
        {"389", ""},// 
        ///////////////////////////////////////////////////////////////
        // 拡散＋追尾＋？
        {"456", ""},// 
        {"457", ""},// 
        {"458", ""},// 
        {"459", "左クリックでポータルへ導く!"},// ディレクション
        // 拡散＋吸収＋？
        {"467", ""},// 
        {"468", ""},// 
        {"469", ""},// 
        // 拡散＋爆発＋？
        {"478", ""},// 
        {"479", ""},// 
        // 拡散＋遅延＋？
        {"489", ""},// 
        ///////////////////////////////////////////////////////////////
        // 追尾＋吸収＋？
        {"567", ""},// 
        {"568", ""},// 
        {"569", ""},// 
        // 拡散＋爆発＋？
        {"578", ""},// 
        {"579", ""},// 
        // 拡散＋遅延＋？
        {"589", ""},// 
        ///////////////////////////////////////////////////////////////
        // 吸収＋爆発＋？
        {"678", ""},// 
        {"679", ""},// 
        // 吸収＋遅延＋？
        {"689", ""},// 
        ///////////////////////////////////////////////////////////////
        // 爆発＋遅延＋？
        {"789", ""},// 
    };
}