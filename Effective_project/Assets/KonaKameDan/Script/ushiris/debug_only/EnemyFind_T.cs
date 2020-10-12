using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFind_T : MonoBehaviour
{
    [SerializeField]
    KeyCode debug_key;

    [SerializeField]
    GameObject test_target;

    [SerializeField]
    GameObject player;

    private void Update()
    {
        if (Input.GetKeyDown(debug_key))
        {
            search();
        }
    }

    public void search()
    {
        GameObject enemy = test_target.GetComponent<EnemyFind>().GetNearEnemyPos(player.transform.position);
        if (enemy == null)
        {
            DebugLogger.Log("Enemy not found");
            return;
        }

        DebugLogger.Log("Near enemy is:" + enemy.name);
        DebugLogger.Log("Enemy list count:" + test_target.GetComponent<EnemyFind>().GetEnemyTransform().Count);
    }
}
