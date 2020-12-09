using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id027_AnnihilationRay : MonoBehaviour
{
    [SerializeField] ParticleSystem annihilationRayParticleObj;
    [SerializeField] Transform pivot;

    [SerializeField] float speed = 5f;
    float timer;
    Quaternion roll;

    bool isPlaySe;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
        roll= pivot.transform.localRotation;

        isPlaySe = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 1f)
        {
            timer += Time.deltaTime;
            return;
        }
        if(isPlaySe)
        {
            // SE
            SE_Manager.SePlay(SE_Manager.SE_NAME.Id027_annihilationRay_first);
            isPlaySe = false;
        }
        //回転
        float step = speed * Time.deltaTime;
        pivot.transform.localRotation =
            Quaternion.RotateTowards(pivot.transform.localRotation, Quaternion.Euler(0, 100, 0), step);
    }
}
