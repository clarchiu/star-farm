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
    EnemyGFX gfx;

    private float attackCoolDown;

    public void Enter(EnemyAI parent)
    {
        Debug.Log("enenmy in attack state");
        this.parent = parent;
        gfx = parent.GetComponentInChildren<EnemyGFX>();
        attackCoolDown = 0f;
    }

    public void Exit()
    {
        gfx.IsAttacking = false;
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
                gfx.Direction = directionToTarget;

                ITargetable targetable = parent.Target.GetComponent<ITargetable>();
                if (targetable != null && attackCoolDown <= 0 && gfx.IsAttacking != true) 
                {
                    parent.StartCoroutine(Attack(targetable));
                    //Debug.Log("attacking");

                    attackCoolDown = 2.5f; //TODO: make this different for differnt enemy types
                }

                attackCoolDown -= Time.deltaTime;

                //Debug.Log(attackCoolDown);
            }
        } else
        {
            parent.ChangeState(new SearchState());
        }
    }

    private IEnumerator Attack(ITargetable targetable) //TODO: check if this actualy runs animation
    {
        //Debug.Log("coroutine started");
        gfx.IsAttacking = true;
        gfx.MyAnimator.SetTrigger("attack");

        //targetable.RemoveHealth(10); //change to variable depending on enemy type
        //targetable.KnockBack(parent.transform.position, 50f);

        yield return new WaitForSeconds(gfx.MyAnimator.GetCurrentAnimatorStateInfo(2).length); //check how long the animation is

        gfx.IsAttacking = false;
        //Debug.Log("coroutine finished");
    }
}
