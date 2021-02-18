using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGuideControl : MonoBehaviour
{
    [SerializeField] ImageSwitcher w, a, s, d, e, q, space, wheel, left;
    List<ImageSwitcher> switchers;
    Dictionary<string, int> keyIndex = new Dictionary<string, int>
    {
        {"w",0 },
        {"a",1 },
        {"s",2 },
        {"d",3 },
        {"e",4 },
        {"q",5 },
        {"space",6 },
        {"wheel",7 },
        {"left",8 }
    };

    private void Awake()
    {
        switchers = new List<ImageSwitcher> { w, a, s, d, e, q, space, wheel, left };
    }

    public void SetState(List<bool> inputs)
    {
        int count = 0;
        switchers.ForEach(item =>
        {
            item.Switch(inputs[count]);
            count++;
        });
    }

    public void Switch(string key,bool state)
    {
        switchers[keyIndex[key]].Switch(state);
    }

    public void Show(string key,bool state=false)
    {
        switchers[keyIndex[key]].Show(state);
    }
}