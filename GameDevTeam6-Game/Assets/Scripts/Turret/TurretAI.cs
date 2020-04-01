using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour, ITargetable
{
    private Vector3 projectileOrigin;
    public GameObject pfTurretProjectile;

    public int maxRange;
    public float timeBetweenAttacks;
    public float attackCoolDown;
    public int damage;
    public int maxHealth;
    private int currentHealth;

    private void Awake()
    { 
        projectileOrigin = transform.Find("ProjectileOrigin").position;

        if (maxRange == 0 || timeBetweenAttacks == 0 || damage == 0 || maxHealth == 0)
        {
            maxRange = 7;
            timeBetweenAttacks = 1f;
            attackCoolDown = timeBetweenAttacks;
            damage = 10;
            maxHealth = 100;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        Bounds bounds = GetComponent<Collider2D>().bounds;
        AstarPath.active.UpdateGraphs(bounds);
    }

    // Update is called once per frame
    void Update()
    {
        attackCoolDown -= Time.deltaTime;

        if (attackCoolDown <= 0f)
        {
            attackCoolDown = timeBetweenAttacks;

            EnemyAI target = GetClosestEnemy();

            if (target != null)
            {
                TurretProjectile.CreateProjectile(pfTurretProjectile.transform, projectileOrigin, target, damage, GetComponent<Collider2D>());
            }
        }

    }

    private EnemyAI GetClosestEnemy()
    {
        EnemyAI closest = null;
        foreach (EnemyAI enemy in EnemyAI.enemies)
        {
            if(Vector3.Distance(transform.position, enemy.transform.position) <= maxRange)
            {
                if (closest == null) { closest = enemy; }
                else
                {
                    float distanceToClosest = Vector3.Distance(transform.position, closest.transform.position);
                    float distanceToNew = Vector3.Distance(transform.position, enemy.transform.position);
                    if ( distanceToClosest > distanceToNew)
                    {
                        closest = enemy;
                    }
                }
            }
        }
        return closest;
    }

    #region ITargetable Implementation
    void ITargetable.SetHealth(int amount)
    {
        currentHealth = amount;
    }

    void ITargetable.RemoveHealth(GameObject source, int amount)
    {
        if (currentHealth - amount > 0)
        {
            currentHealth -= amount;
            HealthBar_ healthBar = GetComponent<HealthBar_>();
            healthBar.UpdateHealthBar((float)currentHealth / maxHealth);
        }
        else
        {
            currentHealth = 0;
            Destroy(gameObject);
        }
    }

    void ITargetable.GainHealth(int amount)
    {
        currentHealth += amount;
    }

    void ITargetable.GetKnockedBack(Vector2 origin, float amount)
    {
        //should not be implemented
    }
    #endregion
}
