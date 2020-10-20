using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

#if UNITY_EDITOR
using UnityEditor; 
#endif

public class UshirisSystem : MonoBehaviour
{
    public List<GameObject> Enemy = new List<GameObject>();
    public List<Enemy> enemies = new List<Enemy>();
    [HideInInspector] public List<GameObject> gameObjects = new List<GameObject>();

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

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        system = target as UshirisSystem;

        system.Enemy.ForEach(item =>
        {
            if (GUILayout.Button("Summon " + item.name))
            {
                var enemy = Instantiate(item);
                enemy.transform.position = PlayerManager.GetManager.GetPlObj.transform.position + PlayerManager.GetManager.GetPlObj.transform.forward * 25;
            }
        });
    }
}
#endif