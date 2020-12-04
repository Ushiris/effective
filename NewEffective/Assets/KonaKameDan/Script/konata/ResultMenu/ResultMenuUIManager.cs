using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultMenuUIManager : MonoBehaviour
{
    [SerializeField] string[] titleNameArr;
    [SerializeField] float radius = 10;
    [SerializeField] GameObject scoreObj;

    List<ResultMenuUI> resultMenuUIs = new List<ResultMenuUI>();
    static readonly float kAngle = 360;

    int scoreRollPlayNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < titleNameArr.Length; i++)
        {
            float r = (kAngle / titleNameArr.Length) * i;
            r *= Mathf.Deg2Rad;

            var pos = new Vector3(radius * Mathf.Cos(r), 0f, radius * Mathf.Sin(r));
            var obj = Instantiate(scoreObj, pos, Quaternion.identity);
            resultMenuUIs.Add(obj.GetComponent<ResultMenuUI>());
        }

        resultMenuUIs[scoreRollPlayNum].IsPlay = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (resultMenuUIs[scoreRollPlayNum].IsPlay)
        {
            
        }
        else if (titleNameArr.Length - 1 != scoreRollPlayNum)
        {
            scoreRollPlayNum++;
            resultMenuUIs[scoreRollPlayNum].IsPlay = true;
        }

        Debug.Log("aaaa" + scoreRollPlayNum);
    }
}
