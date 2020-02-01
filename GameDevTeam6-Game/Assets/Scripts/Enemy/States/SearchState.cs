using System;
using System.Collections.Generic;
using UnityEngine;


/*
 * Search state responsible for finding a target for the agent
 * Search state can go to path state if a target is found or idle if there are no more targets 
 * - Clarence 
 */

internal class SearchState: IState
{
    private EnemyAI parent;

    public void Enter(EnemyAI parent)
    {
        this.parent = parent;
        Debug.Log("enemy in search state");
    }

    public void Exit()
    {

    }

    public void Update()
    {
        if (parent.Target == null)
        {
            GameObject target = FindPlayer();

            if (target != null)
            {
                Debug.Log("target found");
                parent.Target = target;
                parent.ChangeState(new PathState());
            } else
            {
                parent.ChangeState(new IdleState());
            }
        }
        
    }

    private GameObject FindPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player");
    }

    //TODO: needs to be different for different subclasses
    private GameObject FindClosestTarget()
    {
        GameObject[] gameObjs;
        gameObjs = GameObject.FindGameObjectsWithTag("Targetable");

        GameObject closestObj = null;
        float closestDis = Mathf.Infinity;
        Vector3 selfPos = parent.transform.position;
        foreach (GameObject obj in gameObjs)
        {
            Vector3 diff = obj.transform.position - selfPos;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < closestDis)
            {
                closestObj = obj;
                closestDis = curDistance;
            }
        }

        return closestObj;
    }
}
