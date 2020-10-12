using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class DebugLogger : MonoBehaviour
{
    [Conditional("UNITY_EDITOR")]
    public static void Log(object message)
    {
        UnityEngine.DebugLogger.Log(message);
    }

    [Conditional("UNITY_EDITOR")]
    public static void Log(object message, Object context)
    {
        UnityEngine.DebugLogger.Log(message, context);
    }
}
