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
            Vector3 targetDir = (parent.Target.transform.position - parent.transform.position).normalized;
            parent.RB.velocity = targetDir * parent.MyAttributes.speed;
            //parent.transform.position = targetDir * parent.MyAttributes.speed * Time.deltaTime;

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
        //parent.GFX.Direction = targetDir;
    }

    protected override void SetGFXState()
    {
        parent.GFX.MyState = GFXStates.MOVING;
    }
}
