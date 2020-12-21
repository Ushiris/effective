using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FusionCirclePreview : MonoBehaviour
{
    [SerializeField] Image unknown;
    [SerializeField] Image beforeIcon;
    [SerializeField] Image afterIcon;
    [SerializeField] TextMeshProUGUI text;

    ArtsIconImage beforeArtsIconImage;
    ArtsIconImage afterArtsIconImage;
    Dictionary<string, bool> isCreateArts = new Dictionary<string, bool>();

    // Start is called before the first frame update
    void Start()
    {
        text.text = "";
        beforeArtsIconImage = beforeIcon.gameObject.GetComponent<ArtsIconImage>();
        afterArtsIconImage = afterIcon.gameObject.GetComponent<ArtsIconImage>();

        foreach (var item in afterArtsIconImage.data.GetTable())
        {
            isCreateArts.Add(item.Key, false);
        }
    }

    /// <summary>
    /// イメージを変える
    /// </summary>
    /// <param name="id"></param>
    public void ImageChange(string id)
    {
        if (id.Length <= 1)
        {
            OnUnknownImage();
            text.text = "";
        }
        else
        {
            //画像の変更
            if (isCreateArts[id])
            {
                afterIcon = afterArtsIconImage.data.GetTable()[id].image;
                OnAfterImage();
            }
            else
            {
                beforeIcon = beforeArtsIconImage.data.GetTable()[id].image;
                OnBeforeImage();
            }

            //テキストの変更
            text.text = ArtsList.GetLookedForArts(id).name;
        }
    }

    /// <summary>
    /// 作成したアーツを登録する
    /// </summary>
    /// <param name="id"></param>
    public void OnCreateArtsCheck(string id)
    {
        if (id.Length > 1)
        {
            isCreateArts[id] = true;
        }
    }

    void OnUnknownImage()
    {
        unknown.enabled = true;
        beforeIcon.enabled = false;
        afterIcon.enabled = false;
    }

    void OnAfterImage()
    {
        unknown.enabled = false;
        beforeIcon.enabled = false;
        afterIcon.enabled = true;
    }

    void OnBeforeImage()
    {
        unknown.enabled = false;
        afterIcon.enabled = false;
        beforeIcon.enabled = true;
    }
}
