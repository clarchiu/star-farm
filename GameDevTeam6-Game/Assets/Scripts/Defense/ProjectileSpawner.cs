using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private Projectile bullet = null;

    public Projectile SpawnProjectile(Vector3 position, Quaternion rotation) {

        Projectile projectile = Instantiate(bullet, position, rotation) as Projectile;
        return projectile;
    }
}
