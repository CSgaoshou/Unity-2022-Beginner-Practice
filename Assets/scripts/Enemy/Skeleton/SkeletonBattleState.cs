using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Transform player;
    private EnemySkeleton enemy;
    private int moveDir;

    public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySkeleton enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;

        // 进入战斗状态时，立即面向玩家，避免背后检测导致射线方向错误
        if (player != null)
        {
            if (player.position.x > enemy.transform.position.x && enemy.facingDir == -1)
                enemy.Flip();
            else if (player.position.x < enemy.transform.position.x && enemy.facingDir == 1)
                enemy.Flip();
        }

        stateTimer = enemy.battleTime;
    }

    public override void Update()
    {
        base.Update();

        if (player == null)
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;

            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if (CanAttack())
                {
                    stateMachine.ChangeState(enemy.attackState);
                    return;
                }
            }
        }
        else
        {
            if(stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 10 )
            {
                stateMachine.ChangeState(enemy.idleState);
                return;
            }
        }

        if (player.position.x > enemy.transform.position.x)
        {
            moveDir = 1;
        }
        else if (player.position.x < enemy.transform.position.x)
        {
            moveDir = -1;
        }

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttack + enemy.attackCooldown)
        {
            enemy.lastTimeAttack = Time.time;
            return true;
        }
        return false;
    }
}
