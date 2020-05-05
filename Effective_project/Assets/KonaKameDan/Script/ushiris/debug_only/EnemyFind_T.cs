using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class EnemyFind_T : MonoBehaviour
{
    [SerializeField]
    KeyCode debug_key;

    [SerializeField]
    GameObject test_target;

    [SerializeField]
    GameObject player;

    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    private void Update()
    {
        if (Input.GetKey(debug_key))
        {
            search();
        }
    }

    public void search()
    {
        GameObject enemy = test_target.GetComponent<EnemyFind>().GetNearEnemyPos(player.transform.position);
        Debug.Log("Near enemy is:" + enemy.name);
    }
}
