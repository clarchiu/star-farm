using System;
using UnityEngine;

/*
 * Follow state responsible for following a target once it is close enough.
 * Follow state can go to attack state if target is within attack range or
 * it can go to search state if the target is killed or out of aggro range
 * - Clarence 
 */

internal class FollowState: IState
{
    private EnemyAI parent;

    public void Enter(EnemyAI parent)
    {
        this.parent = parent;
        parent.GFX.MyState = GFXStates.Moving;
        Debug.Log("enemy in follow state");
    }

    public void Exit()
    {
        parent.RB.velocity = Vector2.zero;
    }

    public void Update()
    {
        if (parent.Target != null)
        {
            //Find the target's direction
            Vector3 direction = (parent.Target.transform.position - parent.transform.position).normalized;

            parent.RB.velocity = direction * 2f;

            parent.GFX.Direction = parent.RB.velocity.normalized;

            //calculate distance between target and itself
            float distance = Vector3.Distance(parent.Target.transform.position, parent.transform.position);

            if (distance <= 1)
            {
                parent.ChangeState(new AttackState());
            }
        }
        else
        {
            parent.ChangeState(new SearchState());
        }

    }
}
