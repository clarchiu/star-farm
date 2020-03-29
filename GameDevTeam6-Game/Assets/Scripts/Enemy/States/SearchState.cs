using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Search state responsible for finding a target for the agent
 * Search state can go to path state if a target is found or idle if there are no more targets 
 * - Clarence 
 */

internal class SearchState: EnemyState
{
    private float searchCoolDown; 

    public override void Enter(EnemyAI parent)
    {
        //Debug.Log("enemy in search state");

        base.Enter(parent);

        searchCoolDown = 2f;
    }

    public override void Exit()
    {
        //implementation not needed
    }

    public override void Update()
    {
        if (parent.Target == null && searchCoolDown <= 0)
        {
            GameObject target = FindClosestTarget();

            if (target != null)
            {
                //Debug.Log("target found");

                parent.Target = target;
                parent.ChangeState(new PathState());
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

    //TODO: needs to be different for different enemies
    private GameObject FindClosestTarget()
    {
        List<GameObject> gameObjs = new List<GameObject>(GameObject.FindGameObjectsWithTag("Plant"));
        gameObjs.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Structure")));
        gameObjs.Add(GameObject.FindGameObjectWithTag("Player"));
        gameObjs.Add(GameObject.FindGameObjectWithTag("Ship"));

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

        //Debug.Log(closestObj.tag);
        return closestObj;
    }

    protected override void SetGFXState()
    {
        parent.GFX.MyState = GFXStates.IDLING;
    }

    protected override void SetGFXDirection()
    {
        //implementation not needed
    }
}
