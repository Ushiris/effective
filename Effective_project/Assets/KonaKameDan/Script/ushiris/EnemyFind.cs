using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFind : MonoBehaviour
{
    List<GameObject> enemy = new List<GameObject>();

    static readonly string enemyTag = "Enemy";

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == enemyTag)
        {
            enemy.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == enemyTag)
        {
            Debug.Log("aaa");
            enemy.Remove(other.gameObject);
        }
    }

    public List<GameObject> GetEnemyTransform()
    {
        return enemy;
    }

    //射撃等のターゲットにすべき敵を取得します。
    public GameObject GetNearEnemyPos(Vector3 pivot)
    {
        if (enemy.Count == 0) return null; //敵が見当たらない場合にnullを返します。

        GameObject result=null;
        enemy.ForEach((e) => { if (e != null) result = e; });
        if (result == null) return null;

        float dist_min = Vector3.Distance(pivot, result.transform.position);

        enemy.ForEach((tr) =>
        {
            if (tr == null) return;

            var dist_x = Vector3.Distance(pivot, tr.transform.position);
            if (dist_min > dist_x)
            {
                result = tr;
                dist_min = dist_x;
            }
        });

        return result;
    }

}
