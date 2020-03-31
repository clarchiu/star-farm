using System;
using UnityEngine;

public abstract class EnemyState : IState
{
    protected EnemyAI enemy;
    protected static float attackCoolDown = 0f;

    public virtual void Enter(EnemyAI enemy)
    {
        this.enemy = enemy;

        SetGFXState();
    }

    public abstract void Exit();

    public virtual void Update()
    {
        if (attackCoolDown > 0)
        {
            attackCoolDown -= Time.deltaTime;
        }
    }

    protected abstract void SetGFXState();

    protected abstract void SetGFXDirection();
}
