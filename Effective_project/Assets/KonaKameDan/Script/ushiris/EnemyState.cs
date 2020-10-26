using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyState
{
    public enum MoveState
    {
        Chase,
        Stay,
        Confuse,

        STATE_AMOUNT
    }
    public enum Enchants:int
    {
        Stan,
        Blind,

        ENCHANT_AMOUNT
    }

    public MoveState move = MoveState.Stay;
    public List<bool> enchants = new bool[(int)Enchants.ENCHANT_AMOUNT].ToList();

    public delegate void EnchantMove();
    public Dictionary<Enchants, EnchantMove> moves;

    public bool IsFine()
    {
        bool result = true;
        enchants.ForEach((item) => { if (item == true) result = false; });
        return result;
    }

    public static bool IsAttackable(MoveState move)
    {
        switch (move)
        {
            case MoveState.Chase:
                return true;
            case MoveState.Stay:
                return false;
            case MoveState.Confuse:
                return true;
            default:
                return false;
        }
    }
}
