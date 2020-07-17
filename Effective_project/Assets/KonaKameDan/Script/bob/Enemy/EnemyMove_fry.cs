using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove_fry : MonoBehaviour
{
    private Transform player;
    private Rigidbody rb;
    private float speed = 2;
    private float fryPosUp = 5;
    private float fryPosDown = 3;

    void Start()
    {
        Debug.Log("メインシーン実装時にはここをクリックしてコメントをとっておいて！");
        //player = PlayerManager.GetManager.GetPlObj.transform;
        rb = GetComponent<Rigidbody>();
        transform.position = new Vector3(transform.position.x, Random.Range(fryPosDown, fryPosUp), transform.position.z);
    }

    void Update()
    {
        Debug.Log("メインシーン実装時にはここをクリックしてコメントをとっておいて！");
        //transform.LookAt(player);

        //if (Vector3.Distance(player.position, transform.position) < 3)// プレイヤーが範囲内にいるとき
        //{
        //    rb.velocity = Vector3.zero;
        //}
        //else// プレイヤーが範囲内にいないとき
        //{
        //    rb.velocity = transform.forward * speed;
        //}

        if (this.transform.position.y > fryPosUp)
        {
            rb.velocity = transform.up * -speed;
        }
        if (this.transform.position.y < fryPosDown)
        {
            rb.velocity = transform.up * speed;
        }
    }
}
