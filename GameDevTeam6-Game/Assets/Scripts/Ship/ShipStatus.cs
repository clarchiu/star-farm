using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStatus : MonoBehaviour, ITargetable
{
    public int MaxHealth;
    public int health;
    private HealthBar_ healthBar;

    private void Awake()
    {
        healthBar = gameObject.AddComponent<HealthBar_>();
    }
    void ITargetable.GainHealth(int amount)
    {
        health += amount;
        healthBar.UpdateHealthBar(health / MaxHealth);
    }

    void ITargetable.KnockBack(Vector2 origin, float amount)
    {
        //implementation not necessary
    }

    void ITargetable.RemoveHealth(GameObject source, int amount)
    {
        if (health - amount > 0)
        {
            health -= amount;
            healthBar.UpdateHealthBar((float)health / MaxHealth);
        }
        else
        {
            health = 0;
            Debug.Log("ship destroyed");
            Destroy(gameObject);
        }
        healthBar.UpdateHealthBar(health / MaxHealth);
    }

    void ITargetable.SetHealth(int amount)
    {
        health = amount;
        healthBar.UpdateHealthBar(health / MaxHealth);
    }

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<HealthBar_>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
