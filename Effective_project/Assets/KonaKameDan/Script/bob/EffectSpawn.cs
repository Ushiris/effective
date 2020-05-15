using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawn : MonoBehaviour
{
    private EffectOccurrence effectOccurrence;
    private Vector3 SpawnPos;
    public float SpawnRange = 5.0f;// effectスポーン範囲
    public float posY = 0.0f;// effectスポーン高さ
    public GameObject playerPos;// playerのポジション

    private void Start()
    {
        effectOccurrence = GetComponent<EffectOccurrence>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnPos = new Vector3(Random.Range(-SpawnRange, SpawnRange), posY, Random.Range(-SpawnRange, SpawnRange));
            SpawnPos = new Vector3(Random.Range(-SpawnRange + playerPos.transform.position.x, SpawnRange + playerPos.transform.position.x),
                                   posY + playerPos.transform.position.y,
                                   Random.Range(-SpawnRange + playerPos.transform.position.z, SpawnRange + playerPos.transform.position.z));
            effectOccurrence.EffectGenerate(SpawnPos);
        }
    }
}
