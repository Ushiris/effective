using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id127_Inferno : MonoBehaviour
{
    [SerializeField] GameObject infernoParticleObj;
    GameObject infernoParticle;

    // Start is called before the first frame update
    void Start()
    {
        //位置の初期設定
        Arts_Process.RollReset(gameObject);
        transform.localPosition += new Vector3(0, -0.45f, 0);

        //パーティクル生成
        infernoParticle = Instantiate(infernoParticleObj, transform);
        Arts_Process.SetParticleSetCollision(infernoParticle);
    }

    private void Update()
    {
        if (infernoParticle.transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
