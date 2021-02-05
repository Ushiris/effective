using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アーツの生成を管理
/// </summary>
public class ArtsInstantManager : MonoBehaviour
{
    [System.Serializable]
    public class ArtsStatusData
    {
        public GameObject prefab;
        public float coolTime;
        public float coolTimeDown;
        public List<NameDefinition.EffectName> effect;
    }

    public PrefabDictionary prefabs;

    static ArtsInstantManager GetArtsInstantManager;

    // Start is called before the first frame update
    void Start()
    {
        GetArtsInstantManager = this;

        foreach(var id in prefabs.GetTable().Keys)
        {
            if (prefabs.GetTable()[id].prefab.GetComponent<ArtsStatus>() == null)
            {
                prefabs.GetTable()[id].prefab.AddComponent<ArtsStatus>();
            }
            //if(prefabs.GetTable()[id].prefab.GetComponent<SphereCollider>() == null)
            //{
            //    var c=prefabs.GetTable()[id].prefab.AddComponent<SphereCollider>();
            //    c.isTrigger = true;
            //}
            prefabs.GetTable()[id].prefab.tag = "Arts";

            //idからエフェクトを取得
            prefabs.GetTable()[id].effect = CastCharToEffectName(id);
        }
    }

    //アーツを生成
    public static void InstantArts(GameObject artsPivot, string artsId)
    {
        if (artsPivot != null)
        {
            var prefabs = GetArtsInstantManager.prefabs;
            switch (artsId)
            {
                case "045": InstantArts(); break;
                case "04": InstantArts(); break;
                case "05": InstantArts(); break;
                case "45": InstantArts(); break;
                case "09": InstantArts(); break;
                case "049": InstantArts(); break;
                case "59": InstantArts(); break;
                case "059": InstantArts(); break;
                case "459": InstantArts(ArtsStatus.ArtsType.Support); break;
                case "49": InstantArts(); break;
                case "02": InstantArts(); break;
                case "024": InstantArts(); break;
                case "025": InstantArts(); break;
                case "25": InstantArts(ArtsStatus.ArtsType.Support); break;
                case "29": InstantArts(ArtsStatus.ArtsType.Support); break;
                case "24": InstantArts(ArtsStatus.ArtsType.Support); break;
                case "259": InstantArts(ArtsStatus.ArtsType.Support); break;
                case "029": InstantArts(ArtsStatus.ArtsType.Support); break;
                case "245": InstantArts(ArtsStatus.ArtsType.Support); break;
                case "249": InstantArts(ArtsStatus.ArtsType.Support); break;
                case "01": InstantArts(ArtsStatus.ArtsType.Slash); break;
                case "14": InstantArts(ArtsStatus.ArtsType.Slash); break;
                case "15": InstantArts(ArtsStatus.ArtsType.Slash); break;
                case "145": InstantArts(ArtsStatus.ArtsType.Slash); break;
                case "159": InstantArts(ArtsStatus.ArtsType.Slash); break;
                case "19": InstantArts(ArtsStatus.ArtsType.Slash); break;
                case "019": InstantArts(ArtsStatus.ArtsType.Slash); break;
                case "125": InstantArts(ArtsStatus.ArtsType.Slash); break;
                case "012": InstantArts(ArtsStatus.ArtsType.Support); break;
                case "12": InstantArts(ArtsStatus.ArtsType.Slash); break;
                case "014": InstantArts(ArtsStatus.ArtsType.Slash); break;
                case "015": InstantArts(ArtsStatus.ArtsType.Slash); break;
                case "129": InstantArts(ArtsStatus.ArtsType.Slash); break;
                case "149": InstantArts(ArtsStatus.ArtsType.Slash); break;
                case "027": InstantArts(); break;
                case "057": InstantArts(); break;
                case "079": InstantArts(); break;   //アマテラス
                case "127": InstantArts(); break;
                case "479": InstantArts(); break;
                case "07": InstantArts(); break;
                case "017": InstantArts(); break;
                case "27": InstantArts(); break;
                case "57": InstantArts(); break;
                case "147": InstantArts(); break;
                case "257": InstantArts(); break;
                case "047": InstantArts(); break;
                case "79": InstantArts(); break;
                case "47": InstantArts(); break;
                case "279": InstantArts(); break;
                case "457": InstantArts(); break;
                case "247": InstantArts(); break;
                case "579": InstantArts(); break;
                default: break;
            }

            //生成処理の方
            void InstantArts(ArtsStatus.ArtsType artsType = ArtsStatus.ArtsType.Shot)
            {
                //生成
                var obj = Instantiate(prefabs.GetTable()[artsId].prefab, artsPivot.transform);
                var artsStatus = obj.GetComponent<ArtsStatus>();

                SetArtsStatus(artsStatus, artsType);
            }

            //プールから取得の方
            void GetArts(ArtsStatus.ArtsType artsType = ArtsStatus.ArtsType.Shot)
            {
                //プールから取得
                var name = prefabs.GetTable()[artsId].prefab.name;
                var artsStatus = StartUpParticle.GetArts(name);
                if (artsStatus == null)
                {
                    Debug.LogError("Artsのプールが足りません"); return;
                }
                artsStatus.gameObject.transform.parent = artsPivot.transform;

                SetArtsStatus(artsStatus, artsType);

                artsStatus.gameObject.SetActive(true);
            }

            //打ち手の情報をArtsに格納する
            void SetArtsStatus(ArtsStatus artsStatus, ArtsStatus.ArtsType artsType)
            {
                //代入
                artsStatus.myEffectCount = artsPivot.GetComponentInParent<MyEffectCount>();
                artsStatus.myStatus = artsPivot.GetComponentInParent<Status>();
                artsStatus.artsType = artsType;
                artsStatus.artsPivot = artsPivot.transform;

                var data = artsPivot.GetComponentInParent<ArtsRequiredData>();
                artsStatus.modelSiz = data.modelSiz;

                if (artsPivot.transform.root.gameObject.tag == "Player")
                {
                    artsStatus.myObj = artsPivot.transform.root.gameObject;
                    artsStatus.type = ArtsStatus.ParticleType.Player;
                }
                else if (artsPivot.transform.parent.gameObject.tag == "Enemy")
                {
                    var obj = artsPivot.transform.parent.gameObject;
                    artsStatus.myObj = obj;
                    artsStatus.type = ArtsStatus.ParticleType.Enemy;
                }
            }
        }
    }

    //stringをエフェクト名に変換
    List<NameDefinition.EffectName> CastCharToEffectName(string str)
    {
        List<NameDefinition.EffectName> effectNames = new List<NameDefinition.EffectName>();
        char[] cArr = str.ToCharArray();
        foreach (var c in cArr)
        {
            int i = int.Parse(string.Join(null, c));
            effectNames.Add((NameDefinition.EffectName)i);
        }
        return new List<NameDefinition.EffectName>(effectNames);
    }

    //デバッグと切り替える処理
    public static string SelectArts(string artsId, string debugNum = "0")
    {
        if (debugNum == "0" && artsId != null)
        {
            return artsId;
        }
        else return debugNum;
    }


    [System.Serializable]
    public class PrefabDictionary : Serialize.TableBase<string, ArtsStatusData, Name2Prefab> { }

    [System.Serializable]
    public class Name2Prefab : Serialize.KeyAndValue<string, ArtsStatusData>
    {
        public Name2Prefab(string key, ArtsStatusData value) : base(key, value) { }
    }
}
