﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MoveState = EnemyState.MoveState;
using Enchants = EnemyState.Enchants;
using System;
using System.Linq;

public class EnemyBrainBase : MonoBehaviour
{
    public EnemyState state = new EnemyState();

    protected static GameObject player;
    protected NavMeshAgent navMesh;
    protected StopWatch thinkTimer;
    protected List<StopWatch> EnchantTimer=new List<StopWatch>();
    protected Vector3 DefaultPos;
    protected Formation formation = new Formation();
    protected Transform HidePos;
    protected Rigidbody rb;
    protected Enemy enemyData;
    public bool IsCommand { get; protected set; } = false;
    protected bool IsLockAI = false;

    public void Awake()
    {
        enemyData = GetComponent<Enemy>();
        thinkTimer = gameObject.AddComponent<StopWatch>();
        navMesh = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        for(int i = 0; i < (int)Enchants.ENCHANT_AMOUNT; ++i)
        {
            EnchantTimer.Add(gameObject.AddComponent<StopWatch>());
            EnchantTimer[i].IsActive = false;
        }
    }

    protected void Start()
    {
        if (player == null) player = GameObject.FindWithTag("Player");

        DefaultPos = transform.position;
        thinkTimer.LapTime = 0.5f;
        InitDefaultAction();

        StopWatch.Summon(3, () => { _ = UnityEngine.Random.Range(0, 1) == 0 ? formation++ : formation--; }, gameObject);

        formation.value = (Formation.Formation_)UnityEngine.Random.Range(0, (int)Formation.Formation_.FORMATION_COUNT);

        state.moves = new Dictionary<Enchants, EnemyState.EnchantMove>
        {
            {
                Enchants.Stan,
                ()=>
                {
                    navMesh.SetDestination(transform.position);
                }
            },
            {
                Enchants.Blind,()=>Stay()
            },
        };

        rb.velocity = Vector3.zero;
    }

    void InitDefaultAction()
    {
        Think = StandardThink;
        Stan = StandardStan;
        Blind = StandardBlind;
        Stay = ReturnToHome;
        IsAttackable = IsAttackable_;
        FindFlag = StandardFindFlag;
        FindAction = FindAction_Circle;
        EnchantAction = StandardEnchantAction;
    }

    //オブジェクトがアクティブになった時 
    protected void OnEnable()
    {
        if (navMesh != null) navMesh.updatePosition = true;
        Hide();
    }

    //オブジェクトが非表示になった時 
    void OnDisable()
    {
        if (navMesh != null) navMesh.updatePosition = false;
    }

    public delegate void EnemyBrainAction();
    public delegate bool EnemyBrainFlag();
    public delegate void EnemyStateChange(float time);

    public StopWatch.TimeEvent Think { get => thinkTimer.LapEvent; protected set => thinkTimer.LapEvent = value; }
    public EnemyStateChange Stan { get; protected set; }
    public EnemyStateChange Blind { get; protected set; }
    public EnemyBrainAction Stay { get; protected set; }
    public EnemyBrainFlag IsAttackable { get; protected set; }
    public EnemyBrainFlag FindFlag { get; protected set; }
    public EnemyBrainAction FindAction { get; protected set; }
    public EnemyBrainAction EnchantAction { get; protected set; }

    public void AddEnchant(Enchants enchant, float time)
    {
        state.enchants[(int)enchant] = true;
        EnchantTimer[(int)enchant].ResetTimer();
        EnchantTimer[(int)enchant].LapEvent = () => { state.enchants[(int)enchant] = false; EnchantTimer[(int)enchant].SetActive(false); };
        EnchantTimer[(int)enchant].LapTime = time;
        EnchantTimer[(int)enchant].IsActive = true;
    }

    private void StandardThink()
    {
        switch (state.move)
        {
            case MoveState.Chase:
                LookAtPlayerXZ();
                FindAction();
                break;

            case MoveState.Stay:
                Stay();
                break;

            case MoveState.Confuse:
                Stay();
                break;

            default:
                DebugLogger.Log(gameObject.name + "「こういう時(" + state.ToString() + ")にどうすればいいのかわからん」");
                break;
        }

        EnchantAction();
    }

    private void ReturnToHome()
    {
        navMesh.SetDestination(DefaultPos);
    }

    private bool IsAttackable_()
    {
        switch (state.move)
        {
            case MoveState.Chase:
                return true;
            case MoveState.Stay:
                return false;
            case MoveState.Confuse:
                return true;
            case MoveState.STATE_AMOUNT:
                throw new NotImplementedException();
            default:
                throw new NotImplementedException();
        }
    }

    private void StandardStan(float time)
    {
        AddEnchant(Enchants.Stan, time);
        Think();
    }

    private void StandardBlind(float time)
    {
        AddEnchant(Enchants.Blind, time);
        Think();
    }

