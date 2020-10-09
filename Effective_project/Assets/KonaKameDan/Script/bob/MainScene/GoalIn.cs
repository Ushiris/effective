using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalIn : MonoBehaviour
{
    bool returnResultScene;
    static readonly string Player_tag = "Player";

    public bool isLock = true;
    public static Vector3 GetGoalPos { get; private set; }

    private void Start()
    {
        GetGoalPos = transform.position;
        returnResultScene = false;
    }
    private void Update()
    {
        if (returnResultScene)
        {
            if (MainGameManager.GetArtsReset) MainGameManager.GetArtsReset = false;
            SceneManager.LoadScene("Result");
        }
    }
    private void OnCollisionEnter(Collision other)// 何かに当たった瞬間
    {
        if (other.gameObject.tag == Player_tag && !isLock)// エフェクトオブジェクトの場合
        {
            returnResultScene = true;
        }
    }
}
