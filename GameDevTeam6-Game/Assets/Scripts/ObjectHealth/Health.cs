using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Use ITargetable interface instead. It's generally better software development practice
 * to use the interface when you are specifying interactions between two objects
 * because it allows for easier debugging, more clarity and easier custom behaviours
 * The interface specifies the behaviours that the objects should have, but it
 * lets the objects themselves decide how they want to be implemented.
 * For example, an enemy might do something different than a player when it's health reaches zero,
 * It's easier and better for the long run to let the enemy and player decide how they want
 * to implement that behaviour. With this script, there is no easy way for you to do that 
 * - Clarence
 */
public class Health : MonoBehaviour
{
    public int healthBar;

    // Start is called before the first frame update
    private void Start()
    {
        throw new System.Exception("use ITargetable interface instead, read the comment at the top of this script definition to see why - Clarence");
    }

    //make sure you call this when initializing targetable object
    public void SetHealth(int health)
    {
        healthBar = health;
    }

    public void RemoveHealth(int amount) {
        healthBar -= amount;
        //Debug.Log(amount + " health removed");
        //Debug.Log(healthBar);

        if (healthBar <= 0)
        {
            Destroy(gameObject);
        }
        //Debug.Log("Health remaining: " + healthBar);
    }

    public void GainHealth(int amount) {
        healthBar += amount;
    }
}
