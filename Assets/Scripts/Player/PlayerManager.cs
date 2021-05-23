using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    //重力等の変更ができるようにパブリック変数とする
    public float gravity;
    public float speed;
    public float jumpSpeed;
    public float rotateSpeed;

    //外部から値が変わらないようにPrivateで定義
    private CharacterController characterController;
    private Animator animator;
    private Vector3 moveDirection = Vector3.zero;

    //物理演算用
    Rigidbody rb;

    //コライダー制御用
    public Collider leftHandCollider;
    public Collider rightHandCollider;
    public Collider leftFootCollider;
    public Collider rightFootCollider;

    // HP
    public int maxHp = 100;
    int hp;
    public PlayerUIManager playerUIManager;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        DisableCollider();
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }

    //rayを使用した接地判定メソッド
    public bool CheckGrounded()
    {
        //初期位置と向き
        var ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);

        //rayの探索範囲
        var tolerance = 0.3f;

        //rayのHit判定
        //第一引数：飛ばすRay
        //第二引数：Rayの最大距離
        return Physics.Raycast(ray, tolerance);
    }

    // キャラ操作関数
    public void Move()
    {
        //rayを使用した接地判定
        if (CheckGrounded() == true)
        {
            //前進処理
            if (Input.GetKey(KeyCode.UpArrow))
            {
                animator.SetFloat("Speed", speed);
                moveDirection.z = speed;
            }
            else
            {
                animator.SetFloat("Speed", 0);
                moveDirection.z = 0;
            }

            //方向転換
            //方向キーのどちらも押されている時
            if(Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
            {

            }
            //左方向キーが押されている時
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(0, rotateSpeed * -1, 0);
            }
            //右方向キーが押されている時
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(0, rotateSpeed, 0);
            }

            //jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpSpeed;
            }

            //重力を発生させる
            moveDirection.y -= gravity * Time.deltaTime;

            //移動の実行
            Vector3 globalDirection = transform.TransformDirection(moveDirection);
            characterController.Move(globalDirection * Time.deltaTime);

            //速度が０以上の時、Runを実行する
            //animator.SetBool("Run", moveDirection.z > 0.0f);
        }
    }

    // 攻撃関数
    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("Attack");
        }
    }

    // 当たり判定
    private void OnTriggerEnter(Collider other)
    {
        // ダメージを与えるものにぶつかったら
        Damager damager = other.GetComponent<Damager>();
        if (damager != null)
        {
            Debug.Log("ダメージを受けた");
            animator.SetTrigger("Damage");
            Damage(damager.damage);
        }
    }

    // 被ダメージ
    void Damage(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            hp = 0;
            animator.SetTrigger("Die");
        }
        Debug.Log("プレイヤーの残りHP" + hp);
        playerUIManager.UpdateHP(hp);
    }


    // 攻撃判定の有効化
    public void EnableCollider()
    {
        leftHandCollider.enabled = true;
        rightHandCollider.enabled = true;
        leftFootCollider.enabled = true;
        rightFootCollider.enabled = true;
    }

    // 攻撃判定の無効化
    public void DisableCollider()
    {
        leftHandCollider.enabled = false;
        rightHandCollider.enabled = false;
        leftFootCollider.enabled = false;
        rightFootCollider.enabled = false;
    }
}
