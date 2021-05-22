using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeManager : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;
    Animator animator;

    public Collider attackCollider; 

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;
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
        }

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
