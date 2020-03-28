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
    private float attackCoolDown = 0f;
    private bool isAttacking = false;

    public override void Enter(EnemyAI parent)
    {
        //Debug.Log("enenmy in attack state");

        base.Enter(parent);
    }

    public override void Exit()
    {
        isAttacking = false;
    }

    public override void Update()
    {
        if (parent.Target != null)
        {
            SetGFXDirection();

            if (parent.InAttackRange)
            {
                ITargetable targetable = parent.Target.GetComponent<ITargetable>();

                if (targetable != null && attackCoolDown <= 0 && !isAttacking)
                {
                    parent.StartCoroutine(Attack(targetable));
                }
            } else 
            {
                parent.ChangeState(new FollowState());
                return;
            }

            attackCoolDown -= Time.deltaTime;
        }
        else
        {
            parent.ChangeState(new SearchState());
            return;
        }
    }

    protected override void SetGFXDirection()
    {
        //this makes it so that the enemy always faces the target
        Vector2 directionToTarget = (parent.Target.transform.position - parent.transform.position).normalized;
        parent.GFX.Direction = directionToTarget; //set direction on GFX layer
    }

    protected override void SetGFXState()
    {
        parent.GFX.MyState = GFXStates.ATTACKING;
    }

    private IEnumerator Attack(ITargetable targetable) //TODO: might be able to define this in EnemyGFX
    {
        isAttacking = true;
        attackCoolDown = parent.MyAttributes.attackCoolDown; 

        parent.GFX.MyAnimator.SetTrigger("attack");

        targetable.RemoveHealth(parent.gameObject, parent.MyAttributes.attackDamage); 
        targetable.KnockBack(parent.transform.position, 50f); //TODO: make amount of knockback scale with damage?

        yield return new WaitForSeconds(parent.GFX.MyAnimator.GetCurrentAnimatorStateInfo(2).length); //check how long the animation is

        isAttacking = false;

        yield break;
    }
}
