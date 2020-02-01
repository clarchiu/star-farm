using System;
using UnityEngine;
public class HealthBar_: MonoBehaviour
{
    public GameObject Bar;
    private Transform hpBar;

    // Start is called before the first frame update
    void Start()
    {
        GameObject healthBar = Instantiate(Bar, transform);
        hpBar = healthBar.transform.GetChild(0);
        hpBar.transform.localScale = new Vector3(1f, hpBar.transform.localScale.y, hpBar.transform.localScale.z);
    }

    public void UpdateHealthBar(float percentage)
    {
        if (percentage >= 0)
        {
            hpBar.transform.localScale = new Vector3(percentage, hpBar.transform.localScale.y, hpBar.transform.localScale.z);
        }
    }
}
