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

public class EnemyAI: MonoBehaviour, ITargetable, ICombative
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
    #endregion

    #region Public Properties
    public static List<EnemyAI> enemies = new List<EnemyAI>();

    public EnemyAttributes MyAttributes;    //Constants for enemy type

    public bool IsTargetInAttackRange { get; private set; } //Set by attackRangeCollider
    public bool IsTargetInAggroRange  //Set target to null if not in aggro range
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
            {
                destSetter.target = this.target.transform;
                OnNewTarget();
            }
        }
    }
    #endregion

    #region Private Fields
    private IState currentState;
    #endregion

    #region Setup Methods
    protected virtual void SetUp()
    {
        SetupComponents();
        SetUpAttributes();
    }

    private void SetupComponents()
    {
        healthBar = GetComponent<HealthBar_>();
        destSetter = GetComponent<AIDestinationSetter>();
        rb = GetComponent<Rigidbody2D>();
        gfx = GetComponentInChildren<EnemyGFX>();

        if (healthBar == null || destSetter == null || rb == null
            || gfx == null || aiPath == null)
        {
            this.gameObject.SetActive(false);
            throw new System.Exception("missing components on enemy, add in inspector");
        }
    }

    protected virtual void SetUpAttributes()
    {
        if (MyAttributes.currentHealth == 0 && MyAttributes.maxHealth == 0 && MyAttributes.speed == 0
            && MyAttributes.attackCoolDown == 0 && MyAttributes.attackDamage == 0 && MyAttributes.attackRange == 0)
        {
            this.gameObject.SetActive(false);
            throw new System.Exception("missing MyStats values on EnemyAI script in inspector");
        }

        IsTargetInAttackRange = false;
        aiPath.maxSpeed = MyAttributes.speed;
    }
    #endregion

    #region Monobehaviour methods
    protected void Awake()
    {
        enemies.Add(this);
        SetUp();
    }

    protected virtual void Start()
    {
        ChangeState(new SearchState());
    }

    protected virtual void Update()
    {
        currentState.Update();
    }
    #endregion

    #region Enemy State Methods
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

    #region ICombative implementation
    public void SetInAttackRange(bool inRange)
    {
        IsTargetInAttackRange = inRange;
    }

    public GameObject GetTarget()
    {
        return target;
    }

    public event OnNewTargetDelegate OnNewTarget;

    public virtual void PrimaryAttack(ITargetable target)
    {
        target.RemoveHealth(gameObject, MyAttributes.attackDamage);
        target.KnockBack(transform.position, 0.5f); //TODO: make amount of knockback scale with damage?
    }

    public virtual void SecondaryAttack(ITargetable target)
    {
        // no secondary attack
    }
    #endregion

    #region ITargetable Implementation
    public virtual void SetHealth(int amount)
    {
        MyAttributes.currentHealth = amount;
    }

    public virtual void RemoveHealth(GameObject source, int amount)
    {
        if (MyAttributes.currentHealth - amount > 0)
        {
            MyAttributes.currentHealth -= amount;
            healthBar.UpdateHealthBar((float)MyAttributes.currentHealth / MyAttributes.maxHealth);

            if (!GameObject.ReferenceEquals(target, source) && source.CompareTag("Player"))
            {
                if (source.GetComponent<ITargetable>() != null)
                {
                    //Debug.Log("changing state");
                    Target = source;
                    ChangeState(new PathState());
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

    public virtual void GainHealth(int amount)
    {
        MyAttributes.currentHealth += amount;
    }

    public virtual void KnockBack(Vector2 origin, float amount)
    {
        Debug.Log("knockback");
        Vector2 deltaPosition = ((Vector2)this.transform.position - origin).normalized * amount;

        if (aiPath.canMove == true)
        {
            Debug.Log("astar knockback");
            aiPath.Move(deltaPosition);
        }
        else
        {
            Debug.Log("physics knockback");
            rb.AddForce(deltaPosition * 50f);
        }
        //StartCoroutine(KnockBackTimeout());
    }

    //public virtual IEnumerator KnockbackTimeOut(float knockbackPwr, Vector2 origin)
    //{
    //    float knockDuration = 2f;
    //    float timer = 0;

    //    if (aiPath.canMove)
    //    {
    //        aiPath.canMove = false;
    //        Debug.Log("aipath cannot move");
    //    }

    //    while (knockDuration > timer)
    //    {
    //        timer += Time.deltaTime;
    //        Vector2 deltaPosition = ((Vector2)this.transform.position - origin).normalized;
    //        rb.AddForce(deltaPosition * knockbackPwr);
    //    }

    //    yield return new WaitForSeconds(2f);

    //    aiPath.canMove = true;
    //    Debug.Log("aipath can move");
    //}
    #endregion
}