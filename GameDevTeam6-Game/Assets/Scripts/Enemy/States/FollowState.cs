using UnityEngine;

/*
 * Follow state responsible for following a target once it is close enough.
 * Follow state can go to attack state if target is within attack range or
 * it can go to search state if the target is killed or out of aggro range
 * - Clarence 
 */

internal class FollowState: EnemyState
{
    public override void Enter(EnemyAI parent)
    {
        Debug.Log("enemy in follow state");

        base.Enter(parent);
    }

    public override void Exit()
    {
        parent.RB.velocity = Vector2.zero;
    }

    public override void Update()
    {
        if (!parent.TargetInAggroRange)
        {
            parent.Target = null;
        }

        if (parent.Target != null)
        {
            //Find the target's direction
            Vector2 direction = (parent.Target.transform.position - parent.transform.position).normalized;             
            parent.RB.velocity = direction * parent.MyAttributes.speed; 

            SetGFXDirection();

            if (parent.InAttackRange) 
            {
                parent.ChangeState(new AttackState());
                return;
            }
        }
        else
        {
            parent.ChangeState(new SearchState());
            return;
        }
    }

    protected override void SetGFXDirection()
    {
        parent.GFX.Direction = parent.RB.velocity.normalized;
    }

    protected override void SetGFXState()
    {
        parent.GFX.MyState = GFXStates.MOVING;
    }
}
