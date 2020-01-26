using System;
using System.Collections.Generic;
using UnityEngine;

public class PathState : IState
{
    private Enemy parent;
    private Vector3 destination;
    private Vector3 current;
    private Vector3 goal;
    private Transform transform;

    public void Enter(Enemy parent)
    {
        Debug.Log("enemy in path state");
        this.parent = parent;
        this.transform = parent.transform;
        this.goal = parent.Target.transform.position;

        parent.MyPath = parent.MyAstar.FindPath(parent.transform.position, goal);
        this.current = transform.position;
        this.destination = parent.MyPath.Pop();
    }


    public void Exit()
    {
        parent.MyPath = null;
    }

    public void Update()
    {
        if (parent.MyPath != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, parent.Speed * Time.deltaTime);

            parent.Direction = (destination - transform.position).normalized;

            float distance = Vector2.Distance(destination, transform.position);
            float totalDistance = Vector2.Distance(parent.Target.transform.position, transform.position);

            if (totalDistance <= parent.AttackRange)
            {
                parent.ChangeState(new AttackState());
            }

            if (distance <= 0)
            {
                if (parent.MyPath.Count > 0)
                {
                    destination = parent.MyPath.Pop();
                }
                else
                {
                    parent.MyPath = null;
                    parent.ChangeState(new IdleState());
                }
            }
        }

        

    }
}
