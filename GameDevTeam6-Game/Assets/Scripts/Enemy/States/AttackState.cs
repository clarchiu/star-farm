using System;
using UnityEngine;

public class AttackState : IState
{
    private Enemy parent;

    public void Enter(Enemy parent)
    {
        this.parent = parent;
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
}
