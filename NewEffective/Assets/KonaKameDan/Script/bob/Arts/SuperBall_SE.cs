using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperBall_SE : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(
            collision.gameObject.tag != "Enemy" &&
            collision.gameObject.tag != "Player" &&
            collision.gameObject.tag != "NearEnemyPos" &&
            collision.gameObject.tag != "BossZone" &&
            collision.gameObject.tag != "EnemySpawnZone"
           )
        {
            //SE
            Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id047_PingPong_second, transform.position, null);
        }
    }
}
