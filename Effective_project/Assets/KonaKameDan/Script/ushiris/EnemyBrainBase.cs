using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyBrainBase
{
    void Think();

    void Stan(float time);

    void Blind(float time);

    void Default();

    void AddEnchant(EnemyState.Enchants enchant, float time);

    bool IsAttackable();
}
