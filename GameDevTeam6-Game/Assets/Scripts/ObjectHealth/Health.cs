using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Use ITargetable interface instead. It's generally better software development practice
 * to use the interface pattern when you are specifying interactions between two objects
 * because it allows for easier debugging, more clarity and flexibility.
 * The interface specifies the behaviours that the objects should have, but it
 * lets the objects themselves decide how they want to be implemented.
 * For example, an enemy might do something different than a player when it's health reaches zero,
 * It's easier and better in the long run to let the enemy and player decide how they want
 * to implement that behaviour. It'll easier to achieve that by using an interface
 * - Clarence
 */
public class Health : MonoBehaviour
{
    public int healthBar;

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log(this.transform.gameObject.name);
        gameObject.SetActive(false);
        throw new System.Exception("Health is deprecated. Use ITargetable interface instead, read the comment at the top of this script definition to see why - Clarence");
    }

    //make sure you call this when initializing targetable object
    public void SetHealth(int health)
    {
        healthBar = health;
    }

    public void RemoveHealth(int amount)
    {
        healthBar -= amount;
        //Debug.Log(amount + " health removed");
        //Debug.Log(healthBar);

        if (healthBar <= 0)
        {
            Destroy(gameObject);
        }
        //Debug.Log("Health remaining: " + healthBar);
    }

    public void GainHealth(int amount)
    {
        healthBar += amount;
    }
}