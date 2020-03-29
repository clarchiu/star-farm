using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/*
 * Path state responsible for turning on AIPath class to start searching for paths
 * to the target.
 * From path state, the agent can go into search state if the target ever becomes null
 * or the agent can go into follow state once it is close enough
 * - Clarence 
 */

internal class PathState : EnemyState
{
    public override void Enter(EnemyAI parent)
    {
        //Debug.Log("enemy in path state");

        base.Enter(parent);

        parent.aiPath.canMove = true;
        parent.aiPath.canSearch = true;
    }

    public override void Exit()
    {
        parent.aiPath.canMove = false;
        parent.aiPath.canSearch = false;
    }

    public override void Update()
    {
        SetGFXDirection();

        if (parent.Target == null)
        {
            //Debug.Log("target null");
            parent.ChangeState(new SearchState());
            return;
        }
        else if ( parent.aiPath.reachedEndOfPath == true )
        {
            parent.ChangeState(new FollowState());
            return;
        }
        else if (parent.InAttackRange)
        {
            //Debug.Log("in range");
            parent.ChangeState(new AttackState());
            return;
        }
    }

    protected override void SetGFXDirection()
    {
        parent.GFX.Direction = parent.aiPath.velocity.normalized;
    }

    protected override void SetGFXState()
    {
        parent.GFX.MyState = GFXStates.MOVING;
    }
}