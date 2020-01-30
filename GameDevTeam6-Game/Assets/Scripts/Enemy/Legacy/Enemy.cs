using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //public TileLayout Tilemap { get; set; }
    [SerializeField]
    private AStarInscope astar;
    public AStarInscope MyAstar { get => astar; }

    public Stack<Vector3> MyPath;

    private Animator animator;
    public Animator MyAnimator { get => animator; }

    private Rigidbody2D rigidBody;
    public Rigidbody2D MyRigidBody { get => rigidBody; }


    //private IState currentState;

    private Vector2 direction;
    public Vector2 Direction { get => direction; set => direction = value; }

    private GameObject target;
    public GameObject Target { get => target; set => target = value; }

    //properties to handle animation layers
    public bool IsMoving { get => direction.x != 0 || direction.y != 0; }
    public bool IsAttacking { get; set; }

    private Health myHealth;

    //enemy attributes that need to be initialized by subclasses
    protected abstract string PreferredTarget { get; }
    protected abstract int BaseHealth { get; }
    protected abstract int Damage { get; }
    public abstract float Speed { get; }
    public abstract float AttackRange { get; }

    protected virtual void Start()
    {
        //Tilemap = FindObjectOfType<TileLayout>();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        myHealth = GetComponent<Health>();
        myHealth.SetHealth(BaseHealth);
    }

    protected void Awake()
    {
        //ChangeState(new IdleState());
    }

    protected virtual void Update()
    {
        //currentState.Update();
        HandleLayers();
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }


    public void Move() //FixedUpdate is frame rate independent
    {
        if (MyPath == null)
        {
            rigidBody.velocity = direction.normalized * Speed;
        }
    }

    public void HandleLayers()
    {
        if (IsMoving)
        {
            ActivateLayer("Walk Layer");
            animator.SetFloat("x", direction.x);
            animator.SetFloat("y", direction.y);
        }
        else if (IsAttacking)
        {
            //TODO: add attack layer
            ActivateLayer("Attack Layer");
        }
        else
        {
            ActivateLayer("Idle Layer");
        }
    }

    private void ActivateLayer(string layerName)
    {
        for (int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }

        animator.SetLayerWeight(animator.GetLayerIndex(layerName), 1);

    }

    //public void ChangeState(IState newState)
    //{
    //    if (currentState != null)
    //    {
    //        currentState.Exit();
    //    }
    //    currentState = newState;
    //    currentState.Enter(this);
    //}
}
