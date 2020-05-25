using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDamage_T : MonoBehaviour
{
    public KeyCode debug_key = KeyCode.G;
    public GameObject target_enemy;

    private void Start()
    {
        target_enemy = GameObject.Find("enemy");
    }
    private void Update()
    {
        if (Input.GetKeyDown(debug_key))
        {
            target_enemy.GetComponent<Life>().Damage(1);
            Debug.Log("add 1 damage!");
        }
    }
}
