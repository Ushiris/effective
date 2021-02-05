using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一定時間後にPortalを開ける
/// </summary>
public class Portal : MonoBehaviour
{
    [SerializeField] GoalIn goalIn;
    [SerializeField] Texture portalDefaultTex;
    [SerializeField] Texture portalActiveTex;
    [SerializeField] Material portalMaterial;
    [SerializeField] ParticleSystem[] playerTriggerPlayParticle;
    [SerializeField] ParticleSystem[] openPortalEffect;
    [SerializeField] float gageTime = 5f;

    static bool isPortalOpen = false;

    static readonly string kPlayer_tag = "Player";
    static readonly float kDefaultPortalMaterialEmission = 1f;
    static readonly float kActivePortalMaterialEmission = 50f;

    private void Start()
    {
        isPortalOpen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsReturn(other.tag)) return;

        for (int i = 0; i < playerTriggerPlayParticle.Length; i++)
        {
            playerTriggerPlayParticle[i].Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsReturn(other.tag)) return;

        if (gageTime >= 0)
        {
            gageTime -= Time.deltaTime;
        }
        else if(isPortalOpen)
        {
            goalIn.isLock = false;

            //プレイヤーが近寄ると起きる演出を止めて、Portalに入れるようになった合図の演出を出す
            for (int i = 0; i < playerTriggerPlayParticle.Length; i++)
            {
                playerTriggerPlayParticle[i].Stop();
            }
            for (int i = 0; i < openPortalEffect.Length; i++)
            {
                openPortalEffect[i].Play();

                var s = openPortalEffect[i].GetComponent<PortalParticleControl>();
                if (s != null) s.IsStart = true;
            }

            //ポータルのエミッションの値を変更する
            portalMaterial.SetFloat("Vector1_377AAD1B", kActivePortalMaterialEmission);
            //ポータルに表示するTextureを入れる
            portalMaterial.SetTexture("Texture2D_9BBC0FDC", portalActiveTex);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsReturn(other.tag)) return;

        for (int i = 0; i < playerTriggerPlayParticle.Length; i++)
        {
            playerTriggerPlayParticle[i].Stop();
        }
    }

    private void OnDestroy()
    {
        portalMaterial.SetFloat("Vector1_377AAD1B", kDefaultPortalMaterialEmission);
        portalMaterial.SetTexture("Texture2D_9BBC0FDC", portalDefaultTex);
    }

    bool IsReturn(string tag)
    {
        return !goalIn.isLock || tag != kPlayer_tag;
    }

    public static void OnPortalOpen()
    {
        isPortalOpen = true;
    }
}
