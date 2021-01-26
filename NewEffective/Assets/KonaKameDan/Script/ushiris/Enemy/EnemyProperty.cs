using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperty
{
    public static EnemyProperty Instance            { get; private set; } = new EnemyProperty();
    public static float PlayerFindDistance          { get; private set; } = 50;
    public static float BestAttackDistance_Melee    { get; private set; } = 5;
    public static float BestAttackDistance_Range    { get; private set; } = 22;
    public static float FriendFindDistance          { get; private set; } = 30;
    public static float KnightDistance              { get; private set; } = 5;
    public static float BossAngryDistance           { get; private set; } = 7;
    public static float PortalDefenceDistance       { get; private set; } = 25;

    public static float AimSpeed = .5f;

    public static string ColorID = "Color_DB601A07";

    public static Dictionary<string, Color> enemyColors = new Dictionary<string, Color>
    {
        { "Ambush_Soldier", Color.cyan},
        { "Ambush_Commander", Color.blue},
        { "Ninja_Soldier", Color.magenta},
        { "Ninja_Commander", Color.grey},
        { "Return_Soldier", Color.clear},//clear is default color. it is not true clear.
        { "Return_Commander", Color.red},
        { "Follower",Color.green }
    };

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
        KnightDistance = dist;
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
