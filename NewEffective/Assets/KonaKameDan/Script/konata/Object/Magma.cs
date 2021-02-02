using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magma : MonoBehaviour
{
    [SerializeField] int damage = 100;
    [SerializeField, Header("時間からlapTimeを割った値")] int lapTime = 2;
    Life playerLife;
    StopWatch FireTimer;
    bool IsHitDamage = false;

    private void Start()
    {
        FireTimer = StopWatch.Summon(0.5f, () => IsHitDamage = false, gameObject);
        FireTimer.SetActive(false);
    }

    private void Update()
    {
        if (!IsHitDamage) return;

        playerLife.Damage(damage/2);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (playerLife == null)
        {
            playerLife = collision.gameObject.GetComponent<Life>();
        }
        FireTimer.SetActive(false);
        FireTimer.ResetTimer();
        IsHitDamage = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        FireTimer.SetActive(false);
        FireTimer.ResetTimer();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        FireTimer.SetActive(true);
    }
}
