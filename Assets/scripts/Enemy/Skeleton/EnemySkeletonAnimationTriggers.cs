using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonAnimationTriggers : MonoBehaviour
{
    private EnemySkeleton enemy =>GetComponentInParent<EnemySkeleton>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(enemy.attackCheck.position, new Vector2(enemy.attackCheckRadius * 2, enemy.attackCheckRadius), 0);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                CharacterStats targetStats = hit.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    targetStats.TakeDamage(enemy.stats.damage.GetValue());
                }
                
                hit.GetComponent<Entity>().Damage();
            }
        }
    }

    private void OpenCounterAttackWindow() => enemy.OpenCounterAttackWindow();
    private void CloseCounterAttackWindow() => enemy.CloseCounterAttackWindow();

    private void StopAnimation()
    {
        // 动画播放到最后一帧时，销毁敌人对象
        Destroy(enemy.gameObject);
    }
}
