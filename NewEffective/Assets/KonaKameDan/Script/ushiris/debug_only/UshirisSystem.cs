using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UshirisSystem : MonoBehaviour
{
    public delegate void DebugAct(); 
    public List<GameObject> Enemy = new List<GameObject>();
    public List<Enemy> enemies = new List<Enemy>();
    [HideInInspector] public List<GameObject> gameObjects = new List<GameObject>();
    [HideInInspector] public int select_enemy = 0;

    private void Awake()
    {
        gameObjects = new List<GameObject>();
        enemies = new List<Enemy>();
    }

    void Reset()
    {
        gameObjects.ForEach(item => Destroy(item));
        gameObjects = new List<GameObject>();
        enemies = new List<Enemy>();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(UshirisSystem))]
public class UshirisSystemEditor : Editor
{
    UshirisSystem system;

    StayAItype? stayAItype = StayAItype.Return;
    FindAItype? findAItype = FindAItype.Soldier;
    float BestAttackDistance_Melee = EnemyProperty.BestAttackDistance_Melee;
    float BestAttackDistance_Range = EnemyProperty.BestAttackDistance_Range;
    float KnightDistance = EnemyProperty.KnightDistance;
    float PlayerFindDistance = EnemyProperty.PlayerFindDistance;

    public override void OnInspectorGUI()
    {
        system = target as UshirisSystem;

        BestAttackDistance_Melee = EditorGUILayout.FloatField("飛翔Enemyの距離感", BestAttackDistance_Range);
        BestAttackDistance_Range = EditorGUILayout.FloatField(FindAItype.Soldier.ToString() + "の距離感", BestAttackDistance_Melee);
        KnightDistance = EditorGUILayout.FloatField("騎士の距離感", KnightDistance);
        PlayerFindDistance= EditorGUILayout.FloatField("Enemyの索敵半径", PlayerFindDistance);

        EnemyProperty.Instance.SetBestAttackDistance_Melee(BestAttackDistance_Melee);
        EnemyProperty.Instance.SetBestAttackDistance_Range(BestAttackDistance_Range);
        EnemyProperty.Instance.SetPlayerFindDistance(PlayerFindDistance);
        EnemyProperty.Instance.SetExtraAuraDistance(KnightDistance);

        system.Enemy.ForEach(item =>
        {
            if (item == null) return;
            if (GUILayout.Button("召喚：" + item.name))
            {
                var enemy = Instantiate(item);
                enemy.transform.position = PlayerManager.GetManager.GetPlObj.transform.position + PlayerManager.GetManager.GetPlObj.transform.forward * 25;
                system.enemies.Add(enemy.GetComponent<Enemy>());
                enemy.SetActive(true);
            }
        });

        system.select_enemy = EditorGUILayout.IntField("index", system.select_enemy);
        stayAItype = EditorGUILayout.EnumPopup("StayAItype", stayAItype) as StayAItype?;
        findAItype = EditorGUILayout.EnumPopup("FindAItype", findAItype) as FindAItype?;

        if (GUILayout.Button("enemies[" + system.select_enemy + "]を" + stayAItype.ToString() + "に改造する"))
        {
            system.enemies[system.select_enemy].Brain.AIset(stayAItype == null ? StayAItype.Return : (StayAItype)stayAItype);
        }
        if (GUILayout.Button("enemies[" + system.select_enemy + "]を" + findAItype.ToString() + "に改造する"))
        {
            system.enemies[system.select_enemy].Brain.AIset(findAItype == null ? FindAItype.Soldier : (FindAItype)findAItype);
        }

        base.OnInspectorGUI();
    }
}
#endif