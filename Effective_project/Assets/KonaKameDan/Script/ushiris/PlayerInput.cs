using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public delegate bool PlayerAction();

    enum KeyMean
    {
        forward, back, left, right, dush, fusionMenu, fusion, fire, arts1, arts2, arts3, select
    }

    public static List<KeyCode> keys = new List<KeyCode>
    {
        KeyCode.W,KeyCode.S,KeyCode.A,KeyCode.D,KeyCode.LeftShift,KeyCode.E,KeyCode.Q,KeyCode.Alpha1,KeyCode.Alpha2,KeyCode.Alpha3,/* select is brank */KeyCode.None
    };

    public static List<PlayerAction> inputs = new List<PlayerAction>
    {
        ()=>{ return Time.timeScale > 0.1f&&Input.GetKey(keys[0]); },
        ()=>{ return Time.timeScale > 0.1f&&Input.GetKey(keys[1]); },
        ()=>{ return Time.timeScale > 0.1f&&Input.GetKey(keys[2]); },
        ()=>{ return Time.timeScale > 0.1f&&Input.GetKey(keys[3]); },
        ()=>{ return Time.timeScale > 0.1f&&Input.GetKey(keys[4]); },
        ()=>{ return Time.timeScale > 0.1f&&Input.GetKey(keys[5]); },
        ()=>{ return Time.timeScale > 0.1f&&Input.GetKey(keys[6]); },
        ()=>{ return !IsOpenGUI()&&Input.GetKey(keys[7]); },
        ()=>{ return Time.timeScale > 0.1f&&Input.GetKey(keys[8]); },
        ()=>{ return Time.timeScale > 0.1f&&Input.GetKey(keys[9]); },
        ()=>{ return Time.timeScale > 0.1f&&Input.GetKey(keys[10]); },
        ()=>{ return Time.timeScale > 0.1f&&Input.GetKey(keys[11]); }
    };

    static bool IsOpenGUI()
    {
        var UI = FindObjectOfType(typeof(UI_Manager)) as UI_Manager;

        return UI.gameObject.activeSelf || Time.timeScale > 0.1f;
    }

    static bool GetInput(KeyMean key)
    {
        return inputs[(int)key]();
    }
}