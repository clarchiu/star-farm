using System;
using UnityEngine;

public class FollowState : IState
{
    private Enemy parent;

    public void Enter(Enemy parent)
    {
        this.parent = parent;
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

            //move towards target
            parent.transform.position = Vector2.MoveTowards(parent.transform.position,
                parent.Target.transform.position, parent.Speed * Time.deltaTime);
        }
        else
        {
            parent.ChangeState(new IdleState());
        }

    }


}
