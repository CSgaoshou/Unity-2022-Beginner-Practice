using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected LayerMask whatIsEnemy;

    [Header("眩晕")]
    public float stunDuration;
    public Vector2 stunDirection;
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;

    [Header("移动信息")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;

    [Header("攻击信息")]
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector]public float lastTimeAttack;

    public string lastAnimBoolName { get; set; }

    public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();

        CheckForFallDeath();
    }

    private void CheckForFallDeath()
    {
        if (transform.position.y < -10f && !isDead)
        {
            Die();
        }
    }

    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        counterImage.SetActive(false);
    }

    public virtual bool CanBeStunned()
    {
        if (canBeStunned)
        {
            CloseCounterAttackWindow();
            return true;
        }

        return false;
    }

    public override void Die()
    {
        base.Die();
        anim.SetBool("Die", true);

        if (EnemySpawner.instance != null)
        {
            EnemySpawner.instance.RemoveEnemy(gameObject);
        }
    }

    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public virtual RaycastHit2D IsPlayerDetected()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 12, whatIsPlayer);
    }

    public virtual bool IsEnemyInFront()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(wallCheck.position, 2f, whatIsEnemy);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }
}

