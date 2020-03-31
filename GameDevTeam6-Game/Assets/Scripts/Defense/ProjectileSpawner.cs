using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] public bool shootProjectiles = false;
    bool shooting = false;
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
            SpawnProjectile(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.2f), GetComponent<PlayerDirection>().GetLookDirection());
            yield return new WaitForSeconds(rateOfFire);
        }
        shooting = false;
    }

    public Projectile SpawnProjectile(Vector3 position, Quaternion rotation) {

        Projectile projectile = Instantiate(bullet, position, rotation) as Projectile;
        projectile.InitializeVelocity(rotation.eulerAngles);
        //SoundEffects_.Instance.PlaySoundEffect(SoundEffect.laserGun);
        return projectile;
    }
}
