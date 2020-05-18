using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectOccurrence : MonoBehaviour
{
    [SerializeField] private int numberOfEffect; // 出現しているeffectの数
    [SerializeField] private GameObject[] effect; // effectの入れ物

    private void Start()
    {
        numberOfEffect = 0;
    }

    public void EffectGenerate(Vector3 pos)
    {
        int randomValue = Random.Range(0, effect.Length);//　出現させるeffectをランダムに選ぶ

        GameObject.Instantiate(effect[randomValue], pos, Quaternion.Euler(0.0f, 0.0f, 0.0f));// effectの生成

        numberOfEffect++;
    }
}
