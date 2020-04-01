using System;
using UnityEngine;

public abstract class EnemyState : IState
{
    protected EnemyAI enemy;
    protected bool hasExited;

    public virtual void Enter(EnemyAI enemy)
    {
        this.enemy = enemy;
        this.hasExited = false;

        SetGFXState();
    }

    public virtual void Exit()
    {
        hasExited = true;
    }

    public virtual void Update()
    {
        if (hasExited) { return; };
    }

    protected abstract void SetGFXState();

    protected abstract void SetGFXDirection();
}
