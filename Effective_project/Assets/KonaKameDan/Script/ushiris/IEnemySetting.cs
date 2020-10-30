using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ExAItype
{
    Static,
    Deffender,
    Sniper
}
public enum FindAItype
{
    Soldier,
    Commander
}
public enum StayAItype
{
    Ambush,
    Ninja,
    Return,
    follower
}

public interface IEnemySetting
{
    void SetAI(ExAItype type);

    void SetAI(FindAItype type);

    void SetAI(StayAItype type);
}
