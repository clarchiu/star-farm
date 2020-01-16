using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enemy will eventually be an abstract class, need a targetable interface
public abstract class Enemy : MonoBehaviour 
{
    protected float speed; //temporary
    protected Vector2 direction;

    protected string preferredTarget;

    protected int health;
    protected int damage;

    protected GameObject target;
    public GameObject Target { get => target; set => target = value; }

    protected abstract void Attack(GameObject target);
    protected abstract void InitializeProperties();

    //should be part of targetable interface
    //there should be some kind of targetable interface
    public void TakeDamage(int damage) //interface methods
    {
        health -= damage;
    }

    public void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
  

    protected void FollowTarget()
    {
        if(target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                target.transform.position, speed * Time.deltaTime);
        } else
        {
            transform.position = Vector2.MoveTowards(transform.position,
                new Vector2(0f, 0f), speed * Time.deltaTime);
        }
    }

    protected GameObject FindClosestTarget()
    {
        GameObject[] gameObjs;
        gameObjs = GameObject.FindGameObjectsWithTag(preferredTarget);

        GameObject closestObj = null;
        float closestDis = Mathf.Infinity;
        Vector3 selfPos = transform.position;
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
