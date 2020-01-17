using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject Bar;
    public Health health;
    int maxHp;
    private Transform hpBar;

    // Start is called before the first frame update
    void Start()
    {

        GameObject healthBar = Instantiate(Bar, transform);
        health = GetComponent<Health>();
        Debug.Log("SPAWNED HEALTHBAR");
        Debug.Log(health.healthBar);
        maxHp = health.healthBar;
        hpBar = healthBar.transform.GetChild(0);
        hpBar.transform.localScale = new Vector3(health.healthBar * (1.0f) / maxHp * (1.0f), hpBar.transform.localScale.y, hpBar.transform.localScale.z);
        Debug.Log(hpBar.transform.localScale.x + " is the scale");
    }


    public void UpdateHealthBar()
    {
        if (health.healthBar >= 0)
        {
            Debug.Log("Updating HealthBar:" + (health.healthBar * (1.0f) / maxHp * (1.0f)));
            hpBar.transform.localScale = new Vector3((health.healthBar * (1.0f) / maxHp * (1.0f)), hpBar.transform.localScale.y, hpBar.transform.localScale.z);
            Debug.Log(hpBar.transform.localScale.x + " is the scale");
        }
    }

}