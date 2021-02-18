using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGuideControl : MonoBehaviour
{
    [SerializeField] ImageSwitcher w, a, s, d, space, e, q, wheel, left;
    List<ImageSwitcher> switchers;
    Dictionary<string, int> keyIndex = new Dictionary<string, int>
    {
        {"w",0 },
        {"a",1 },
        {"s",2 },
        {"d",3 },
        {"space",4 },
        {"e",5 },
        {"q",6 },
        {"wheel",7 },
        {"left",8 }
    };

    private void Awake()
    {
        switchers = new List<ImageSwitcher> { w, a, s, d, space, e, q, wheel, left };
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

    public void Invisible()
    {
        switchers.ForEach(item =>
        {
            item.Hide();
        });
    }
}