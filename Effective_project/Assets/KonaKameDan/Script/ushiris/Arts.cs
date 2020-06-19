using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arts : MonoBehaviour
{
    public GameObject player;
    public EffectObjectAcquisition backpack;
    public List<ArtsActionElements.ArtsAction> FireActions { get; set; }
    public List<ParticleSystem> particle;

    private void Start()
    {
        backpack = GameObject.FindWithTag("Player").GetComponent<EffectObjectAcquisition>();
    }

    public void Fire()
    {
        FireActions.ForEach((func) => { func(gameObject); });
    }
}
