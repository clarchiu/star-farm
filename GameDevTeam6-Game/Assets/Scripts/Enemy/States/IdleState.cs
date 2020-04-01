using UnityEngine;

public class IdleState: EnemyState
{
    public override void Enter(EnemyAI parent)
    {
        //Debug.Log("enemy in idle state");

        base.Enter(parent);
    }

    public override void Exit()
    {
        base.Exit();//implementation not needed
    }

    public override void Update()
    {
        base.Update();
        //implementation not needed
    }

    protected override void SetGFXDirection()
    {
        //implementation not needed
    }

    protected override void SetGFXState()
    {
        enemy.gfx.MyState = GFXStates.IDLING;
    }
}

