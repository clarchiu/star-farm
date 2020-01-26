using System;
using UnityEngine;

public class FollowState : IState
{
    private Enemy parent;

    public void Enter(Enemy parent)
    {
        this.parent = parent;
        Debug.Log("enemy in follow state");
    }

    public void Exit()
    {
        parent.Direction = Vector2.zero;
    }

    public void Update()
    {
        if (parent.Target != null)
        {
            //Find the target's direction
            parent.Direction = (parent.Target.transform.position - parent.transform.position).normalized;

            //calculate distance between target and itself
            float distance = Vector2.Distance(parent.Target.transform.position, parent.transform.position);

            if (distance <= parent.AttackRange)
            {
                parent.ChangeState(new AttackState());
            }

        }
        else
        {
            parent.ChangeState(new IdleState());
        }

    }


}
