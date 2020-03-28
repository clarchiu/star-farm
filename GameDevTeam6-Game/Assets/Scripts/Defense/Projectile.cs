using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Reference Parameters
    private Rigidbody2D rigidBody = null;
    //Config Params
    [SerializeField] private float projectileSpeed = 10f;
    private GameObject player;

    private float lifeTime = 2;
    private Health health;
    private HealthBar hpBar;

    [SerializeField] private GameObject invisibleCollider;


    void Awake(){
        FindRigidBody();
        player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>(), true);
        //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), invisibleCollider.GetComponent<BoxCollider2D>(), true);
    }

    private void FindRigidBody(){
        rigidBody = GetComponent<Rigidbody2D>();
        if (!rigidBody) {
            Debug.LogWarning("No Rigidbody2D Component found on Projectile!");
        }
    }
    public void InitializeVelocity(Vector3 rotation)
    {
        float angle = Mathf.Deg2Rad *( rotation.z);
        float xVelocity = projectileSpeed * Mathf.Cos(angle);
        float yVelocity = projectileSpeed * Mathf.Sin(angle);
        rigidBody.velocity = new Vector2(xVelocity, yVelocity);
    }
    /*
    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > lifeTime)
        {
            Destroy(this.gameObject);
        }
    }
    */
    void DestroyProjectile(){
        Destroy(gameObject);
        //TODO
        //Add Object Pooling to Avoid Memory Issues
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.CompareTag("Enemy"))
        {
            /* use ITargetable interface instead b/c
             * more verbose, easier to read/debug and
             * easier to implement custom logic for when enemy dies
             * - Clarence
             */
            ITargetable targetable = col.gameObject.GetComponent<ITargetable>();
            targetable.RemoveHealth(player,PlayerUpgrades.Instance.rangedAttackDamage);

            //health = col.gameObject.GetComponent<Health>();
            //health.RemoveHealth(10);
            //hpBar = col.gameObject.GetComponent<HealthBar>();
            //hpBar.UpdateHealthBar();
        }

        Destroy(this.gameObject);

    }
}
