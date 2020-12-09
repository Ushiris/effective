using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDustParticle_SE : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Id479_MeteorRain_second);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (
            collision.gameObject.tag != "Enemy" &&
            collision.gameObject.tag != "Player" &&
            collision.gameObject.tag != "NearEnemyPos" &&
            collision.gameObject.tag != "BossZone" &&
            collision.gameObject.tag != "EnemySpawnZone"
           )
        {
            //SE
            SE_Manager.SePlay(SE_Manager.SE_NAME.Id047_PingPong_third);
        }
    }
}
