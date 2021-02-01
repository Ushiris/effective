using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPoint : MonoBehaviour
{
    public enum PointName { PlayerDamage, EnemyDamage, Shot, Barrier, Spread, Homing, Explosion, Fly, TotalScore }

    public static Dictionary<PointName, int> SetPoint = new Dictionary<PointName, int>()
    {
        {PointName.PlayerDamage,     0 },
        {PointName.EnemyDamage,      0 },
        {PointName.Shot,             0 },
        {PointName.Barrier,          0 },
        {PointName.Spread,           0 },
        {PointName.Homing,           0 },
        {PointName.Explosion,        0 },
        {PointName.Fly,              0 },
        {PointName.TotalScore,       0 },
    };

    public static void OnResetPoint()
    {
        SetPoint = new Dictionary<PointName, int>()
        {
            {PointName.PlayerDamage,     0 },
            {PointName.EnemyDamage,      0 },
            {PointName.Shot,             0 },
            {PointName.Barrier,          0 },
            {PointName.Spread,           0 },
            {PointName.Homing,           0 },
            {PointName.Explosion,        0 },
            {PointName.Fly,              0 },
            {PointName.TotalScore,       0 },
        };
    }

    public static int GetPoint(int num)
    {
        if ((PointName)num != PointName.TotalScore)
        {
            return SetPoint[(PointName)num];
        }
        else
        {
            return TotalScore();
        }
    }

    static int TotalScore()
    {
        var a = SetPoint[PointName.PlayerDamage] - SetPoint[PointName.EnemyDamage];
        var b =
            SetPoint[PointName.Shot] + SetPoint[PointName.Barrier] +
            SetPoint[PointName.Spread] + SetPoint[PointName.Homing] +
            SetPoint[PointName.Explosion] + SetPoint[PointName.Fly];
        var c = b / 6;

        if (a > 0)
        {
            return SetPoint[PointName.PlayerDamage] * c;
        }
        else
        {
            return SetPoint[PointName.PlayerDamage] * c - a;
        }
    }
}
