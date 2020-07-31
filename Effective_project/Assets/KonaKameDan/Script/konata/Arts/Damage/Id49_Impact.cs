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

    [Header("拡散のスタック数に応じてたされる数")]
    [SerializeField] float radiusUp = 0.02f;

    GameObject impact;
    ObjSizChange impactObjSiz;

    float fade;

    ArtsStatus artsStatus;

    int spreadCount;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //エフェクトの所持数を代入
        spreadCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Spread);

        //効果範囲変更
        impactMaxSiz += (Vector3.one * (float)spreadCount) * 2;

        //インパクトオブジェクトのサイズ変更
        impact = Instantiate(impactObj, transform);
        impactObjSiz = Arts_Process.SetAddObjSizChange(
                    impact, Vector3.zero, impactMaxSiz,
                    impactSizUpSpeed,
                    ObjSizChange.SizChangeMode.ScaleUp);

        //インパクトシェーダーの初期化
        fade = Arts_Process.GetImpactShaderMaterialFade(material, 5);

        //敵を吹き飛ばす
        int layerMask = Arts_Process.GetVsLayerMask(artsStatus);
        Arts_Process.Impact(transform.position, radius, layerMask, explosionForce, uppersModifier);
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
