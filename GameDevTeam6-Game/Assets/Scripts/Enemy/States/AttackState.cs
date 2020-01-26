using System;
using System.Collections;
using UnityEngine;

public class AttackState : IState
{
    private Enemy parent;

    public void Enter(Enemy parent)
    {
        this.parent = parent;
        Debug.Log("enemy in attack state");

        parent.MyRigidBody.velocity = Vector2.zero;
        parent.Direction = Vector2.zero;
    }

    public void Exit()
    {

    }

    public void Update()
    {
        if (parent.Target != null)
        {
            //check range and attack
            float distance = Vector2.Distance(parent.Target.transform.position, parent.transform.position);

            if (distance >= parent.AttackRange)
            {
                parent.ChangeState(new FollowState());
            }
        }
        else
        {
            parent.ChangeState(new IdleState());
        }
    }

    public IEnumerator Attack()
    {
        parent.IsAttacking = true;
        parent.MyAnimator.SetTrigger("attack");

        yield return new WaitForSeconds(parent.MyAnimator.GetCurrentAnimatorStateInfo(2).length);
    }
}
