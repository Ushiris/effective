using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtsActionElements : MonoBehaviour
{
    public delegate void ArtsAction(GameObject arts);
    public Dictionary<string, ArtsAction> actions = new Dictionary<string, ArtsAction>();
}
