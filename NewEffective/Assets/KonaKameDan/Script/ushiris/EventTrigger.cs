using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] List<string> targetTag;
    [SerializeField] UnityEvent TriggerEvent;

    private void Start()
    {
        TriggerEvent.AddListener(() => Destroy(gameObject));
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var item in targetTag)
        {
            if (other.CompareTag(item)) TriggerEvent.Invoke();
        }
    }
}
