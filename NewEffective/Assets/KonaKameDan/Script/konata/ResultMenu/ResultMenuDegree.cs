using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultMenuDegree : MonoBehaviour
{
    [SerializeField] TextMesh text;
    [SerializeField] new ParticleSystem particleSystem;
    [SerializeField] float waitTime = 0.3f;

    public bool isMove { get; private set; }

    static readonly float kStartWaitTime = 1f;

    public IEnumerator OnMove(string degree)
    {
        isMove = true;
        yield return new WaitForSeconds(kStartWaitTime);
        yield return StartCoroutine(Move(degree));
        isMove = false;
    }

    IEnumerator Move(string degree)
    {
        particleSystem.Play();
        text.text = degree;
        yield return new WaitForSeconds(waitTime);
    }
}
