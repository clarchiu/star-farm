using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/*
 * This class handles the logical layer of an enemy in order to separate
 * the graphical layer from the logical layer.
 * An enemy can have different behaviours - each behaviour is defined as a state.
 * Each state is responsible for changing to another state
 * 
 * - Clarence
 */

public class EnemyAI: MonoBehaviour, ITargetable
{ 
    public AIPath aiPath;

    private Rigidbody2D rb;
    private AIDestinationSetter destSetter;
    private HealthBar_ healthBar;

    private IState currentState;

    private GameObject target;
    public GameObject Target
    {
        get
        {
            return target;
        }
        set
        {
            target = value;

            if (target != null)
                destSetter.target = this.target.transform;
        }
    }

    private void Awake()
    {
        ChangeState(new SearchState());
    }

    private void Start()
    {
        GetComponents();
    }

    private void Update()
    {
        currentState.Update();
    }

    private void GetComponents()
    {
        healthBar = GetComponent<HealthBar_>();
        destSetter = GetComponent<AIDestinationSetter>();
        rb = GetComponent<Rigidbody2D>();
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

    public void SetDirection(Vector2 direction)
    {
        GetComponentInChildren<EnemyGFX>().Direction = direction;
    }

    //TODO: set this somewhere else
    public int MaxHealth;
    public int health;

    void ITargetable.SetHealth(int amount)
    {
        health = amount;
    }

    void ITargetable.RemoveHealth(int amount)
    {
        if (health - amount > 0)
        {
            health -= amount;
            healthBar.UpdateHealthBar((float)health / MaxHealth);
        } else
        {
            Destroy(gameObject);
        }
    }

    void ITargetable.GainHealth(int amount)
    {
        health += amount;
    }

    void ITargetable.KnockBack(Vector3 origin, float amount)
    {
        Vector3 deltaPosition = (this.transform.position - origin).normalized * amount;

        if (aiPath.canMove == true)
        {
            aiPath.Move(deltaPosition);
        } else
        {
            rb.AddForce(deltaPosition * amount * 1000f, ForceMode2D.Force);
        }
    }
}