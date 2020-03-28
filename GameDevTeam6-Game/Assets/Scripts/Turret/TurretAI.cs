using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    private Vector3 projectileOrigin;
    public GameObject pfTurretProjectile;

    public int maxRange;
    public float timeBetweenAttacks;
    public float attackCoolDown;
    public int damage;

    private void Awake()
    { 
        projectileOrigin = transform.Find("ProjectileOrigin").position;

        if (maxRange == 0 || timeBetweenAttacks == 0 || damage == 0)
        {
            maxRange = 5;
            timeBetweenAttacks = 0.75f;
            attackCoolDown = timeBetweenAttacks;
            damage = 10;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // As an example, use the bounding box from the attached collider
        //Bounds bounds = GetComponent<BoxCollider2D>().bounds;
        //AstarPath.active.UpdateGraphs(bounds);
    }

    // Update is called once per frame
    void Update()
    {
        attackCoolDown -= Time.deltaTime;

        if (attackCoolDown <= 0f)
        {
            attackCoolDown = timeBetweenAttacks;

            EnemyAI enemy = GetClosestEnemy();
            if (enemy != null)
            {
                TurretProjectile.CreateProjectile(pfTurretProjectile.transform, projectileOrigin, enemy, damage);
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
}
