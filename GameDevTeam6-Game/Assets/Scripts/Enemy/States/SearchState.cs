﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Search state responsible for finding a target for the agent
 * Search state can go to path state if a target is found or idle if there are no more targets 
 * - Clarence 
 */

public class SearchState: EnemyState
{
    private float searchCoolDown; 

    public override void Enter(EnemyAI enemy)
    {
        Debug.Log("enemy in search state");

        base.Enter(enemy);

        searchCoolDown = 2f;
    }

    public override void Exit()
    {
        base.Exit();//implementation not needed
    }

    public override void Update()
    {
        base.Update();

        if (!enemy.Target && searchCoolDown <= 0)
        {
            GameObject target = FindClosestTarget();

            if (target != null)
            {
                //Debug.Log("target found");

                enemy.Target = target;
                enemy.ChangeState(new PathState());
                return;
            }
            else
            {
                //set cooldown to 5 if target can't be found the first time
                //to avoid unnecessary computation when there are no targets available
                searchCoolDown = 5f; 
            }
        }

        searchCoolDown -= Time.deltaTime;   //enemy can only search when searchCoolDown == 0
    }

    private GameObject FindPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player");
    }

    //TODO needs to be different for different enemies
    //ToDO change this to coroutine
    private GameObject FindClosestTarget()  
    {
        List<GameObject> gameObjs = new List<GameObject>(GameObject.FindGameObjectsWithTag("Plant"));
        gameObjs.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Structure")));
        gameObjs.Add(GameObject.FindGameObjectWithTag("Player"));
        gameObjs.Add(GameObject.FindGameObjectWithTag("Ship"));
        gameObjs.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Wall")));

        GameObject closestObj = null;

        float closestDis = Mathf.Infinity;
        Vector3 selfPos = enemy.transform.position;

        foreach (GameObject obj in gameObjs)
        {
            Vector3 diff = obj.transform.position - selfPos;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < closestDis && obj.GetComponent<ITargetable>() != null)
            {
                closestObj = obj;
                closestDis = curDistance;
            }
        }

        //Debug.Log(closestObj.tag);
        return closestObj;
    }

    protected override void SetGFXState()
    {
        enemy.gfx.MyState = GFXStates.IDLING;
    }

    protected override void SetGFXDirection()
    {
        //implementation not needed
    }
}
