using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private Projectile bullet = null;

    public Projectile SpawnProjectile(Vector3 position, Vector3 rotation) {

        Projectile projectile = Instantiate(bullet, position, Quaternion.Euler(rotation)) as Projectile;
        projectile.InitializeVelocity(rotation);
        return projectile;
    }
}
