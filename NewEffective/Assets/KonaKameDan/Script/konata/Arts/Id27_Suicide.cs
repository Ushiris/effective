using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id27_Suicide : MonoBehaviour
{
    [SerializeField] GameObject explosionParticleObj;

    [Header("現在HPの割合ダメージ")]
    [SerializeField] float selfHarmRate = 0.5f;

    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //自傷ダメージ
        var life = artsStatus.myObj.GetComponent<Life>();
        var selfHarm = life.HP * selfHarmRate;
        life.Damage((int)selfHarm);

        Instantiate(explosionParticleObj, transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0) Destroy(gameObject);
    }
}
