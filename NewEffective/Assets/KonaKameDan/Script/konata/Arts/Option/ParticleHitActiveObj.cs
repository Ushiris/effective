using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHitActiveObj : MonoBehaviour
{
    [SerializeField] GameObject[] obj;
    [SerializeField] float delay = 0.3f;
    int count = 0;

    int layerMask;

    private void Start()
    {
        layerMask = LayerMask.GetMask("Map");
    }

    void OnParticleCollision(GameObject other)
    {
        if (obj.Length == count)
        {
            return;
        }

        if (Physics.Raycast(transform.position, transform.forward, out var hit, 50f, layerMask))
        {
            obj[count].transform.position = hit.point;
            obj[count].transform.parent = null;
            Destroy(obj[count], 6f);
            count++;
            if (obj.Length == count) StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < obj.Length; i++)
        {
            yield return new WaitForSeconds(delay);
            obj[i].SetActive(true);
            Destroy(obj[i], 3f);
        }
        for (int i = 0; i < obj.Length; i++)
        {
            yield return new WaitForSeconds(delay);
            // SE
            SE_Manager.SePlay(SE_Manager.SE_NAME.Id047_PingPong_third);
        }
    }
}
