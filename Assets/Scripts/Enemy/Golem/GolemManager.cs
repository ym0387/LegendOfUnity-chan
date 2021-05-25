using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GolemManager : MonoBehaviour
{
    public Transform target; //AIのターゲット
    protected NavMeshAgent agent; // ナビゲーション
    protected Animator animator;　// アニメーター

    //攻撃時のコライダー
    public Collider bodyCollider;
    public Collider bodyColliderForAttack;
    public Collider rightHandCollider;
    public Collider leftHandCollider;

    // HP
    public int maxHp;
    protected int hp;

    public GolemUIManager golemUIManager;

    //死亡判定
    protected bool isDie;

    //ゲームクリアテキスト
    public GameObject gameClearText;

    // Start is called before the first frame update
    public void Start()
    {
        hp = maxHp;

        golemUIManager.Init(this);
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;
        DisableCollider();
    }

    // Update is called once per frame
    public void Update()
    {
        //Distanceが5以下になったらPlayerを追跡
        if (hp >= 0.01)
        {
            agent.destination = target.position;
            animator.SetFloat("Distance", agent.remainingDistance);

            if (Vector3.Distance(transform.position, target.position) <= 5)
            {
                transform.LookAt(target);
            }
        }
        
        GameClear();
    }

    public void OnTriggerEnter(Collider other)
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
            //５i\以上のダメージでないとのけぞらない
            if (damager.damage >= 5)
            {
                animator.SetTrigger("Hurt");
            }
            Damage(damager.damage);
        }
    }

    // 被ダメージ
    public void Damage(int damage)
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
        golemUIManager.UpdateHP(hp);
    }

    // 攻撃判定の有効化
    public void EnableColliderForAttack01()
    {
        rightHandCollider.enabled = true;
    }

    public void EnableColliderForAttack02()
    {
        bodyCollider.enabled = false;
        bodyColliderForAttack.enabled = true;
    }

    // 攻撃判定の無効化
    public void DisableCollider()
    {
        bodyCollider.enabled = true;
        bodyColliderForAttack.enabled = false;
        rightHandCollider.enabled = false;
        leftHandCollider.enabled = false;
    }

    //ゲームクリア関数
    public void GameClear()
    {
        //ボス倒したら
        if (isDie)
        {
            //ゲームクリアテキスト表示
            gameClearText.SetActive(true);

            //スペースかマウスクリックで
            if (Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Space))
            {
                //タイトル画面に戻る
                SceneManager.LoadScene("Title");
            }

        }

    }
}

