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

[RequireComponent(typeof(EnemyGFX))]
[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(AIDestinationSetter))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI: MonoBehaviour
{
    #region Fields
    public static List<EnemyAI> enemies = new List<EnemyAI>();

    public EnemyGFX gfx;
    public AIPath aiPath;   //public because I need to set this in inspector
    public AIDestinationSetter destSetter;
    public Rigidbody2D rb;

    private bool canMove;
    public bool CanMove
    {
        get
        {
            if (currentState is PathState)
            {
                return aiPath.canMove;
            }
            return canMove;
        }
        set
        {
            if (currentState is PathState)
            {
                aiPath.canMove = value;
                aiPath.canSearch = value;
                canMove = !aiPath.canMove;
            } else
            {
                canMove = value;
                aiPath.canMove = false; 
                aiPath.canSearch = false;
            }
        }
    }

    private IState currentState;

    public EnemyAttributes MyAttributes;    //Constants for enemy type

    public bool IsTargetInAttackRange { get; set; } //Set by attackRangeCollider
    public bool IsTargetInAggroRange  //Set target to null if not in aggro range
    {
        get
        {   //calculates whether target is within 5 units of enemy
            return (target.transform.position - transform.position).sqrMagnitude <= 5 * 5;
        }
    }

    public delegate void OnNewTargetDelegate();
    public event OnNewTargetDelegate OnNewTarget;

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

            if (target)
            {
                destSetter.target = this.target.transform;
                OnNewTarget();
            }
        }
    }
    #endregion

    protected virtual void SetUp()
    {
        if (!gfx || !aiPath || !destSetter|| !rb)
        {
            this.gameObject.SetActive(false);
            throw new System.Exception("missing components on EnemyAI script in inspector");
        }

        if (MyAttributes.currentHealth == 0 && MyAttributes.maxHealth == 0 && MyAttributes.speed == 0
            && MyAttributes.attackCoolDown == 0 && MyAttributes.attackDamage == 0 && MyAttributes.attackRange == 0)
        {
            this.gameObject.SetActive(false);
            throw new System.Exception("missing MyStats values on EnemyAI script in inspector");
        }

        CanMove = true;
        IsTargetInAttackRange = false;
        aiPath.maxSpeed = MyAttributes.speed;
    }

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
        transform.GetComponentInChildren<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(-transform.position.y);
    }
    #endregion

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

    public virtual void PrimaryAttack(ITargetable target)
    {
        target.RemoveHealth(gameObject, MyAttributes.attackDamage);
        //target.GetKnockedBack(transform.position, 0.5f); //TODO: make amount of knockback scale with damage?
    }

    public virtual void SecondaryAttack(ITargetable target)
    {
        PrimaryAttack(target);// no secondary attack
    }

    public virtual IEnumerator GetKnockedBack(Vector2 origin, float amount)
    {
        //Debug.Log("knockback");
        Vector2 deltaPosition = ((Vector2)this.transform.position - origin).normalized * amount;

        CanMove = false;

        rb.velocity = Vector2.zero;
        rb.AddForce(deltaPosition, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);

        CanMove = true;
    }
    #endregion 
}