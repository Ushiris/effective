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

    readonly float shaderSpeed = 1;
    readonly float coreMaterialSpeed = 1.5f;

    float timer;
    float coreMaterialTimer = 1;
    float shaderTimer = 0;

    private void Start()
    {
        //マテリアルの複製
        shader = shaderObj.GetComponent<Renderer>().material = new Material(shader);
        coreMaterial = coreMaterialObj.GetComponent<Renderer>().material= new Material(coreMaterial);

        //初期化
        shader.SetFloat("Vector1_5D2414AE", 0);
        var color = coreMaterial.color;
        color.a = coreMaterialTimer;
        coreMaterial.color = color;
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
        var color = coreMaterial.GetColor("_BaseColor");
        color.a = coreMaterialTimer;
        color.a = Mathf.Clamp(color.a, 0, 100);
        coreMaterial.SetColor("_BaseColor", color);
    }
}
