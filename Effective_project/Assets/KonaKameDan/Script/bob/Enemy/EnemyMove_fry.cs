using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove_fry : MonoBehaviour
{
    private float enemyStartPos_y;// エネミーの初期位置
    private float enemyNowPos_y;// エネミーの期待現在位置
    [SerializeField] private float sin;
    public float shaking = 0.5f;// 揺れ幅
    public float vibration = 10.0f;// 振動回数
    private float f;// 振動数
    public float fryPosUp = 5;
    public float fryPosDown = 3;

    void Start()
    {
        f = 1.0f / vibration;// 振動数
        transform.position = new Vector3(transform.position.x, Random.Range(fryPosDown, fryPosUp), transform.position.z);
        enemyStartPos_y = this.transform.position.y;
    }

    void Update()
    {
        sin = Mathf.Sin(2 * Mathf.PI * f * Time.time) * shaking;
        enemyNowPos_y = transform.parent.gameObject.transform.position.y + enemyStartPos_y + sin;// 揺れ加減
        this.transform.position = new Vector3(transform.position.x, enemyNowPos_y, transform.position.z);
    }
}
