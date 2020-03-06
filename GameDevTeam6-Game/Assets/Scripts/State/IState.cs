using System;

public interface IState
{
    void Enter(EnemyAI parent);

    void Update();

    void Exit();
}
