using UnityEngine;
using System.Collections;

public class IdleState: IState
{
    private Enemy parent;

    public void Enter(Enemy parent)
    {
        this.parent = parent;
        Debug.Log("enemy in idle state");
    }

    public void Exit()
    {
    }

    public void Update()
    {
        if (parent.Target != null)
        {
            Debug.Log("target found in Idle");
            parent.ChangeState(new PathState());
        }
    }
}
