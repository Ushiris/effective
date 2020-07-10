using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id49_Impact : MonoBehaviour
{
    [Header("敵を吹き飛ばすやつ")]
    [SerializeField] float radius = 10f;
    [SerializeField] float explosionForce = 300f;
    [SerializeField] float uppersModifier = 10f;

    [Header("演出の方")]
    [SerializeField] GameObject impactObj;
    [SerializeField] Material material;
    [SerializeField] Vector3 impactMaxSiz = new Vector3(20, 20, 20);
    [SerializeField] float impactSizUpSpeed = 3f;
    [SerializeField] float fadeSpeed = 10f;

    GameObject impact;
    ObjSizChange impactObjSiz;

    float fade;

    // Start is called before the first frame update
    void Start()
    {
        //インパクトオブジェクトのサイズ変更
        impact = Instantiate(impactObj, transform);
        impactObjSiz = Arts_Process.SetAddObjSizChange(
                    impact,
                    Vector3.zero, impactMaxSiz, Vector3.one,
                    impactSizUpSpeed,
                    ObjSizChange.SizChangeMode.ScaleUp);

        //インパクトシェーダーの初期化
        fade = Arts_Process.GetImpactShaderMaterialFade(material, 5);

        //敵を吹き飛ばす
        Arts_Process.Impact(transform.position, radius, "Enemy", explosionForce, uppersModifier);
    }

    // Update is called once per frame
    void Update()
    {
        //インパクトシェーダーの透明度を上げる
        fade -= fadeSpeed * Time.deltaTime;
        if (fade > 0f) Arts_Process.GetImpactShaderMaterialFade(material, fade);
        if (impactObjSiz.GetSizFlag)
        {
            Destroy(gameObject);
        }
    }
}
