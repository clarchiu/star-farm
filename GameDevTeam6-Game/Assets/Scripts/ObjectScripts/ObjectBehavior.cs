using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehavior : MonoBehaviour, ITargetable
{
    [SerializeField] private int maxHealth;
    private int health;
    private HealthBar_ healthBar;

    void Start()
    {
        healthBar = gameObject.AddComponent<HealthBar_>();
        //GetComponent<ITargetable>().SetHealth(maxHealth);
        health = maxHealth;
    }

    //general scripts for all objects placed on map
    void ITargetable.SetHealth(int amount)
    {
        health = amount;
        healthBar.UpdateHealthBar((float)health / maxHealth);
    }

    void ITargetable.RemoveHealth(GameObject source, int amount)
    {
        Debug.Log(health);
        Debug.Log("amount" + amount);
        health -= amount;
        Debug.Log(health);

        if (health <= 0) {
            PlaceObjects.Instance.DestroyObject(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        }
        healthBar.UpdateHealthBar((float)health / maxHealth);
    }

    void ITargetable.GainHealth(int amount)
    {
        health += amount;
        healthBar.UpdateHealthBar((float)health / maxHealth);
    }

    //you can leave the implementation blank if your object doesn't need to 
    void ITargetable.KnockBack(Vector2 origin, float amount)
    {
        //Doen't need
        return;
    }

}
