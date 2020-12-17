using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FusionCirclePreview : MonoBehaviour
{
    [SerializeField] Image beforeIcon;
    [SerializeField] Image afterIcon;
    [SerializeField] TextMeshProUGUI text;

    ArtsIconImage beforeArtsIconImage;
    ArtsIconImage afterArtsIconImage;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "";
        beforeArtsIconImage = beforeIcon.gameObject.GetComponent<ArtsIconImage>();
        afterArtsIconImage = afterIcon.gameObject.GetComponent<ArtsIconImage>();
    }

    /// <summary>
    /// イメージを変える
    /// </summary>
    /// <param name="id"></param>
    public void ImageChange(string id)
    {
        if (id.Length <= 1)
        {
            text.text = "";
        }
        else
        {
            //beforeIcon = beforeArtsIconImage.data.GetTable()[id].image;
            //afterIcon= afterArtsIconImage.data.GetTable()[id].image;
            text.text = ArtsList.GetLookedForArts(id).name;
        }
    }

    public void OnAfterImage()
    {
        beforeIcon.enabled = false;
        afterIcon.enabled = true;
    }

    public void OnBeforeImage()
    {
        afterIcon.enabled = false;
        beforeIcon.enabled = true;
    }
}
