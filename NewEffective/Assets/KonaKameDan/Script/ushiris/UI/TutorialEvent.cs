using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialEvent : MonoBehaviour
{
    public enum EndSection
    {
        wasd,
        breakBox,
        openUI,
        fusion,
        battle,
        boss
    }

    [SerializeField] string 
        wasd, breakBox, openUI, fusion, battle, boss, portal;
    [SerializeField] TMPro.TMP_Text messageArea;
    List<string> message = new List<string>();

    [SerializeField] KeyGuideControl guide;
    [SerializeField]
    UnityEvent
        OnStartTutorial = new UnityEvent(),
        OnPassedWasd = new UnityEvent(),
        OnPassedBrakeBox=new UnityEvent(),
        OnPassedOpenFusionUI = new UnityEvent(),
        OnPassedFusion = new UnityEvent(),
        OnPassedBattle = new UnityEvent(),
        OnPassedBoss = new UnityEvent();

    private void Start()
    {
        OnStartTutorial.Invoke();

        message = new List<string>{
            wasd,
            breakBox,
            openUI,
            fusion,
            battle,
            boss,
            portal
        };
    }

    private void Update()
    {
        List<bool> input = new List<bool>
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.Space),
            Input.GetKey(KeyCode.E),
            Input.GetKey(KeyCode.Q),
            Input.GetAxis("Mouse ScrollWheel")!=0,
            Input.GetMouseButton(0),
        };

        guide.SetState(input);
    }

    IEnumerable NextMessage()
    {
        yield return wasd;
        yield return breakBox;
        yield return openUI;
        yield return fusion;
        yield return battle;
        yield return boss;
        yield return portal;
        throw new System.Exception("no message");
    }

    public void SetMessage()
    {
        messageArea.text = (string)NextMessage();
    }

    public void ShowWasdKeyGuide()
    {
        guide.Show("w");
        guide.Show("a");
        guide.Show("s");
        guide.Show("d");
        guide.Show("space");
    }

    public void ShowMouseGuide()
    {
        guide.Show("left");
        guide.Show("wheel");
    }

    public void ShowOpenUiKey()
    {
        guide.Show("E");
    }

    public void ShowFusionKey()
    {
        guide.Show("Q");
    }

    public void InvokeEvent(EndSection section)
    {
        switch (section)
        {
            case EndSection.wasd:
                OnPassedWasd.Invoke();
                break;

            case EndSection.breakBox:
                OnPassedBrakeBox.Invoke();
                break;

            case EndSection.openUI:
                OnPassedOpenFusionUI.Invoke();
                break;

            case EndSection.fusion:
                OnPassedFusion.Invoke();
                break;

            case EndSection.battle:
                OnPassedBattle.Invoke();
                break;

            case EndSection.boss:
                OnPassedBoss.Invoke();
                break;

            default:
                throw new System.Exception("unknown section.");
        }
    }
}
