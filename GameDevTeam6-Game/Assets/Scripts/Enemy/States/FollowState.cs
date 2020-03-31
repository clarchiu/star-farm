using UnityEngine;

/*
 * Follow state responsible for following a target once it is close enough.
 * Follow state can go to attack state if target is within attack range or
 * it can go to search state if the target is killed or out of aggro range
 * - Clarence 
 */

internal class FollowState: EnemyState
{
    public override void Enter(EnemyAI enemy)
    {
        base.Enter(enemy);
    }

    public override void Exit()
    {
        enemy.RB.velocity = Vector2.zero;
    }

    public override void Update()
    {
        base.Update();
        if (!enemy.IsTargetInAggroRange)
        {
            enemy.Target = null;
        }

        if (enemy.Target != null)
        {
            //Find the target's direction
            Vector3 targetDir = (enemy.Target.transform.position - enemy.transform.position).normalized;
            enemy.RB.velocity = targetDir * enemy.MyAttributes.speed;
            //parent.transform.position = targetDir * parent.MyAttributes.speed * Time.deltaTime;

            SetGFXDirection();

            if (enemy.IsTargetInAttackRange) 
            {
                enemy.ChangeState(new AttackState());
                return;
            }
        }
        else
        {
            enemy.ChangeState(new SearchState());
            return;
        }
    }

    protected override void SetGFXDirection()
    {
        enemy.GFX.Direction = enemy.RB.velocity.normalized;
        //parent.GFX.Direction = targetDir;
    }

    protected override void SetGFXState()
    {
        enemy.GFX.MyState = GFXStates.MOVING;
    }
}
