using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enemy will eventually be an abstract class, need a targetable interface
public abstract class Enemy : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D myRigidBody;
    private Health myHealth;

    private IState currentState;

    private Vector2 direction;
    public Vector2 Direction { get => direction; set => direction = value; }

    private GameObject target;
    public GameObject Target { get => target; set => target = value; }

    protected abstract string PreferredTarget { get; }
    public abstract float Speed { get; }
    protected abstract int BaseHealth { get; }
    protected abstract int Damage { get; }

    public bool IsMoving { get => direction.x != 0 || direction.y != 0; }

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myHealth = GetComponent<Health>();
        myHealth.SetHealth(BaseHealth);
    }

    protected void Awake()
    {
        ChangeState(new IdleState());
    }

    protected virtual void Update()
    {
        currentState.Update();
        HandleLayers();
    }

    public void Move() //FixedUpdate is frame rate independent
    {
        myRigidBody.velocity = direction.normalized * Speed;
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

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }

}
