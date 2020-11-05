using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シェーダーの初期化
/// </summary>
public class ShaderReset : MonoBehaviour
{
    [SerializeField] Material searchShader;
    [SerializeField] Material rippleShader;

    // Start is called before the first frame update
    void Start()
    {
        Arts_Process.SearchShaderReset(searchShader);
        Arts_Process.RippleShaderReset(rippleShader);
    }
}
