using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultMenuUI : MonoBehaviour
{
    [SerializeField] TextMesh titleText;
    [SerializeField] TextMesh scoreText;

    public int SetScore;
    public float ChangeScoreSpeed = 1000f;
    public bool IsPlay { get; set; }

    float tmpNum;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = tmpNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsPlay) return;
        if (tmpNum != SetScore)
        {
            time += Time.deltaTime;
            tmpNum = (time * time) * ChangeScoreSpeed;
            tmpNum = Mathf.Clamp(tmpNum, 0, SetScore);

            int strNum = (int)tmpNum;

            scoreText.text = strNum.ToString();
        }
        else
        {
            IsPlay = false ;
        }
    }
}
