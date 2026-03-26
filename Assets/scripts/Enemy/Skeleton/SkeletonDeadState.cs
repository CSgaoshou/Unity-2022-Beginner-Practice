using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDeadState : EnemyState
{
    private EnemySkeleton enemy;

    public SkeletonDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySkeleton enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.anim.speed = 1;
        
        rb.velocity = new Vector2(0, 0);
    }

    public override void Update()
    {
        base.Update();
        rb.velocity = new Vector2(0, 0);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
