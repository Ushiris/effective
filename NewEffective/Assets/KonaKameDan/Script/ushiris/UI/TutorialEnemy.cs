using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : MonoBehaviour
{
    [SerializeField] TutorialEvent eventer;
    Life life;

    private void Start()
    {
        life = GetComponent<Life>();
        life.AddLastword(() => eventer.InvokeEvent(TutorialEvent.EndSection.battle));
    }
}
