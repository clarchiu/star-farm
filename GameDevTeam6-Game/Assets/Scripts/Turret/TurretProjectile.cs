using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    private Collider2D sourceTurret;
    private Vector3 origin;
    private EnemyAI enemy;
    private int damage;

    public static void CreateProjectile(Transform pfTurretProjectile, Vector3 spawnPos, EnemyAI enemy, int damage, Collider2D sourceTurret)
    {

        Transform projectileTransform = Instantiate(pfTurretProjectile, spawnPos, Quaternion.identity);

        TurretProjectile turretProjectile = projectileTransform.GetComponent<TurretProjectile>();
        turretProjectile.SetUp(enemy, damage, sourceTurret, spawnPos);
    }

    private void SetUp(EnemyAI enemy, int damage, Collider2D sourceTurret, Vector3 origin)
    {
        this.enemy = enemy;
        this.damage = damage;
        this.sourceTurret = sourceTurret;
        this.origin = origin;

    }

    private void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), sourceTurret);
    }

    private void Update()
    {
        Vector3 targetPos = this.enemy.transform.position;

        InitializeVelocity(targetPos);

        if (Vector3.Distance(origin, transform.position) > 7)
        {
            Destroy(gameObject);
        }
    }

    public void InitializeVelocity(Vector3 targetPos)
    {
        Vector3 targetDir = (targetPos - transform.position).normalized;
        float speed = 12f;
        transform.position += targetDir * speed * Time.deltaTime;
        //float speed = 10f;
        //Vector3 targetDir = (targetPos - transform.position).normalized;
        //Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //rb.velocity = targetDir * speed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            ITargetable targetable = col.gameObject.GetComponent<ITargetable>();
            targetable.RemoveHealth(gameObject, damage);
            targetable.KnockBack(origin, 0.5f);
        }

        Destroy(this.gameObject);
    }
}
