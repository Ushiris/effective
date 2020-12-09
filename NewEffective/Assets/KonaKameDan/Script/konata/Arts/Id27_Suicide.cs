using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id27_Suicide : MonoBehaviour
{
    [Header("現在HPの割合ダメージ")]
    [SerializeField] float selfHarmRate = 0.5f;

    ArtsStatus artsStatus;

    // Start is called before the first frame update
    void Start()
    {
        artsStatus = GetComponent<ArtsStatus>();

        //自傷ダメージ
        var life = artsStatus.myObj.GetComponent<Life>();
        var selfHarm = life.HP * selfHarmRate;
        life.Damage((int)selfHarm);
        Destroy(gameObject, 2f);
        //SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Id27_Suicide_first);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Arts_Process.GetEnemyTag(artsStatus))
        {
            //ダメージ処理
            Debug.Log(other.gameObject.name + "に???ダメージ");
        }
    }
}
