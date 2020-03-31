using System.Collections;
using UnityEngine;

/*
 * Attack state responsible for dealing damage to a target and
 * setting IsAttacking in EnemyGFX to true.
 * Attack state can go to follow state if the target walks out of it's attack range
 * - Clarence 
 */

internal class AttackState: EnemyState
{
    private bool isAttacking = false;

    public override void Enter(EnemyAI enemy)
    {
        Debug.Log("enenmy in attack state");

        base.Enter(enemy);
    }

    public override void Exit()
    {
        //Debug.Log("leaving attack state");
        isAttacking = false;
    }

    public override void Update()
    {
        base.Update();

        if (enemy.Target != null)
        {
            SetGFXDirection();

            if (enemy.IsTargetInAttackRange)
            {
                ITargetable targetable = enemy.Target.GetComponent<ITargetable>();

                if (targetable != null && attackCoolDown <= 0 && !isAttacking)
                {
                    enemy.StartCoroutine(Attack(targetable));
                }
            } else if (!isAttacking) // finish attack first
            {
                enemy.ChangeState(new FollowState());
                return;
            }
        }
        else
        {
            enemy.ChangeState(new SearchState());
            return;
        }
    }

    protected override void SetGFXDirection() //this makes it so that the enemy always faces the target
    {
        Vector2 directionToTarget = (enemy.Target.transform.position - enemy.transform.position).normalized;
        enemy.GFX.Direction = directionToTarget; //set direction on GFX layer
    }

    protected override void SetGFXState()
    {
        enemy.GFX.MyState = GFXStates.ATTACKING;
    }

    private IEnumerator Attack(ITargetable targetable) //TODO: might be able to define this in EnemyGFX
    {
        isAttacking = true;

        attackCoolDown = enemy.MyAttributes.attackCoolDown; 

        enemy.GFX.MyAnimator.SetTrigger("attack");

        yield return new WaitForSeconds(enemy.GFX.MyAnimator.GetCurrentAnimatorStateInfo(2).length); //check how long the animation is

        enemy.PrimaryAttack(targetable);
        isAttacking = false;

        yield break;
    }
}