    public void FindAction_Circle()
    {
        Vector3 add = Vector3.zero;

        switch (formation.value)
        {
            case Formation.Formation_.forward:
                add += Vector3.forward * EnemyProperty.BestAttackDistance_Melee;
                break;
            case Formation.Formation_.forward_right:
                add += (Vector3.forward + Vector3.right) * (EnemyProperty.BestAttackDistance_Melee * 0.7f);
                break;
            case Formation.Formation_.right:
                add += Vector3.right * EnemyProperty.BestAttackDistance_Melee;
                break;
            case Formation.Formation_.behind_right:
                add += (Vector3.back + Vector3.right) * (EnemyProperty.BestAttackDistance_Melee * 0.7f);
                break;
            case Formation.Formation_.behind:
                add += Vector3.back*EnemyProperty.BestAttackDistance_Melee;
                break;
            case Formation.Formation_.behind_left:
                add += (Vector3.back + Vector3.left) * (EnemyProperty.BestAttackDistance_Melee * 0.7f);
                break;
            case Formation.Formation_.left:
                add += Vector3.left*EnemyProperty.BestAttackDistance_Melee;
                break;
            case Formation.Formation_.forward_left:
                add += (Vector3.forward + Vector3.right) * (EnemyProperty.BestAttackDistance_Melee * 0.7f);
                break;
            default:
                break;
        }

        navMesh.SetDestination(player.transform.position + add);
        LookAtPlayerXZ();
    }

    private void StandardEnchantAction()
    {
        for (int i = 0; i < state.enchants.Count; i++)
        {
            if (state.enchants[i])
            {
                state.moves[(Enchants)i]();
            }
        }
    }

    private bool StandardFindFlag() 
    {
        return Vector3.Distance(player.transform.position, transform.position) <= EnemyProperty.PlayerFindDistance;
    }

    public void Hide()
    {
        var effects=new List<GameObject>();
        foreach (var item in FindObjectsOfType<TreasureBoxPresenter>())
        {
            effects.Add(item.gameObject);
        }
        if (effects.Count == 0)
        {
            HidePos = transform;
            return;
        }
        List<Transform> e_trans = new List<Transform>();
        effects.ForEach(item => e_trans.Add(item.transform));
        HidePos = e_trans[0];

        var k_dist = Vector3.Distance(transform.position, HidePos.position);
        e_trans.ForEach(item =>
        {
            if (k_dist > Vector3.Distance(item.position, HidePos.position))
            {
                HidePos = item;
                k_dist = Vector3.Distance(transform.position, HidePos.position);
            }
        });
    }

    public void Follow(Transform boss)
    {
        if (IsLockAI) return;

        FindAction = () => navMesh.SetDestination(boss.position);
        Stay = () => navMesh.SetDestination(boss.position);
    }

    public void AIset(StayAItype type)
    {
        if (IsLockAI) return;

        switch (type)
        {
            case StayAItype.Ambush:
                Stay = () =>
                {
                    navMesh.SetDestination(player.transform.position + player.transform.right * (UnityEngine.Random.Range(0, 1) == 0 ? 20 : -20));
                };
                break;

            case StayAItype.Ninja:
                FindAction = () => { };
                Hide();
                Stay = () => { Hide(); navMesh.SetDestination(HidePos.position); };
                break;

            case StayAItype.Return:
                Stay = ReturnToHome;
                break;

            default:
                break;
        }
    }

    public void AIset(FindAItype type)
    {
        if (IsLockAI) return;

        switch (type)
        {
            case FindAItype.Soldier:
                Stay = ReturnToHome;
                FindAction = FindAction_Circle;
                break;

            case FindAItype.Commander:
                IsCommand = true;
                Influence();
                break;

            default:
                break;
        }
    }

    void Influence()
    {
        var enemies = new List<GameObject>();
        foreach (var item in FindObjectsOfType<Enemy>())
        {
            enemies.Add(item.gameObject);
        }
        RemoveNotActive(enemies);
        RemoveCommand(enemies);

        if (enemies.Count == 0) return;

        var tr = new List<Transform>();
        enemies.ForEach(item => tr.Add(item.transform));

        List<float> dist = new List<float> { Vector3.Distance(transform.position, tr[0].position) };
        List<int> idx = new List<int>() { 0 };

        for (int j = 0; j < tr.Count; j++)
        {
            for (int i = 0; i < dist.Count; i++)
            {
                var dist_x = Vector3.Distance(transform.position, tr[j].position);
                if (dist[i] > dist_x || i == dist.Count - 1)
                {
                    dist.Insert(i, dist_x);
                    idx.Insert(i, j);
                    break;
                }
            }
        }

        for (int i = 0; i < (enemies.Count < 4 ? enemies.Count : 4); i++)
        {
            enemies[idx[i]].GetComponent<EnemyBrainBase>().Follow(transform);
        }
    }

    protected void RemoveNotActive(List<GameObject> enemies)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            var item = enemies[i];
            if (!item.activeSelf) enemies.Remove(item);
        }
    }

    protected void RemoveCommand(List<GameObject> enemies)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            var item = enemies[i];
            if (item.GetComponent<EnemyBrainBase>().IsCommand) enemies.Remove(item);
        }
    }

    protected void LookAtPlayerXZ()
    {
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        enemyData.MuzzleLookAt(player.transform.position);
    }
}
