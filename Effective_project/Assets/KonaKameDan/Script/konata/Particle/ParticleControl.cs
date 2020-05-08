using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// テスト用
/// </summary>
public class ParticleControl : MonoBehaviour
{
    [SerializeField] GameObject particleObj;
    ParticleSystem.EmissionModule em;
    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OnShotStartTrigger())
        {
            obj= Instantiate(particleObj, transform);
            obj.GetComponent<HomingParticle>().target = MouseTarget.ms;
            em = obj.GetComponent<ParticleSystem>().emission;
            em.enabled = true;        
        }

        if (obj != null)
        {
            if (!OnShotTrigger())
            {
                em.enabled = false;
            }
        }
    }

    bool OnShotStartTrigger()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }

    bool OnShotTrigger()
    {
        return Input.GetKey(KeyCode.Mouse0);
    }
}
