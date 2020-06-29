using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSearch : MonoBehaviour
{
    [SerializeField] float fixPos;

    public GameObject obj;
    public float deleteTime;

    RectTransform rt;
    Image img;
    Vector3 pos;

    Vector3 rot;
    float maxX, minX;
    float maxY, minY;

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        img = GetComponent<Image>();

        maxX = Screen.width - fixPos;
        minX = 0 + fixPos;
        maxY = Screen.height - fixPos;
        minY = 0 + fixPos;

        Destroy(gameObject, deleteTime);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 objPos = obj.transform.position;
        pos = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector3(objPos.x, objPos.z, objPos.y));

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        if (pos.x == minX)
        {
            rot = new Vector3(0, 0, -90);
        }
        else if (pos.x == maxX)
        {
            rot = new Vector3(0, 0, 90);
        }
        else if (pos.y == minY)
        {
            rot = new Vector3(0, 0, 0);
        }
        else if (pos.y == maxY)
        {
            rot = new Vector3(0, 0, 180);
        }

        if (pos.x == minX || pos.x == maxX ||
            pos.y == minY || pos.y == maxY)
        {
            img.enabled = true;
            rt.rotation = Quaternion.Euler(rot);
            rt.position = pos;
        }
        else
        {
            img.enabled = false;
        }
    }
}
