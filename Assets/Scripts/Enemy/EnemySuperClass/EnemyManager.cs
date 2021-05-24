﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public Transform target; //AIのターゲット
    NavMeshAgent agent; // ナビゲーション
    Animator animator;　// アニメーター

    public Collider attackCollider; //攻撃時のコライダー

    // HP
    public int maxHp;
    int hp;

    public EnemyUIManager enemyUIManager;

    //死亡判定
    static bool isDie;

    // Start is called before the first frame update
    public virtual void Start()
    {
        hp = maxHp;

        enemyUIManager.Init(this);
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;
        DisableCollider();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (isDie)
        {
            return;
        }

        //Playerを追跡
        agent.destination = target.position;
        animator.SetFloat("Distance", agent.remainingDistance);

        //Distanceが5以下になったらPlayerの方を向く
        if (Vector3.Distance(transform.position, target.position) <= 5
            && hp >= 0.01)
        {
            transform.LookAt(target);
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (isDie)
        {
            return;
        }

        // ダメージを与えるものにぶつかったら
        Damager damager = other.GetComponent<Damager>();
        if (damager != null)
        {
            Debug.Log("敵にダメージ");
            animator.SetTrigger("Hurt");
            Damage(damager.damage);
        }
    }

    // 被ダメージ
    public virtual void Damage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            animator.SetTrigger("Die");
            Destroy(gameObject, 5f);
            isDie = true;
        }
        Debug.Log("敵の残りHP" + hp);
        enemyUIManager.UpdateHP(hp);
    }

    // 攻撃判定の有効化
    public virtual void EnableCollider()
    {
        attackCollider.enabled = true;
    }

    // 攻撃判定の無効化
    public virtual void DisableCollider()
    {
        attackCollider.enabled = false;
    }
}