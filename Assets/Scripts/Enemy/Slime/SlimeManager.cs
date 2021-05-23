using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeManager : MonoBehaviour
{
    public Transform target; //AIのターゲット
    NavMeshAgent agent; // ナビゲーション
    Animator animator;　// アニメーター

    public Collider attackCollider; //攻撃時のコライダー

    // HP
    public int maxHp;
    int hp;


    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;
        DisableCollider();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.position;
        animator.SetFloat("Distance", agent.remainingDistance);
    }

    private void OnTriggerEnter(Collider other)
    {
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
    void Damage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            animator.SetTrigger("Die");
        }
        Debug.Log("敵の残りHP" + hp);
    }

    // 攻撃判定の有効化
    public void EnableCollider()
    {
        attackCollider.enabled = true;
    }

    // 攻撃判定の無効化
    public void DisableCollider()
    {
        attackCollider.enabled = false;
    }

}
