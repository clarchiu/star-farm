using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int healthBar;
    
    // Start is called before the first frame update
    public void OnEnable()
    {
        healthBar = 100;
    }

    // Update is called once per frame
    // public void Update()
    // {
        
    // }

    public void removeHealth(int amount) {
        healthBar -= amount;
        Debug.Log("Health remaining: " + healthBar);
    }

    public void gainHealth(int amount) {
        healthBar += amount;
    }
}
