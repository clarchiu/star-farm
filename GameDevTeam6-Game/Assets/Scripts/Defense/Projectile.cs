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
    private float currentTime = 0;

    void Awake(){
        FindRigidBody();
        player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>(), true);
    }

    private void FindRigidBody(){
        rigidBody = GetComponent<Rigidbody2D>();
        if (!rigidBody) {
            Debug.LogWarning("No Rigidbody2D Component found on Projectile!");
        }
    }
    public void InitializeVelocity(Vector3 rotation)
    {
        float angle = Mathf.Deg2Rad *( rotation.z + 90);
        float xVelocity = projectileSpeed * Mathf.Cos(angle);
        float yVelocity = projectileSpeed * Mathf.Sin(angle);
        rigidBody.velocity = new Vector2(xVelocity, yVelocity);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > lifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    void DestroyProjectile(){
        Destroy(gameObject);
        //TODO
        //Add Object Pooling to Avoid Memory Issues
    }
}
