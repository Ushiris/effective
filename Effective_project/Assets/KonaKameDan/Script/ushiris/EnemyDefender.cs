﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyDefender : EnemyBrainBase
{
    Transform king;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        FindJob();

        navMesh.stoppingDistance = EnemyProperty.ExtraAuraDistance;
        Default = ExMove;
        FindAction = ExMove;
    }

    void ExMove()
    {
        if (king == null) FindJob();
        navMesh.SetDestination(king.position);
    }

    void FindJob()
    {
        List<GameObject> enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        RemoveDefender(enemies);
        List<Transform> e_trans = new List<Transform>();
        enemies.ForEach(item => e_trans.Add(item.transform));
        king = e_trans[0];

        var k_dist = Vector3.Distance(transform.position, king.position);
        e_trans.ForEach(item =>
        {
            if (k_dist > Vector3.Distance(item.position, king.position))
            {
                king = item;
                k_dist = Vector3.Distance(transform.position, king.position);
            }
        });
    }

    void RemoveDefender(List<GameObject> enemies)
    {
        for(int i=0;i<enemies.Count;i++)
        {
            var item = enemies[i];
            if (item.name.Contains("Defender")) enemies.Remove(item);
        }
    }
}
