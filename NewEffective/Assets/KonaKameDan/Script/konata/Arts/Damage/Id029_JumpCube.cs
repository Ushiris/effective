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
    LayerMask magmaMask;

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
        magmaMask = LayerMask.GetMask("ExceptionMap");
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

            //SE
            Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id029_JumpCube_first, transform.position, artsStatus);
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
                var pos = jumpCube.transform.position;
                pos.y = NewMap.GetGroundPosMatch(transform.position) + 2f;
                if (pos.y > -50)
                {
                    artsStatus.myObj.transform.position = pos;
                    var rb = artsStatus.myObj.GetComponent<Rigidbody>();
                    if (rb !=null)
                    {
                        rb.velocity = Vector3.zero;
                    }
                }


                //SE
                Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id025_PrimitiveShield_third, transform.position, artsStatus);

                Destroy(gameObject);
            }

            enemies= Physics.OverlapSphere(jumpCube.transform.position, 0.3f, magmaMask);

            foreach (Collider hit in enemies)
            {
                parent.transform.position = jumpCube.transform.position;

                //SE
                Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id025_PrimitiveShield_third, transform.position, artsStatus);

                Destroy(gameObject);
            }

            if (jumpCube.transform.position.y < -50)
            {
                Destroy(gameObject);
            }
        }
    }
}
