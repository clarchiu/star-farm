using System;
using UnityEngine;
public class HealthBar_: MonoBehaviour
{
    private GameObject Bar;
    private Transform hpBar;
    private bool setup = false;

    // Start is called before the first frame update
    void Setup()
    {
        Bar = ResourceManager.Instance.healthBar;
        GameObject healthBar = Instantiate(Bar, transform);
        hpBar = healthBar.transform.GetChild(0);
        hpBar.transform.localScale = new Vector3(1, hpBar.transform.localScale.y, hpBar.transform.localScale.z);
        setup = true;
    }

    public void UpdateHealthBar(float percentage)
    {
        if (setup == false)
        {
            Setup();
        }
        if (percentage >= 0)
        {
            hpBar.transform.localScale = new Vector3(percentage, hpBar.transform.localScale.y, hpBar.transform.localScale.z);
        }
    }
}
