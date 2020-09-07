using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id027_AnnihilationRay : MonoBehaviour
{
    [SerializeField] GameObject annihilationRayParticleObj;
    GameObject annihilationRayParticle;

    [SerializeField] float speed = 5f;

    [SerializeField] Vector3 defaultRot = new Vector3(30, -60, 0);

    ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        //パーティクル生成
        annihilationRayParticle = 
            Instantiate(annihilationRayParticleObj, transform);
        annihilationRayParticle.transform.localRotation = Quaternion.Euler(defaultRot);
        annihilationRayParticle.transform.localPosition = new Vector3(0, 5, 0);
        ps = annihilationRayParticle.GetComponent<ParticleSystem>();

        //Rigidbodyのセット
        var rb = annihilationRayParticle.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;

        //当たり判定生成
        Arts_Process.SetParticleHitSetCollision(annihilationRayParticle);
    }

    // Update is called once per frame
    void Update()
    {
        //回転
        float step = speed * Time.deltaTime;
        transform.localRotation = 
            Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(0, 100, 0), step);

        //削除
        var pse = ps.emission;
        if (annihilationRayParticle != null && pse.enabled)
        {
            if (transform.localRotation.eulerAngles.y > 99f)
            {
                pse.enabled = false;
            }
        }

        //本体を消す
        if (transform.childCount == 0) Destroy(gameObject);
    }
}
