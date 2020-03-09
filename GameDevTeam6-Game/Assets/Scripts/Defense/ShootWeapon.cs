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
                GetComponent<PlayerStates>().ChangeState(playerStates.INTERACTING);
                GetComponent<ProjectileSpawner>().shootProjectiles = true;
            } else if (Input.GetMouseButton(1))
            {
                if (GetComponent<PlayerStates>().GetState() != playerStates.INTERACTING)
                {
                    GetComponent<PlayerStates>().ChangeState(playerStates.INTERACTING);
                }
            }
            else if (Input.GetMouseButtonUp(1))
            {
                GetComponent<ProjectileSpawner>().shootProjectiles = false;
            }
        }
    }
}
