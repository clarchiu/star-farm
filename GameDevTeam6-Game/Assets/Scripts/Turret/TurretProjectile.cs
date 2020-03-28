using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    private EnemyAI enemy;
    private int damage;

    public static void CreateProjectile(Transform pfTurretProjectile, Vector3 spawnPos, EnemyAI enemy, int damage)
    {

        Transform projectileTransform = Instantiate(pfTurretProjectile, spawnPos, Quaternion.identity);

        TurretProjectile turretProjectile = projectileTransform.GetComponent<TurretProjectile>();
        turretProjectile.SetUp(enemy, damage);
    }

    private void SetUp(EnemyAI enemy, int damage)
    {
        this.enemy = enemy;
        this.damage = damage;
    }

    private void Update()
    {
        Vector3 targetPos = this.enemy.transform.position;

        InitializeVelocity(targetPos);

        if (Vector3.Distance(transform.position, targetPos) < 0.5f)
        {
            ITargetable target = this.enemy as ITargetable;
            target.RemoveHealth(gameObject, 10);
            Destroy(gameObject);
        }
    }

    public void InitializeVelocity(Vector3 targetPos)
    {
        Vector3 targetDir = (targetPos - transform.position).normalized;
        float speed = 12f;
        transform.position += targetDir * speed * Time.deltaTime;

        //float speed = 15f;
        //Vector3 targetDir = (enemy.transform.position - transform.position).normalized;
        //Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //rb.velocity = targetDir * speed;
    }
}
