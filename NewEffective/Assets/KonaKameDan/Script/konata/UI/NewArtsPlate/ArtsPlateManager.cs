using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// アーツプレートを制御する
/// </summary>
public class ArtsPlateManager : MonoBehaviour
{
    [SerializeField] ArtsPlateFrame[] frameArr;
    [SerializeField] ArtsPlateEffectIcon[] effectIconArr;
    [SerializeField] ArtsIconImage artsIcon;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] float waitTime = 0.3f;
    [SerializeField] float speed = 30f;

    int effectIconMoveNum;
    string SetId;

    // Start is called before the first frame update
    void Start()
    {
        OnReset();
    }

    // Update is called once per frame
    void Update()
    {
        if (effectIconMoveNum == effectIconArr.Length - 1) return;

        for (int i = 0; i < effectIconArr.Length; i++)
        {
            if (!effectIconArr[i].GetIsCheckMove)
            {
                effectIconMoveNum++;
            }

            effectIconArr[i].updata();
        }
    }

    /// <summary>
    /// アーツプレートの表示を変更する
    /// </summary>
    /// <param name="id"></param>
    public void OnArtsPlateChange(string id)
    {
        SetId = id;
        effectIconMoveNum = 0;
        OnReset();
        Process();
    }

    void Process()
    {
        //アイコン
        StartCoroutine(OnEffectIcon(0, SetId, waitTime * 0));
        StartCoroutine(OnEffectIcon(1, SetId, waitTime * 1));
        StartCoroutine(OnEffectIcon(2, SetId, waitTime * 2));

        //フレーム
        StartCoroutine(OnFrame(0, SetId, waitTime * 0.5f));
        StartCoroutine(OnFrame(1, SetId, waitTime * 1.5f));
        StartCoroutine(OnFrame(2, SetId, waitTime * 2.5f));

        //プレビュー
        StartCoroutine(OnText(SetId, waitTime * 3.5f));
        StartCoroutine(OnArtsIcon(SetId, waitTime * 3.5f));


        IEnumerator OnEffectIcon(int num, string id, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            if (num < effectIconArr.Length && id.Length > num)
            {
                effectIconArr[num].ImageChange(id[num].ToString());
                effectIconArr[num].ChangeMoveSiz(speed);
            }
            else
            {
                effectIconArr[num].ImageChange("");
            }
        }

        IEnumerator OnFrame(int num, string id, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            if (num < frameArr.Length && id.Length > num)
            {
                frameArr[num].ChangeImage(id[num].ToString());
            }
            else
            {
                frameArr[num].ChangeImage("");
            }
        }

        IEnumerator OnText(string id, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            text.text = ArtsList.GetLookedForArts(SetId).name;
        }

        IEnumerator OnArtsIcon(string id, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            artsIcon.ChangeImage(id);
        }
    }

    void OnReset()
    {
        for(int i=0;i< effectIconArr.Length; i++)
        {
            effectIconArr[i].ImageChange("");
            frameArr[i].ChangeImage("");
            text.text = "";
            artsIcon.ChangeImage("");
        }
    }
}
