using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// アーツ作成メニューの中央に表示されるものの処理
/// </summary>
public class FusionCirclePreview : MonoBehaviour
{
    [SerializeField] Image unknown;
    [SerializeField] Image beforeIcon;
    [SerializeField] Image afterIcon;
    [SerializeField] TextMeshProUGUI text;

    ArtsIconImage beforeArtsIconImage;
    ArtsIconImage afterArtsIconImage;
    static Dictionary<string, bool> isCreateArts = new Dictionary<string, bool>();

    // Start is called before the first frame update
    void Start()
    {
        text.text = "";
        beforeArtsIconImage = beforeIcon.gameObject.GetComponent<ArtsIconImage>();
        afterArtsIconImage = afterIcon.gameObject.GetComponent<ArtsIconImage>();

        //初期化
        if (MainGameManager.GetArtsReset || isCreateArts == null)
        {
            isCreateArts.Clear();
            foreach (var item in afterArtsIconImage.data.GetTable())
            {
                isCreateArts.Add(item.Key, false);
            }
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
                afterIcon.sprite = afterArtsIconImage.data.GetTable()[id].image;
                OnAfterImage();
            }
            else
            {
                beforeIcon.sprite = beforeArtsIconImage.data.GetTable()[id].image;
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
