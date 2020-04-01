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

public class PathState : EnemyState
{
    public override void Enter(EnemyAI enemy)
    {
        base.Enter(enemy);
        //parent.aiPath.canMove = true;
        //parent.aiPath.canSearch = true;
        enemy.CanMove = true;
    }

    public override void Exit()
    {
        base.Exit();
        //enemy.aiPath.canMove = false;
        //enemy.aiPath.canSearch = false;
        enemy.CanMove = false;
    }

    public override void Update()
    {
        base.Update();
        if (!enemy.Target)
        {
            //Debug.Log("target null");
            enemy.ChangeState(new SearchState());
            return;
        }

        SetGFXDirection();

        if (enemy.aiPath.reachedEndOfPath == true)
        {
            enemy.ChangeState(new FollowState());
            return;
        }
        //else if (enemy.IsTargetInAttackRange)
        //{
        //    Debug.Log("in attack range");
        //    enemy.ChangeState(new AttackState());
        //    return;
        //}
    }

    protected override void SetGFXDirection()
    {
        enemy.gfx.Direction = enemy.aiPath.velocity.normalized;
    }

    protected override void SetGFXState()
    {
        enemy.gfx.MyState = GFXStates.MOVING;
    }
}