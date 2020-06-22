using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtsInstantManager : MonoBehaviour
{

    [SerializeField] PrefabDictionary prefabs;
    [SerializeField] string debugNum = "045";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (OnTrigger())
        {
            //アーツが出る場所
            Transform artsPivot = PlayerManager.GetManager.GetPlObj.transform.GetChild(3);

            switch (SelectArts())
            {
                case "045":Instantiate(prefabs.GetTable()["045"], artsPivot); break;
                case "04": Instantiate(prefabs.GetTable()["04"], artsPivot); break;
                case "05": Instantiate(prefabs.GetTable()["05"], artsPivot); break;
                default: break;
            }
        }
    }

    //アーツを放つキー
    bool OnTrigger()
    {
        return Input.GetMouseButtonDown(0);
    }

    //デバッグと切り替える処理
    string SelectArts()
    {
        if (debugNum == null)
        {
            return ArtsList.GetSelectArts.id;
        }
        else return debugNum;
    }


    [System.Serializable]
    public class PrefabDictionary : Serialize.TableBase<string, GameObject, Name2Prefab> { }

    [System.Serializable]
    public class Name2Prefab : Serialize.KeyAndValue<string, GameObject>
    {
        public Name2Prefab(string key, GameObject value) : base(key, value) { }
    }
}
