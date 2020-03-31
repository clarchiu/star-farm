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
        base.Enter(parent);

        parent.aiPath.canMove = true;
        parent.aiPath.canSearch = true;
    }

    public override void Exit()
    {
        enemy.aiPath.canMove = false;
        enemy.aiPath.canSearch = false;
    }

    public override void Update()
    {
        base.Update();
        SetGFXDirection();

        if (enemy.Target == null)
        {
            //Debug.Log("target null");
            enemy.ChangeState(new SearchState());
            return;
        }
        else if (enemy.IsTargetInAttackRange)
        {
            enemy.ChangeState(new AttackState());
            return;
        }
        else if (enemy.aiPath.reachedEndOfPath == true)
        {
            enemy.ChangeState(new FollowState());
            return;
        }
    }

    protected override void SetGFXDirection()
    {
        enemy.GFX.Direction = enemy.aiPath.velocity.normalized;
    }

    protected override void SetGFXState()
    {
        enemy.GFX.MyState = GFXStates.MOVING;
    }
}