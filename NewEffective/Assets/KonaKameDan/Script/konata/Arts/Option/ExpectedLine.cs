using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpectedLine : MonoBehaviour
{
    [SerializeField] GameObject expectedLineObj;
    [SerializeField] Vector3 fixPos;

    string tmpId;

    static readonly Vector3 id079_Amaterasu = new Vector3(0, 0, 5);
    static readonly Vector3 id049_ArrowRain = new Vector3(0, 0, 12);
    static readonly Vector3 id479_MeteorRain = new Vector3(5, 0, 13);

    // Start is called before the first frame update
    void Start()
    {
        expectedLineObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var arts = MyArtsDeck.GetSelectArtsDeck;
        if (arts.id != tmpId)
        {
            tmpId = arts.id;

            if(tmpId== "079" || tmpId == "049" || tmpId == "479")
            {
                expectedLineObj.SetActive(true);
            }
            else
            {
                expectedLineObj.SetActive(false);
            }
        }

        switch (tmpId)
        {
            case "079": OnLine(id079_Amaterasu); break;
            case "049": OnLine(id049_ArrowRain); break;
            case "479": OnLine(id479_MeteorRain); break;
            default: break;
        }
    }

    void OnLine(Vector3 pos)
    {
        expectedLineObj.transform.localPosition = pos + fixPos;
        var y = NewMap.GetGroundPosMatch(expectedLineObj.transform.position) + 0.5f;
        var fix = expectedLineObj.transform.position;
        fix.y = y;
        expectedLineObj.transform.position = fix;
    }
}
