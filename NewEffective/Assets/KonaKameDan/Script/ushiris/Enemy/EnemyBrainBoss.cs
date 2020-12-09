using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrainBoss : EnemyBrainBase
{
    /*
     入れるもの
    ・ボスはターゲットが存在しない場合、ポータルの前に居る
    ・ボスはポータルから離れることができない
    　(一定の距離、離れるとポータルの位置に戻る)
    ・複数のアーツを使用する(1～3のアーツを同時に)
    ・使用するアーツによってボスの色またはパーティクルを出す
    ・プレイヤーがポータルに近づくと激しく攻撃をする
　   1~3のアーツを使っていたところ3つに常になるとか、速度が上がるとか
     */

    EnemyArtsInstant attackManager;
    EnemyArtsPickUp artsSelector;

    private new void Awake()
    {
        base.Awake();
        attackManager = GetComponent<EnemyArtsInstant>();
        artsSelector = GetComponent<EnemyArtsPickUp>();
    }

    void ArtsFire()
    {
        List<string> artsList = artsSelector.ArtsTable;

    }
}
