using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* use HealthBar_ instead, they're basically the same but the other one is a little bit
 * easier to read and is compatible with ITargetable interface -Clarence
 */
public class HealthBar : MonoBehaviour
{
    public GameObject Bar;
    private Transform hpBar;
    public Health health;
    int maxHp;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        throw new System.Exception("use HealthBar_ instead, read the comment at the top of this script to see why - Clarence");

        GameObject healthBar = Instantiate(Bar, transform);
        health = GetComponent<Health>();
        maxHp = health.healthBar;
        hpBar = healthBar.transform.GetChild(0);
        hpBar.transform.localScale = new Vector3(health.healthBar * (1.0f) / maxHp * (1.0f), hpBar.transform.localScale.y, hpBar.transform.localScale.z);
    }


    public void UpdateHealthBar()
    {
        if (health.healthBar >= 0)
        {
            hpBar.transform.localScale = new Vector3((health.healthBar * (1.0f) / maxHp * (1.0f)), hpBar.transform.localScale.y, hpBar.transform.localScale.z);
        }
    }

}