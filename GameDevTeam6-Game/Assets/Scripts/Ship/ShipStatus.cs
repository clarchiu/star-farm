using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStatus : MonoBehaviour, ITargetable
{
    public int MaxHealth;
    public int health;
    public HealthBar_ healthBar;

    void ITargetable.GainHealth(int amount)
    {
        health += amount;
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
            //Destroy(gameObject);
        }
    }

    void ITargetable.SetHealth(int amount)
    {
        health = amount;
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
