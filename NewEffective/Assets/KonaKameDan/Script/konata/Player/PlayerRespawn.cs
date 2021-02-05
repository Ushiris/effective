using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] float speed = 30f;
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] ParticleSystem spawnParticle;
    [SerializeField] GameObject model;
    [SerializeField] TpsPlayerControl tpsPlayerControl;

    bool isMove = false;
    float timer;

    PlayerArtsInstant artsInstant;
    PlayerMove playerMove;
    Rigidbody rb;
    new SphereCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<SphereCollider>();
        playerMove = GetComponent<PlayerMove>();
        artsInstant = GetComponent<PlayerArtsInstant>();
        SE_Manager.SePlay(SE_Manager.SE_NAME.PlayerSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMove) return;
        
        var target = NewMap.GetPlayerRespawnPos;
        timer += Time.deltaTime * Time.deltaTime;

        transform.position =
            Vector3.MoveTowards(transform.position, target, speed * timer);

        var dis = Vector3.Distance(transform.position, target);
        if (dis < 0.1f)
        {
            SE_Manager.SePlay(SE_Manager.SE_NAME.PlayerSpawn);
            spawnParticle.Play();
            OnLock(false);
        }
    }

    public void OnLock(bool isLock)
    {
        var isEnable = !isLock;

        if (isLock)
        {
            SE_Manager.SePlay(SE_Manager.SE_NAME.PlayerFall);
            deathParticle.Play();
        }

        model.SetActive(isEnable);
        collider.enabled = isEnable;
        playerMove.enabled = isEnable;
        tpsPlayerControl.enabled = isEnable;
        artsInstant.isArtsInstantLock = !isEnable;
        isMove = !isEnable;
        rb.isKinematic = !isEnable;

        rb.velocity = Vector3.zero;
    }
}
