using System.Collections;
using UnityEngine;

/*
 * Attack state responsible for dealing damage to a target and
 * setting IsAttacking in EnemyGFX to true.
 * Attack state can go to follow state if the target walks out of it's attack range
 * - Clarence 
 */

internal class AttackState : IState
{
    EnemyAI parent;

    private float attackCoolDown;

    public void Enter(EnemyAI parent)
    {
        Debug.Log("enenmy in attack state");
        this.parent = parent;
        attackCoolDown = 0f;
        parent.GFX.MyState = GFXStates.Attacking;
    }

    public void Exit()
    {
        //parent.GFX.IsAttacking = false;
    }

    public void Update()
    {
        if (parent.Target != null)
        {
            float distance = Vector2.Distance(parent.Target.transform.position, parent.transform.position);

            if (distance > 1.7) //start following if target is 1.7 units away 
            {
                parent.ChangeState(new FollowState());
            }
            else if (distance <= 1.3) //TODO: this needs to be changed to a variable depending on the enemy type
            {
                //this makes it so that the enemy always faces the target
                Vector2 directionToTarget = (parent.Target.transform.position - parent.transform.position).normalized;
                parent.GFX.Direction = directionToTarget;

                ITargetable targetable = parent.Target.GetComponent<ITargetable>();

                if (targetable != null && attackCoolDown <= 0 /*&& parent.GFX.IsAttacking != true*/) 
                {
                    parent.StartCoroutine(Attack(targetable));

                    attackCoolDown = 2.5f; //TODO: make this different for differnt enemy types
                }

                attackCoolDown -= Time.deltaTime;

            }
        } else
        {
            parent.ChangeState(new SearchState());
        }
    }

    private IEnumerator Attack(ITargetable targetable) //TODO: might be able to define this in EnemyGFX
    {
        //parent.GFX.IsAttacking = true;

        parent.GFX.MyAnimator.SetTrigger("attack");

        targetable.RemoveHealth(10); //TODO: change damage amount to variable depending on enemy type
        targetable.KnockBack(parent.transform.position, 50f); //TODO: make amount of knockback scale with damage?

        yield return new WaitForSeconds(parent.GFX.MyAnimator.GetCurrentAnimatorStateInfo(2).length); //check how long the animation is

        //parent.GFX.IsAttacking = false;
    }
}
