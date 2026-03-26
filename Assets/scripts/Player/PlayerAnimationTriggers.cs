using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {   
        Collider2D[] colliders = Physics2D.OverlapBoxAll(player.attackCheck.position, new Vector2(player.attackCheckRadius * 2, player.attackCheckRadius), 0);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent <Entity>().Damage();
                hit.GetComponent <CharacterStats>().TakeDamage(player.stats.damage.GetValue());

                Debug.Log(player.stats.damage.GetValue());
            }
        }
    }

    private void StopAnimation()
    {
        player.anim.speed = 0;
    }
}
