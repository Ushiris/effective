using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    enum InputName:byte
    {
        W,
        A,
        S,
        D,
        E,
        Q,
        Esc,
        PushMouseLeft,
        ReleseMouceLeft,
        One,
        Twe,
        Three,
        INPUT_COUNT
    }

    List<KeyCode> useKey = new List<KeyCode>{
        KeyCode.W,
        KeyCode.A,
        KeyCode.S,
        KeyCode.D,
        KeyCode.E,
        KeyCode.Q,
        KeyCode.Escape,
        KeyCode.Mouse0,
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3
    };

    public delegate bool PlayerAction();

    public List<bool> inputs = new List<bool> {
        
    };

    bool isPause { get { return Time.timeScale < 0.1; } }


}
