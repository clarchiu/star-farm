using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWeapon : MonoBehaviour
{
    private MultiTool multiTool;
    // creates audiosource and audioclip for laser fire sound
    AudioSource laser;
    public AudioClip laser_fire;

    private void Awake()
    {
        multiTool = FindObjectOfType<MultiTool>();
        //creates audiosource component attached onto player 
        laser = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (multiTool.GetMode() != ToolModes.combatMode) return;
        else {
            if (Input.GetMouseButtonDown(1))
            {
                GetComponent<ProjectileSpawner>().shootProjectiles = true;
                //plays laser fire sound
                laser.PlayOneShot(laser_fire, 1f);
                
            } else if (Input.GetMouseButtonUp(1))
            {
                GetComponent<ProjectileSpawner>().shootProjectiles = false;
                
            }
        }
    }
}
