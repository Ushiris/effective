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
        {"07", "左クリックで前方にロケットを発射し、当たった場所で爆発する!"},// ロケットランチャー
        {"08", ""},// チャージガン
        {"09", "左クリック長押しで飛距離が伸びる矢を発射!"},// 弓矢
        // 射撃＋？
        {"12", "未完成"},// シールドバッシュ
        {"13", ""},// 
        {"14", "左クリックで自分の周りに攻撃!"},// 回転切り
        {"15", "左クリックで敵に近づき切りつける!"},// ファント
        {"16", ""},// 
        {"17", ""},// 
        {"18", "ため切り"},// ため切り
        {"19", "左クリックでジャンプ!そのまま剣で切りかかる!"},// ジャンプスラッシュ
        // 防御＋？
        {"23", ""},// 
        {"24", "左クリックで敵の弾を消す衝撃波を発生!"},// EMP
        {"25", "左クリックで自分の周りに３つ盾を出現させる!"},// 破られぬ盾
        {"26", ""},// 
        {"27", "左クリックで現在HPの半分を消費して自分を中心の爆発を起こす!"},// 自爆
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
        {"45", "左クリックでエフェクトの位置がわかる!"},// 探知
        {"46", ""},// 
        {"47", "左クリックで周りに煙幕を出し、敵が自分を見失う!"},// スモークグレネード
        {"48", "ザ・ワールド"},// ワールド
        {"49", "左クリックで周りの敵をふっとばす!"},// インパクト
        // 追尾＋？
        {"56", ""},// 
        {"57", "左クリックで自分の周りに爆弾を発生させ、敵が当たると爆発する!"},// ボマー
        {"58", "スロー"},// スロー
        {"59", "左クリックでついてくる天使を召喚!近くに敵がいると攻撃してくれる!"},// 天使降臨
        // 吸収＋？
        {"67", ""},// 
        {"68", "フリーズ"},// フリーズ
        {"69", "カウンター"},// カウンター
        // 爆発＋？
        {"78", "C4"},// C4
        {"79", "左クリックで投擲!着地地点で爆発する!"},// グレネード
        // 遅延＋？
        {"89", "ノックバック"},// ノックバック
        ///////////////////////////////////////////////////////////////
        // 射撃＋斬撃＋？
        {"012", "左クリックで一定時間無敵になる!"},// インビジブル
        {"013", ""},// 
        {"014", "左クリックで近距離に大ダメージ!"},// 乱れ突き
        {"015", "近くに敵がいるときに左クリック!敵を追いかけ攻撃する!"},// 召喚「ヴァルキリー」
        {"016", ""},// 
        {"017", "左クリックで自分の周りに3回攻撃をする!"},// ケロべロス
        {"018", ""},// 
        {"019", "左クリックで自分に戻ってくる斬撃を飛ばす!当たると跳ね返る!"},// カマイタチ
        // 射撃＋防御＋？
        {"023", ""},// 
        {"024", "左クリックで自分の周りに貫通するビーム発射!"},// ディフュージョン
        {"025", "左クリックで前方に敵を押し出す盾を発射!"},// 原初の盾
        {"026", ""},// 
        {"027", "左クリックで前方にビームを発射し、地面を爆破して攻撃する!"},// 殲滅光線
        {"028", ""},// 
        {"029", "左クリック長押しで狙いを定め離すと投擲!着地地点に瞬間移動!"},// ジャンプキューブ
        // 射撃＋設置＋？
        {"034", ""},// 
        {"035", ""},// 
        {"036", ""},// 
        {"037", ""},// 
        {"038", ""},// 
        {"039", ""},// 
        // 射撃＋拡散＋？
        {"045", "左クリックで追尾弾を大量に発射!"},// ハウンズ
        {"046", ""},// 
        {"047", "左クリックで地面や壁に当たると跳ねる弾を飛ばし、一定時間後爆発する!"},// 強者を食らうもの
        {"048", ""},// 
        {"049", "左クリックで前方に矢が降り注ぎ敵を攻撃する!"},// アローレイン
        // 射撃＋追尾＋？
        {"056", ""},// 
        {"057", "左クリックで近い敵の方向に爆弾を投擲する!"},// マーナガルム
        {"058", ""},// 
        {"059", "近くに敵がいるときに左クリック!敵を追いかけ攻撃する!"},// 召喚「ピクシー」
        // 射撃＋吸収＋？
        {"067", ""},// 
        {"068", ""},// 
        {"069", ""},// 
        // 射撃＋爆発＋？
        {"078", ""},// 
        {"079", "左クリックで前方に範囲攻撃する!"},// アマテラス
        // 射撃＋遅延＋？
        {"089", ""},// 
        ///////////////////////////////////////////////////////////////
        // 斬撃＋防御＋？
        {"123", ""},// 
        {"124", "ラッシュ"},// ラッシュ
        {"125", "左クリックで自分の周りに３つ剣を出現させる!"},// ソーディダンス
        {"126", ""},// 
        {"127", "左クリックで前方の地面に直線状の爆発を起こす!"},// インフェルノ
        {"128", ""},// 
        {"129", "未完成"},// ツバメ返し
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
        {"147", "左クリックで目の前を切りつけ、切った敵を爆発させる!"},// 鬼雨
        {"148", ""},// 
        {"149", "未完成"},// 六聖剣
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
        {"247", "左クリックで目の前に投擲!敵の動きを一定時間麻痺させる!"},// スタングレネード
        {"248", ""},// 
        {"249", "左クリックで周りで浮いている敵を地面に落とす!"},// イカロス
        // 防御＋追尾＋？
        {"256", ""},// 
        {"257", "左クリックで自分の周りに爆弾を発生させ、敵に近づくと爆弾が突っ込んでいき爆発する!"},// 這い寄るモノ
        {"258", ""},// 
        {"259", "左クリックで自分の周りに飛んでるものを消す空間を発生!"},// 飛翔の呪い
        // 防御＋吸収＋？
        {"267", ""},// 
        {"268", ""},// 
        {"269", ""},// 
        // 防御＋爆発＋？
        {"278", ""},// 
        {"279", "左クリックで前方に突撃!敵を弾き出す!"},// チャージドライブ
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
        {"457", "左クリックで前方に重力の渦が発生!一定時間経過で爆発!"},// ビックバン
        {"458", ""},// 
        {"459", "左クリックでポータルへ導く!"},// ディレクション
        // 拡散＋吸収＋？
        {"467", ""},// 
        {"468", ""},// 
        {"469", ""},// 
        // 拡散＋爆発＋？
        {"478", ""},// 
        {"479", "左クリックで前方に隕石が降り注ぎ敵を攻撃する!"},// メテオレイン
        // 拡散＋遅延＋？
        {"489", ""},// 
        ///////////////////////////////////////////////////////////////
        // 追尾＋吸収＋？
        {"567", ""},// 
        {"568", ""},// 
        {"569", ""},// 
        // 拡散＋爆発＋？
        {"578", ""},// 
        {"579", "左クリックでついてくるサラマンダーを召喚!敵に当たると爆発する弾を前方に投げる!"},// 召喚「サラマンダー」
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