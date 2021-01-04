using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField] GameObject beamObj;
    [SerializeField] ParticleSystem beamCoreParticle;
    [SerializeField] ParticleSystem beamParticle;
    [SerializeField] ParticleSystem startBlastParticle;

    public float beamRange = 5;
    public float lostStartTime = 5;
    public bool isGetEnd { get; private set; }
    public GameObject GetBeamObj => beamObj; 

    float timer;
    bool isMoveStop;
    Vector3 beamObjSiz;

    static readonly float kSizUpSpeed = 50f;
    static readonly float kDefaultSiz = 0f;
    static readonly float kStartDelay = 1f;

    public bool SePlayOneShot = false;

    // Start is called before the first frame update
    void Start()
    {
        var beamCoreObj = beamObj.transform.GetChild(0);
        beamObjSiz = beamObj.transform.localScale;
        beamObjSiz.z = kDefaultSiz;
        beamObj.transform.localScale = beamObjSiz;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoveStop)
        {
            if (timer < lostStartTime)
            {
                var time = Time.deltaTime;
                timer += time;

                if (timer > kStartDelay)
                {
                    //ビームのサイズ変更(伸ばす処理)
                    if (beamObjSiz.z < beamRange)
                    {
                        beamObjSiz = beamObj.transform.localScale;
                        beamObjSiz.z += kSizUpSpeed * time;
                        beamObj.transform.localScale = beamObjSiz;

                        if(SePlayOneShot)
                        {
                            //SE
                            Arts_Process.Se3dPlay(SE_Manager.SE_NAME.Id024_Diffusion_second, transform.position, null);
                        }
                    }
                }
            }
            else
            {
                //時間が来たらParticleを止める
                beamCoreParticle.Stop();
                beamParticle.Stop();
                startBlastParticle.Stop();
                isMoveStop = true;
                isGetEnd = true;
            }
        }
    }
}
