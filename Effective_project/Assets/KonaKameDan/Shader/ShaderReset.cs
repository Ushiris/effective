using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderReset : MonoBehaviour
{
    [SerializeField] Material searchShader;

    // Start is called before the first frame update
    void Start()
    {
        Arts_Process.SearchShaderReset(searchShader);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
