using UnityEngine;

internal class IdleState: EnemyState
{
    public override void Enter(EnemyAI parent)
    {
        //Debug.Log("enemy in idle state");

        base.Enter(parent);
    }

    public override void Exit()
    {
        //implementation not needed
    }

    public override void Update()
    {
        //implementation not needed
    }

    protected override void SetGFXDirection()
    {
        //implementation not needed
    }

    protected override void SetGFXState()
    {
        parent.GFX.MyState = GFXStates.IDLING;
    }
}

