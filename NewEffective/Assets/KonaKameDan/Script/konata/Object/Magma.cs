using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magma : MonoBehaviour
{
    [SerializeField] int damage = 100;
    [SerializeField, Header("時間からlapTimeを割った値")] int lapTime = 2;
    Life playerLife;

    // Start is called before the first frame update
    void Start()
    {
        playerLife = PlayerManager.GetManager.GetPlObj.GetComponent<Life>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //継続ダメージ処理
            if ((int)Time.time % lapTime == 0)
            {
                playerLife.Damage(damage);
            }
        }
    }
}
