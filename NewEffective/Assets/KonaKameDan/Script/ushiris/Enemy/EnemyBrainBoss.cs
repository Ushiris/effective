using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrainBoss : MonoBehaviour
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
    [SerializeField] ParticleSystem artsFireParticle;
    [SerializeField] GameObject muzzle;
    [SerializeField] EnemyBrainBase brain;

    GameObject player;
    EnemyArtsInstant attackManager;
    EnemyArtsPickUp artsSelector;
    Transform portal_tr;
    Life life;

    private void Awake()
    {
        attackManager = GetComponent<EnemyArtsInstant>();
        artsSelector = GetComponent<EnemyArtsPickUp>();
        life = GetComponent<Life>();
    }

    private void Start()
    {
        player = PlayerManager.GetManager.GetPlObj;
        portal_tr = GameObject.Find("Portal").transform;
        brain.AIset(StayAItype.Return);
        brain.LockAI();
        life.AddBeat(CheckArea);
    }

    void CheckArea()
    {
        //発狂モード
        if (Vector3.Distance(player.transform.position, portal_tr.position) < EnemyProperty.BossAngryDistance)
        {
            attackManager.ChangeAction(() => ArtsFire(Random.Range(2, 4)));
        }
        //警告モード
        else if (Vector3.Distance(transform.position, portal_tr.position) > EnemyProperty.PortalDefenceDistance)
        {
            attackManager.ChangeAction(() => ArtsFire(Random.Range(1, 2)));
        }
        //迎撃モード
        else
        {
            attackManager.ChangeAction(() => ArtsFire(Random.Range(1, 3)));
        }
    }

    void ArtsFire(int amount)
    {
        List<string> artsList = artsSelector.ArtsTable;
        for(uint i = 0; i < amount; i++)
        {
            ArtsInstantManager.InstantArts(muzzle, artsList[Random.Range(0, artsList.Count)]);
        }
        artsFireParticle.Play();
    }
}
