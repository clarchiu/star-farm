using UnityEngine;

/*
 * Follow state responsible for following a target once it is close enough.
 * Follow state can go to attack state if target is within attack range or
 * it can go to search state if the target is killed or out of aggro range
 * - Clarence 
 */

public class FollowState: EnemyState
{
    public override void Enter(EnemyAI enemy)
    {
        base.Enter(enemy);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.rb.velocity = Vector2.zero;
    }

    public override void Update()
    {
        base.Update();

        if (!enemy.CanMove) { return; }
        
        if (!enemy.IsTargetInAggroRange)
        {
            enemy.Target = null;
        }
        if (!enemy.Target)
        {
            enemy.ChangeState(new SearchState());
            return;
        }
        //Find the target's direction
        Vector3 targetDir = (enemy.Target.transform.position - enemy.transform.position).normalized;
        enemy.rb.velocity = targetDir * enemy.MyAttributes.speed;
        //parent.transform.position = targetDir * parent.MyAttributes.speed * Time.deltaTime;
        SetGFXDirection();

        if (enemy.IsTargetInAttackRange)
        {
            enemy.ChangeState(new AttackState());
            return;
        }
    }

    protected override void SetGFXDirection()
    {
        enemy.gfx.Direction = enemy.rb.velocity.normalized;
        //parent.GFX.Direction = targetDir;
    }

    protected override void SetGFXState()
    {
        enemy.gfx.MyState = GFXStates.MOVING;
    }
}
