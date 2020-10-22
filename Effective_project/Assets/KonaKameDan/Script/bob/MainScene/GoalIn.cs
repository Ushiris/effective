using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoalIn : MonoBehaviour
{
    [SerializeField] GameObject StageSelectCanvas;
    GameObject stageSelectCanvas;

    public bool isLock = true;
    public static Vector3 GetGoalPos { get; private set; }

    static readonly string Player_tag = "Player";

    private void Start()
    {
        GetGoalPos = transform.position;

        //ステージ選択画面を生成
        stageSelectCanvas = Instantiate(StageSelectCanvas);
        stageSelectCanvas.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)// 何かに当たった瞬間
    {
        if (other.gameObject.tag == Player_tag && !isLock)// エフェクトオブジェクトの場合
        {
            UI_Manager.GetMainUiCanvas.enabled = false;
            stageSelectCanvas.SetActive(true);
        }
    }
}
