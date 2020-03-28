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
    #region Components
    //GFX
    private EnemyGFX gfx;
    public EnemyGFX GFX { get => gfx; }

    //Pathing
    public AIPath aiPath;
    private AIDestinationSetter destSetter;

    //Physics
    private Rigidbody2D rb;
    public Rigidbody2D RB { get => rb; }

    //Combat
    private HealthBar_ healthBar;
    private CircleCollider2D attackRangeCollider;
    #endregion

    #region Public Properties
    //Constants for enemy type
    public EnemyAttributes MyAttributes;

    //Set by attackRangeCollider
    public bool InAttackRange { get; private set; } = false;

    //Set target to null if not in aggro range
    public bool TargetInAggroRange
    {
        get
        {   //calculates whether target is within 5 units of enemy
            return (target.transform.position - transform.position).sqrMagnitude <= 5 * 5;
        }
    }

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
    #endregion

    #region Private Fields
    private IState currentState;
    #endregion

    public static List<EnemyAI> enemies = new List<EnemyAI>();

    protected void Awake()
    {
        enemies.Add(this);
        SetupComponents();
    }

    protected virtual void Start()
    {
        ChangeState(new SearchState());
    }

    protected virtual void Update()
    {
        currentState.Update();
    }

    #region Public Methods
    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter(this);
    }
    #endregion

    #region ITargetable Implementation
    void ITargetable.SetHealth(int amount)
    {
        MyAttributes.currentHealth = amount;
    }

    void ITargetable.RemoveHealth(GameObject source, int amount)
    {
        if (MyAttributes.currentHealth - amount > 0)
        {
            MyAttributes.currentHealth -= amount;
            healthBar.UpdateHealthBar((float) MyAttributes.currentHealth / MyAttributes.maxHealth);

            if (!GameObject.ReferenceEquals(target, source))
            {
                if (source.GetComponent<ITargetable>() != null)
                {
                    Target = source;
                }
            }
        }
        else
        {
            MyAttributes.currentHealth = 0;
            enemies.Remove(this);
            Destroy(gameObject);
        }
    }

    void ITargetable.GainHealth(int amount)
    {
        MyAttributes.currentHealth += amount;
    }

    void ITargetable.KnockBack(Vector2 origin, float amount)
    {
        Vector2 deltaPosition = ((Vector2) this.transform.position - origin).normalized * amount;

        if (aiPath.canMove == true)
        {
            aiPath.Move(deltaPosition);
        }
        else
        {
            rb.AddForce(deltaPosition * amount * 1000f, ForceMode2D.Force);
        }
    }
    #endregion

    #region Private Methods
    private void SetupComponents()
    {
        healthBar = GetComponent<HealthBar_>();
        destSetter = GetComponent<AIDestinationSetter>();
        rb = GetComponent<Rigidbody2D>();
        gfx = GetComponentInChildren<EnemyGFX>();
        attackRangeCollider = GetComponent<CircleCollider2D>();

        if (healthBar == null || destSetter == null || rb == null
            || gfx == null || aiPath == null || attackRangeCollider == null)
        {
            this.gameObject.SetActive(false);
            throw new System.Exception("missing components on enemy, add in inspector");
        }

        if (MyAttributes.currentHealth == 0 && MyAttributes.maxHealth == 0 && MyAttributes.speed == 0
            && MyAttributes.attackCoolDown == 0 && MyAttributes.attackDamage == 0 && MyAttributes.attackRange == 0)
        {
            this.gameObject.SetActive(false);
            throw new System.Exception("missing MyStats values on EnemyAI script in inspector");
        }

        aiPath.maxSpeed = MyAttributes.speed;
        attackRangeCollider.radius = MyAttributes.attackRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameObject.ReferenceEquals(target, collision.gameObject))
        {
            InAttackRange = true;
            //Debug.Log("enemy in range");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameObject.ReferenceEquals(target, collision.gameObject))
        {
            InAttackRange = false;
            //Debug.Log("enemy out of range");
        }
    }
    #endregion
}