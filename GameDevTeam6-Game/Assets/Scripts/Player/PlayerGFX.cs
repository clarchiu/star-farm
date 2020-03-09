using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGFX : MonoBehaviour
{

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Required so that direction is set even if no buttons are being pressed
        anim.SetFloat("x", 0);
        anim.SetFloat("y", 0);

        if (GetComponent<PlayerDirection_>().GetDirection() == playerDir.left)
        {
            anim.SetFloat("x", -1);
        }
        else if (GetComponent<PlayerDirection_>().GetDirection() == playerDir.right)
        {
            anim.SetFloat("x", 1);
        }
        else if (GetComponent<PlayerDirection_>().GetDirection() == playerDir.up)
        {
            anim.SetFloat("y", 1);
        }
        else if (GetComponent<PlayerDirection_>().GetDirection() == playerDir.down)
        {
            anim.SetFloat("y", -1);
        }
    }

    private GFXStates state;
    public GFXStates MyState //make it the responsibility of the logical layer to set state
    {
        set
        {
            state = value;

            if (state == GFXStates.MOVING)
            {
                ActivateLayer("Walk Layer");
            }
            else if (state == GFXStates.ATTACKING)
            {
                ActivateLayer("Attack Layer");
                anim.SetTrigger("attack");
            }
            else
            {
                ActivateLayer("Idle Layer");
            }
        }
    }

    private void ActivateLayer(string layerName)
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }
        anim.SetLayerWeight(anim.GetLayerIndex(layerName), 1);
    }
}
