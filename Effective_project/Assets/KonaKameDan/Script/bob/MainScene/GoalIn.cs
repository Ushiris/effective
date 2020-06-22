using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalIn : MonoBehaviour
{
    private bool returnResultScene;
    public string Player_tag = "Player";

    private void Start()
    {
        returnResultScene = false;
    }
    private void Update()
    {
        if (returnResultScene)
            SceneManager.LoadScene("Result");
    }
    private void OnCollisionEnter(Collision other)// 何かに当たった瞬間
    {
        if (other.gameObject.tag == Player_tag)// エフェクトオブジェクトの場合
        {
            returnResultScene = true;
        }
    }
}
