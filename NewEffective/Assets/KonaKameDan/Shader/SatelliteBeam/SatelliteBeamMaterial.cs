using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アマテラスのマテリアル操作
/// </summary>
public class SatelliteBeamMaterial : MonoBehaviour
{
    public float coolTime = 5f;
    public bool isTimeStart; 

    [SerializeField] GameObject shaderObj;
    [SerializeField] GameObject coreMaterialObj;

    [SerializeField] Material shader;
    [SerializeField] Material coreMaterial;

    static readonly float shaderSpeed = 1;
    static readonly float coreMaterialSpeed = 1.5f;
    static readonly string kCoreMaterialColorName = "_BaseColor";
    static readonly string kCoreMaterialEnissionColorName = "_EmissiveColor";
    static readonly Color32 kBooleColorCode = new Color32(18, 0, 255, 0);
    static readonly Color32 kRedColorCode = new Color32(255, 44, 0, 0);

    float timer;
    float coreMaterialTimer = 1;
    float shaderTimer = 0;

    private void Start()
    {
        ArtsStatus artsStatus = GetComponentInParent<ArtsStatus>();

        //マテリアルの複製
        shader = shaderObj.GetComponent<Renderer>().material = new Material(shader);
        coreMaterial = coreMaterialObj.GetComponent<Renderer>().material = new Material(coreMaterial);

        //初期化
        shader.SetFloat("Vector1_5D2414AE", 0);
        var color = coreMaterial.color;
        color.a = coreMaterialTimer;
        coreMaterial.color = color;

        //Artsを出す者によって色を変える
        switch (artsStatus.type)
        {
            case ArtsStatus.ParticleType.Player:
                coreMaterial.SetColor(kCoreMaterialEnissionColorName, kBooleColorCode);
                break;
            case ArtsStatus.ParticleType.Enemy:
                coreMaterial.SetColor(kCoreMaterialEnissionColorName, kRedColorCode);
                break;
            case ArtsStatus.ParticleType.Unknown:break;
            default:break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimeStart)
        {
            if (timer < coolTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                Shader(shaderSpeed);
                CoreMaterial(coreMaterialSpeed);

                if (shaderTimer > 1 && coreMaterialTimer < 0)
                {
                    isTimeStart = false;
                }
            }
        }
    }

    void Shader(float speed)
    {
        shaderTimer += speed * Time.deltaTime;
        shader.SetFloat("Vector1_5D2414AE", shaderTimer);
    }

    void CoreMaterial(float speed)
    {
        coreMaterialTimer -= speed * Time.deltaTime;
        var color = coreMaterial.GetColor(kCoreMaterialColorName);
        color.a = coreMaterialTimer;
        color.a = Mathf.Clamp(color.a, 0, 100);
        coreMaterial.SetColor(kCoreMaterialColorName, color);
    }
}
