using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : SkeletonGroundedState
{
    public SkeletonMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySkeleton enemy) : base(_enemyBase, _stateMachine, _animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerDetected())
        {
            stateMachine.ChangeState(enemy.battleState);
            return;
        }

        bool wall = enemy.IsWallDetected();
        bool ground = enemy.IsGroundDetected();
        bool enemyInFront = enemy.IsEnemyInFront();

        if(wall || !ground || enemyInFront)
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
            return;
        }

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);
    }
}
