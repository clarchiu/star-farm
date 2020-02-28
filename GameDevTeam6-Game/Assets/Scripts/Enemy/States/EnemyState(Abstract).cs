using System;
using UnityEngine;

public abstract class EnemyState : IState
{
    protected EnemyAI parent;

    public virtual void Enter(EnemyAI parent)
    {
        this.parent = parent;

        SetGFXState();
    }

    public abstract void Exit();

    public abstract void Update();

    protected abstract void SetGFXState();

    protected abstract void SetGFXDirection();
}
