using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOccurrence : MonoBehaviour
{
    [SerializeField] private int numberOfEnemys; // 出現しているenemyの数
    [SerializeField] private GameObject[] enemys; // Enemyの入れ物

    private void Start()
    {
        numberOfEnemys = 0;
    }

    public void EnemyGenerate(Vector3 pos)
    {
        int randomValue = Random.Range(0, enemys.Length);//　出現させるenemyをランダムに選ぶ

        var enemyObj= Instantiate(enemys[randomValue], pos, Quaternion.Euler(0.0f, 0.0f, 0.0f));// enemyの生成

        //世界のレベルをエネミーのステータスに入れる
        var status = enemyObj.GetComponent<Status>();
        status.Lv = WorldLevel.GetWorldLevel;

        numberOfEnemys++;
    }
}