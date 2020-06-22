using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id45_Search : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] float speed = 30;
    float dis;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = PlayerManager.GetManager.GetPlObj.transform.position;
        Arts_Process.SearchPosSet(material, pos);
        Arts_Process.SearchShaderReset(material);
    }

    // Update is called once per frame
    void Update()
    {
        dis += speed * Time.deltaTime;
        Arts_Process.SearchShaderStart(material, dis);

        if (Input.GetMouseButtonDown(0)) Destroy(gameObject);
    }
}
