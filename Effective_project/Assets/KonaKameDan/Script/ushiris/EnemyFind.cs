using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFind : MonoBehaviour
{
    List<GameObject> enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            enemy.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            enemy.Remove(other.gameObject);
        }
    }

    public List<GameObject> GetEnemyTransform()
    {
        return enemy;
    }

#nullable enable 
    //射撃等のターゲットにすべき敵を取得します。
    public GameObject? GetNearEnemyPos(Vector3 pivot)
    {
        if (enemy.Count == 0) return null; //敵が見当たらない場合にnullを返します。

        GameObject result = enemy[0];
        float dist_min = Vector3.Distance(pivot, result.transform.position);

        enemy.ForEach((tr) =>
        {
            var dist_x = Vector3.Distance(pivot, tr.transform.position);
            if (dist_min < dist_x)
            {
                result = tr;
                dist_min = dist_x;
            }
        });

        return result;
    }
#nullable disable

}
