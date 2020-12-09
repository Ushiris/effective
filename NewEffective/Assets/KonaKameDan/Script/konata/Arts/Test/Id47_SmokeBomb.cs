using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id47_SmokeBomb : MonoBehaviour
{
    [SerializeField] GameObject smokeBombParticle;
    GameObject smokeBombObj;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        smokeBombObj = Instantiate(smokeBombParticle, transform);
        //SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Id257_Haiyoru_second);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.childCount == 0) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Enemy") return;

        var enemy = other.gameObject.GetComponent<Enemy>();
        enemy.Stan(4f);
    }
}
