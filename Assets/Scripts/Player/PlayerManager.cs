using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    //重力等の変更ができるようにパブリック変数とする
    public float gravity;
    public float speed;
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

    //UI
    public PlayerUIManager playerUIManager;

    // HP
    public float maxHp = 100;
    float hp;

    //スタミナ
    public float maxStamina = 100;
    float stamina;

    //Die判定
    bool isDie;

    //GameOverテキスト
    public GameObject gameOverText;

    void Start()
    {
        // 各コンポーネント取得
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        //コライダー無効
        DisableCollider();

        // 最大HP/スタミナ付与
        hp = maxHp;
        stamina = maxStamina;
        playerUIManager.Init(this);

    }

    void Update()
    {
        Move();
        Attack();
        AutoRecovery();
        GameOver();
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
        if (isDie)
        {
            return;
        }

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

            //移動の実行
            Vector3 globalDirection = transform.TransformDirection(moveDirection);
            characterController.Move(globalDirection * Time.deltaTime);
        }
    }

    // 攻撃関数
    public void Attack()
    {
        //Aキーの入力
        if (Input.GetKeyDown(KeyCode.A))
        {
            //スタミナが20以上の場合
            if(stamina >= 30)
            {
                //スタミナを消費して攻撃
                stamina -= 30;
                playerUIManager.UpdateStamina(stamina);
                animator.SetTrigger("Attack");
            }
        }
    }

    // 当たり判定
    private void OnTriggerEnter(Collider other)
    {
        if (isDie)
        {
            return;
        }

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
            isDie = true;
        }
        Debug.Log("プレイヤーの残りHP" + hp);
        playerUIManager.UpdateHP(hp);

    }

    // ジャブ時攻撃判定の有効化
    public void EnableColliderForJab()
    {
        leftHandCollider.enabled = true;
    }

    // ハイキック時の攻撃判定の有効化
    public void EnableColliderForHikick()
    {
        rightFootCollider.enabled = true;
    }

    // スピンキック時の攻撃判定の有効化
    public void EnableColliderForSpinkick()
    {
        leftFootCollider.enabled = true;
    }

    // 攻撃判定の無効化
    public void DisableCollider()
    {
        leftHandCollider.enabled = false;
        rightHandCollider.enabled = false;
        leftFootCollider.enabled = false;
        rightFootCollider.enabled = false;
    }

    //自動回復
    public void AutoRecovery()
    {
        if (isDie)
        {
            return;
        }
        //HP・スタミナの自動回復
        if (hp <= maxHp)
        {
            hp += 0.005f;
            playerUIManager.UpdateHP(hp);
        }
        if(stamina <= maxStamina)
        {
            stamina += 0.15f;
            playerUIManager.UpdateStamina(stamina);
        }
    }

    //ゲームオーバ関数
    public void GameOver()
    {
        //ユニティちゃん死亡時
        if (isDie)
        {
            //ゲームオーバーテキスト表示
            gameOverText.SetActive(true);

            //スペースかマウスクリックで
            if(Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Space))
            {
                //タイトル画面に戻る
                SceneManager.LoadScene("Title");
            }
            
        }

    }
}
