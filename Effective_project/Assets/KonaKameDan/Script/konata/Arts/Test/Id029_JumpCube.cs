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

    Vector3 rot;
    CollisionHitPos collisionHitPos;
    GameObject jumpCube;
    GameObject parent;
    List<GameObject> miracles = new List<GameObject>();

    bool isStart;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < miracleCount; i++)
        {
            miracles.Add(Instantiate(miracleObj, transform));
        }

        //ランダムに回転の速さを変える
        rot = new Vector3(Random.Range(0, 2f), Random.Range(0, 2f), Random.Range(0, 2f));

        //親オブジェクト取得
        parent = transform.root.gameObject;

        //軌跡を生成
        miracles =
            Arts_Process.Miracle(miracles, miracleSpace, v0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isStart = true;
        }
        else if (Input.GetMouseButtonUp(0) && isStart)
        {
            transform.parent = null;

            //JumpCube生成
            jumpCube = Instantiate(jumpCubeObj, transform);

            //jumpCubeを投げる
            Rigidbody jumpCubeRb = jumpCube.GetComponent<Rigidbody>();
            jumpCubeRb.AddForce(v0, ForceMode.VelocityChange);

            //CollisionHitPosの取得
            collisionHitPos = jumpCube.GetComponent<CollisionHitPos>();
        }

        if (jumpCube != null)
        {
            //empCubeの回転
            var move = rot * cubeRotationSpeed * Time.deltaTime;
            jumpCube.transform.Rotate(move, Space.World);

            //親を移動させる
            if (collisionHitPos != null)
            {
                var pos = collisionHitPos.hitPos.Value;
                parent.transform.position = pos;

                Destroy(gameObject);
            }
        }
    }
}
