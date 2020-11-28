using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuCamera : MonoBehaviour
{
    [SerializeField] GameObject camera;
    [SerializeField] GameObject selectMenuIconGroup;
    [SerializeField] Transform pivot;
    [SerializeField] float speed = 3f;

    [SerializeField] TitleMenuSelectChange nextButton;
    [SerializeField] TitleMenuSelectChange backButton;

    public bool IsMoveEnd { get; private set; }

    int listNum = 0;
    List<GameObject> select = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        var s = selectMenuIconGroup.GetComponentsInChildren<TitleMenuSelectIcon>();
        foreach(var obj in s)
        {
            select.Add(obj.gameObject);
        }

        //クリックに反応するようにする
        nextButton.isPointerClick = () => Next();
        backButton.isPointerClick = () => Back();
    }

    // Update is called once per frame
    void Update()
    {
        //次に進むために配列番号を進める
        if (IsListNumPlusMove()) Next();
        else if (IsListNumMinusMove()) Back();

        //移動
        transform.LookAt(pivot);
        var target = select[listNum].transform.position;
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * speed);

        //移動が終了したかどうか
        var dis = Vector3.Distance(transform.position, target);
        IsMoveEnd = dis < 0.2f;
    }

    //次に進むために配列番号を進める
    void Next()
    {
        if (TitleMenuSelectIcon.IsSceneLoadProcess) return;
        if (listNum < select.Count - 1) listNum++;
        else listNum = 0;
    }

    //前に戻るために配列番号を戻す
    void Back()
    {
        if (TitleMenuSelectIcon.IsSceneLoadProcess) return;
        if (listNum > 0) listNum--;
        else listNum = select.Count - 1;
    }

    bool IsListNumPlusMove()
    {
        return Input.GetKeyDown(KeyCode.LeftArrow);
    }

    bool IsListNumMinusMove()
    {
        return Input.GetKeyDown(KeyCode.RightArrow);
    }
}
