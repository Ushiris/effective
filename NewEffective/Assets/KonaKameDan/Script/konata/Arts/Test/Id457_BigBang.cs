using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id457_BigBang : MonoBehaviour
{
    [SerializeField] Vector3 pos = new Vector3(0, 0, 5f);
    [SerializeField] float vacuumPower = 50;
    [SerializeField] float explosionTiming = 5f;

    [SerializeField] GameObject explosionObj;
    [SerializeField] GameObject vacuumParticle;

    StopWatch timer;

    AudioSource se;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = pos;
        Arts_Process.RollReset(gameObject);
        Arts_Process.GroundPosMatch(gameObject);

        //一定時間ごとに隕石を生成
        timer = gameObject.AddComponent<StopWatch>();
        timer.LapTime = explosionTiming;
        timer.LapEvent = () => { Explosion(); };

        //SE
        se = SE_Manager.SePlay(SE_Manager.SE_NAME.Id457_BigBang_second);
    }

    void Explosion()
    {
        vacuumParticle.SetActive(false);
        explosionObj.SetActive(true);
        Destroy(gameObject, 1.5f);

        //SE
        SE_Manager.SePlay(SE_Manager.SE_NAME.Id047_PingPong_third);
        SE_Manager.ForcedPlayStop(se);

        //ダメージの処理
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            Arts_Process.Vacuum(other.gameObject, transform.position, vacuumPower);
        }
    }
}
