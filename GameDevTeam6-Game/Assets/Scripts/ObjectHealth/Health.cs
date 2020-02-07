using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int healthBar;

    // Start is called before the first frame update
    private void Start()
    {
    }

    //make sure you call this when initializing targetable object
    public void SetHealth(int health)
    {
        healthBar = health;
    }

    public void RemoveHealth(int amount) {
        healthBar -= amount;
        Debug.Log(amount + " health removed");
        Debug.Log(healthBar);

        if (healthBar <= 0)
        {
            Destroy(gameObject);
        }
        Debug.Log("Health remaining: " + healthBar);

    }

    public void GainHealth(int amount) {
        healthBar += amount;
    }
}
