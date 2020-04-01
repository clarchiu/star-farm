using System.Collections;
using UnityEngine;

/*
 * Attack state responsible for dealing damage to a target and
 * setting IsAttacking in EnemyGFX to true.
 * Attack state can go to follow state if the target walks out of it's attack range
 * - Clarence 
 */

public class AttackState: EnemyState
{
    private bool isAttacking = false;
    private ITargetable targetable;
    private float attackCoolDown = 0f;

    public override void Enter(EnemyAI enemy)
    {
        base.Enter(enemy);
        targetable = enemy.Target.GetComponent<ITargetable>(); //get component only once
    }

    public override void Exit()
    {
        base.Exit();
        //Debug.Log("leaving attack state");
        isAttacking = false;
    }

    public override void Update()
    {
        base.Update();

        SetGFXDirection();

        if (!enemy.Target)
        {
            enemy.ChangeState(new SearchState());
            return;
        }
        if (!enemy.IsTargetInAttackRange && !isAttacking) // finish attack first
        {
            enemy.ChangeState(new FollowState());
            return;
        }
        if (enemy.IsTargetInAttackRange && !isAttacking && attackCoolDown <= 0)
        {
            //Debug.Log("attacking");
            enemy.StartCoroutine(Attack(targetable));
        }
        
        attackCoolDown -= Time.deltaTime;
    }

    protected override void SetGFXDirection() //this makes it so that the enemy always faces the target
    {
        Vector2 directionToTarget = (enemy.Target.transform.position - enemy.transform.position).normalized;
        enemy.gfx.Direction = directionToTarget; //set direction on GFX layer
    }

    protected override void SetGFXState()
    {
        enemy.gfx.MyState = GFXStates.ATTACKING;
    }

    private IEnumerator Attack(ITargetable targetable) //TODO: might be able to define this in EnemyGFX
    {
        isAttacking = true;

        attackCoolDown = enemy.MyAttributes.attackCoolDown; 

        enemy.gfx.MyAnimator.SetTrigger("attack");
        
        yield return new WaitForSeconds(enemy.gfx.MyAnimator.GetCurrentAnimatorStateInfo(2).length); //check how long the animation is

        enemy.PrimaryAttack(targetable);

        isAttacking = false;
    }
}
