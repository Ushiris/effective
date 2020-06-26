using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSearch : MonoBehaviour
{
    [SerializeField] Vector3 fixPos;
    public GameObject obj;
    RectTransform rt;
    public Vector3 pos;

    float maxX, minX;
    float maxY, minY;

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();

        maxX = Screen.width;
        minX = 0;
        maxY = Screen.height;
        minY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 objPos = obj.transform.position;
        pos = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector3(objPos.x, objPos.z, objPos.y));

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        if (pos.x == 0)
        {
            rt.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
        }


        rt.position = pos;
    }
}
