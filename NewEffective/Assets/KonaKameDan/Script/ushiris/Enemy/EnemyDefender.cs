using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDefender : EnemyBrainBase
{
    [SerializeField] MeshRenderer ColorChangeablePoint;
    [SerializeField] int material_index;
    Transform king;

    // Start is called before the first frame update
    new void Start()
    {
        mainMaterial = ColorChangeablePoint.materials[material_index];

        ApplyChangeColor = () =>
        {
            var temp = ColorChangeablePoint.materials;
            temp[material_index] = mainMaterial;
            ColorChangeablePoint.materials = temp;
        };

        base.Start();

        FindJob();

        navMesh.stoppingDistance = EnemyProperty.KnightDistance;
        Stay = ExMove;
        FindAction = ExMove;

        var rand = Random.Range(1, 10);
        if (rand >= 8)
        {
            AIset(FindAItype.Commander);
        }
        if (rand > 5)
        {
            AIset(FindAItype.Soldier);
        }
    }

    void ExMove()
    {
        if (king == null || !king.gameObject.activeSelf) FindJob();
        if (navMesh.pathStatus != UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            if(navMesh.pathStatus != NavMeshPathStatus.PathInvalid)navMesh.SetDestination(king.position);
        }
    }

    void FindJob()
    {
        List<GameObject> enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        RemoveDefender(enemies);
        RemoveNotActive(enemies);

        if (enemies.Count == 0)
        {
            AIset(FindAItype.Soldier);
        }

        List<Transform> e_trans = new List<Transform>();
        enemies.ForEach(item => e_trans.Add(item.transform));
        king = e_trans[0];

        if (enemies.Count == 1)
        {
            return;
        }

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

    void KnockBack()
    {

    }
}
