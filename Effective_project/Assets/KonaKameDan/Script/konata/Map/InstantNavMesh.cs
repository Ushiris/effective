﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 動的なナビゲーションメッシュ生成
/// </summary>
public class InstantNavMesh : MonoBehaviour
{
    NavMeshSurface navMeshSurface;

    void Awake()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    void Start()
    {
        navMeshSurface.BuildNavMesh();
    }
}