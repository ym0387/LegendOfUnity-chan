using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcManager : EnemyManager
{
    // 被ダメージ
    public override void Damage(int damage)
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
}
