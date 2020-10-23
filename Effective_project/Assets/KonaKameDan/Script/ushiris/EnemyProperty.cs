using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperty : IEnemySetting
{
    public static EnemyProperty Instance { get; private set; } = new EnemyProperty();
    public static float PlayerFindDistance { get; private set; } = 50;
    public static float BestAttackDistance_Melee { get; private set; } = 5;
    public static float BestAttackDistance_Range { get; private set; } = 22;
    public static float FriendFindDistance { get; private set; } = 30;
    public static float ExtraAuraDistance { get; private set; } = 5;

    public void SetPlayerFindDistance(float dist)
    {
        PlayerFindDistance = dist;
    }

    public void SetFriendFindDistance(float dist)
    {
        FriendFindDistance = dist;
    }

    public void SetExtraAuraDistance(float dist)
    {
        ExtraAuraDistance = dist;
    }

    public void SetAI(ExAItype type)
    {
        throw new System.NotImplementedException();
    }

    public void SetAI(FindAItype type)
    {
        throw new System.NotImplementedException();
    }

    public void SetAI(StayAItype type)
    {
        throw new System.NotImplementedException();
    }

    public void SetBestAttackDistance_Melee(float dist)
    {
        BestAttackDistance_Melee = BestAttackDistance_Range > dist ? dist : BestAttackDistance_Range;
    }

    public void SetBestAttackDistance_Range(float dist)
    {
        BestAttackDistance_Range = PlayerFindDistance * 0.9f > dist ? dist : PlayerFindDistance * 0.9f;
    }
}
