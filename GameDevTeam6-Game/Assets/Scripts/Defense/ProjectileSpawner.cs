using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] bool shootProjectiles = false;
    private bool shooting = false;
    [SerializeField] float rateOfFire = 0.5f;
    private Coroutine shootCoroutine = null;
    [SerializeField] private Projectile bullet = null;

    void Update(){
        if (shootProjectiles && !shooting) {
            shootCoroutine = StartCoroutine(ShootCoroutine());
        }
        if (!shootProjectiles) {
            StopAllCoroutines();
            shooting = false;
        }
    }

    IEnumerator ShootCoroutine() {
        shooting = true;
        while (shootProjectiles) {
            SpawnProjectile(gameObject.transform.position, gameObject.transform.eulerAngles);
            yield return new WaitForSeconds(rateOfFire);
        }
        shooting = false;
    }

    public Projectile SpawnProjectile(Vector3 position, Vector3 rotation) {

        Projectile projectile = Instantiate(bullet, position, Quaternion.Euler(rotation)) as Projectile;
        projectile.InitializeVelocity(rotation);
        return projectile;
    }
}
