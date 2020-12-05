using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id029_JumpCube : MonoBehaviour
{
    [SerializeField] GameObject miracleObj;
    [SerializeField] GameObject jumpCubeObj;
    [SerializeField] Vector3 v0 = new Vector3(0, 5, 7);
    [SerializeField] float cubeRotationSpeed = 100f;
    [SerializeField] float miracleCount = 10;
    [SerializeField] float miracleSpace = 0.1f;

    [Header("飛翔のスタック数に応じてたされる数")]
    [SerializeField]
    float flyingDistance = 0.5f;

    Vector3 rot;
    GameObject jumpCube;
    GameObject parent;
    List<GameObject> miracles = new List<GameObject>();

    LayerMask layerMask;

    bool isStart = true;

    ArtsStatus artsStatus;

    int flyCount;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        for (int i = 0; i < miracleCount; i++)
        {
            miracles.Add(Instantiate(miracleObj, transform));
        }

        //エフェクトの所持数を代入
        flyCount = Arts_Process.GetEffectCount(artsStatus, NameDefinition.EffectName.Fly);

        //飛距離変更
        v0.z += flyingDistance * (float)flyCount;


        //ランダムに回転の速さを変える
        rot = new Vector3(Random.Range(0, 2f), Random.Range(0, 2f), Random.Range(0, 2f));

        //親オブジェクト取得
        parent = transform.root.gameObject;

        //軌跡を生成
        miracles =
            Arts_Process.Trajectory(miracles, miracleSpace, v0);

        transform.localPosition = new Vector3(0, 1f, 0);
        layerMask = LayerMask.GetMask("Map");

        //SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Id029_JumpCube_first);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && isStart)
        {
            transform.parent = null;

            //JumpCube生成
            jumpCube = Instantiate(jumpCubeObj, transform);

            //jumpCubeを投げる
            Rigidbody jumpCubeRb = jumpCube.GetComponent<Rigidbody>();
            jumpCubeRb.AddRelativeFor​​ce(v0, ForceMode.VelocityChange);

            isStart = false;
        }

        if (jumpCube != null)
        {
            //jumpCubeの回転
            var move = rot * cubeRotationSpeed * Time.deltaTime;
            jumpCube.transform.Rotate(move, Space.World);

            //マップにjumpCubeが当たった場合親を移動させる
            Collider[] enemies;
            enemies = Physics.OverlapSphere(jumpCube.transform.position, 0.3f, layerMask);
            foreach (Collider hit in enemies)
            {
                parent.transform.position = jumpCube.transform.position;
                Destroy(gameObject);

                //SE
                SE_Manager.SePlay(SE_Manager.SE_NAME.Id029_JumpCube_third);
            }
        }
    }
}
