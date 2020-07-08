using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Id459_Direction : MonoBehaviour
{
    [SerializeField] GameObject fairyParticleObj;
    [SerializeField] GameObject fairyTraceParticleObj;

    [Header("ステータス")]
    [SerializeField] Vector3 instantPos = new Vector3(0, 0.5f, 0);
    [SerializeField] Vector3 siz = new Vector3(3, 3, 3);
    [SerializeField] float speed = 10f;

    static GameObject id459_DirectionObj;

    NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        //すでに存在している場合前のオブジェクトを消す
        if (id459_DirectionObj != null) Destroy(id459_DirectionObj);
        id459_DirectionObj = gameObject;

        //妖精償還
        GameObject fairyParticle = Instantiate(fairyParticleObj, transform);
        fairyParticle.transform.localPosition = instantPos;
        fairyParticle.transform.localScale = siz;

        //妖精から出る粉を生成
        GameObject fairyTraceParticle = 
            Instantiate(fairyTraceParticleObj, fairyParticle.transform);

        //親子関係解除
        transform.parent = null;

        //ゴールまで行く
        navMeshAgent = Arts_Process.SetNavMeshAgent(gameObject);
        navMeshAgent.speed = speed;
        navMeshAgent.destination = GoalIn.GetGoalPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
