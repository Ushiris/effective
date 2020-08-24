using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id027_AnnihilationRay : MonoBehaviour
{
    [SerializeField] GameObject annihilationRayParticleObj;
    GameObject annihilationRayParticle;

    [SerializeField] float speed = 5f;

    [SerializeField] Vector3 defaultRot = new Vector3(30, -60, 0);

    // Start is called before the first frame update
    void Start()
    {
        annihilationRayParticle = 
            Instantiate(annihilationRayParticleObj, transform);

        annihilationRayParticle.transform.localRotation = Quaternion.Euler(defaultRot);


        annihilationRayParticle.transform.localPosition = new Vector3(0, 5, 0);

        var rb= annihilationRayParticle.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        Arts_Process.SetParticleHitSetCollision(annihilationRayParticle);
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.localRotation = 
            Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(0, 100, 0), step);

        if (transform.localRotation.eulerAngles.y > 99f)
        {
            var ps = annihilationRayParticle.GetComponent<ParticleSystem>();
            var pse = ps.emission;
            pse.enabled = false;
            
        }
    }
}
