using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultMenuUI : MonoBehaviour
{
    [SerializeField] TextMesh titleText;
    [SerializeField] TextMesh scoreText;

    public int SetScore;
    public float ChangeScoreSpeed = 10f;
    public bool IsPlay { get; set; }

    int tmpNum;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = tmpNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsPlay) return;
        if (tmpNum == SetScore)
        {
            tmpNum += (int)(ChangeScoreSpeed * Time.deltaTime);
            tmpNum = Mathf.Clamp(tmpNum, 0, SetScore);
            scoreText.text = tmpNum.ToString();
        }
        else
        {
            IsPlay = false ;
        }
    }
}
