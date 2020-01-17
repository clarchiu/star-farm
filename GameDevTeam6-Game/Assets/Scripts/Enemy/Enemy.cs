using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enemy will eventually be an abstract class, need a targetable interface
public abstract class Enemy : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D myRigidBody;

    protected Vector2 direction;
    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    protected abstract string PreferredTarget { get; }
    protected abstract float Speed { get; }
    protected abstract int Health { get; set; }
    protected abstract int Damage { get; }

    private GameObject target;
    public GameObject Target { get => target; set => target = value; }

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        HandleLayers();
    }

    protected abstract void Attack(GameObject target);

    //should be part of targetable interface
    //there should be some kind of targetable interface
    public void TakeDamage(int damage) //interface methods
    {
        Health -= damage;
    }

    public void Move() //FixedUpdate is frame rate independent
    {
        myRigidBody.velocity = direction.normalized * Speed;
    }

    public void HandleLayers()
    {
        if (IsMoving)
        {
            ActivateLayer("Walk Layer");
            animator.SetFloat("x", direction.x);
            animator.SetFloat("y", direction.y);
        }
        else
        {
            ActivateLayer("Idle Layer");
        }
    }

    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }

        animator.SetLayerWeight(animator.GetLayerIndex(layerName), 1);

    }


    protected void FollowTarget()
    {
        if (target != null)
        {
            direction = (target.transform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position,
                target.transform.position, Speed * Time.deltaTime);
        }
        else
        {
            direction = Vector2.zero;
        }
        //else
        //{
        //    Vector2 origin = new Vector2(27f, 27f);
        //    direction = (origin - (Vector2)transform.position).normalized;
        //    transform.position = Vector2.MoveTowards(transform.position,
        //        origin, Speed * Time.deltaTime);
        //}
    }

    protected GameObject FindClosestTarget()
    {
        GameObject[] gameObjs;
        gameObjs = GameObject.FindGameObjectsWithTag(PreferredTarget);

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
