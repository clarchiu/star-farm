using System;
using System.Collections.Generic;
using UnityEngine;

public class PathState : IState
{
    private Enemy parent;
    private Stack<Vector3> path;
    private Vector3 destination;
    private Vector3 current;
    private Vector3 goal;
    private Transform transform;

    public void Enter(Enemy parent)
    {
        this.parent = parent;
        this.transform = parent.transform;
    }

    public void Exit()
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        throw new NotImplementedException();
    }
}
