using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWeapon : MonoBehaviour
{
    private MultiTool multiTool;

    private void Awake()
    {
        multiTool = FindObjectOfType<MultiTool>();
    }
    void Update()
    {
        if (multiTool.GetMode() != ToolModes.combatMode) return;
        else {
            if (Input.GetMouseButtonDown(1))
            {
                GetComponent<ProjectileSpawner>().shootProjectiles = true;
            } else if (Input.GetMouseButtonUp(0))
            {
                GetComponent<ProjectileSpawner>().shootProjectiles = false;
            }
        }
    }
}
