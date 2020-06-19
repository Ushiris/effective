using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSearch : MonoBehaviour
{
    [SerializeField] Vector3 fixPos;
    public GameObject obj;
    RectTransform rt;
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        pos = RectTransformUtility.WorldToScreenPoint(Camera.main, obj.transform.position);

        pos.x = Mathf.Clamp(pos.x, 0f, Screen.width);
        pos.y = Mathf.Clamp(pos.y, 0f, Screen.height);

        rt.position = pos;
    }
}
