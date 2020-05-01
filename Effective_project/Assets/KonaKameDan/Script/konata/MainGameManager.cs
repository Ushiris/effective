using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 今所持しているエフェクトリストを取得できるぞ！
    /// </summary>
    public static List<EffectObjectAcquisition.EffectObjectClass> GetPlEffectList
    {
        get { return new List<EffectObjectAcquisition.EffectObjectClass>(EffectObjectAcquisition.GetEffectObjAcquisition.effectObjectAcquisition); }
    }
}
