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
    void SetPlayerFindDistance(float dist);

    void SetFriendFindDistance(float dist);

    void SetExtraAuraDistance(float dist);

    void SetBestAttackDistance_Melee(float dist);

    void SetBestAttackDistance_Range(float dist);

    void SetAI(ExAItype type);

    void SetAI(FindAItype type);

    void SetAI(StayAItype type);
}
