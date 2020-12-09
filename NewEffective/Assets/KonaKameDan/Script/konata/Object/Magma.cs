using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magma : MonoBehaviour
{
    [SerializeField] int damage = 100;
    [SerializeField, Header("時間からlapTimeを割った値")] int lapTime = 2;
    Life playerLife;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //継続ダメージ処理
            if ((int)Time.time % lapTime == 0)
            {
                if (playerLife == null)
                {
                    playerLife = collision.gameObject.GetComponent<Life>();
                }
                playerLife.Damage(damage);
            }
        }
    }
}
