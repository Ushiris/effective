using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magma : MonoBehaviour
{
    [SerializeField] int damage = 100;
    [SerializeField] float lapTime = 2;
    Life playerLife;
    bool isHitDamage;

    private void Start()
    {
        playerLife = NewMap.GetPlayerObj.GetComponent<Life>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        isHitDamage = true;
        StartCoroutine(Damage(lapTime));
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        isHitDamage = false;
    }

    IEnumerator Damage(float lapTime)
    {
        while (isHitDamage && playerLife != null)
        {
            playerLife.Damage(damage);
            yield return new WaitForSeconds(lapTime);
            //yield return new WaitForEndOfFrame();
        }
    }
}
