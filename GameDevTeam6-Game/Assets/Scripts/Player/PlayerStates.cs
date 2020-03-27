using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    private playerStates state;

    private static PlayerStates _instance;
    public static PlayerStates Instance
    {
        get
        {
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<PlayerStates>();
                }
                if (_instance == null)
                {
                    Debug.Log("PlayerStates script not found!, Add resource manager prefab to your scene!");
                }
                return _instance;
            }
        }
    }

    public void ChangeState(playerStates proposedState)
    {
        if (state == playerStates.INTERACTING && proposedState == playerStates.WALKING)
        {
            Debug.Log("Illegal state change, can't go from walking to attacking");
        }
        if (state == playerStates.INTERACTING && proposedState == playerStates.INTERACTING)
        {
            //Clicking too fast, ignore
        } else {
            state = proposedState;
            if (state == playerStates.WALKING) {
                GetComponent<PlayerGFX>().MyState = GFXStates.MOVING;
            } else if (state == playerStates.IDLE) {
                GetComponent<PlayerGFX>().MyState = GFXStates.IDLING;
            } else if (state == playerStates.INTERACTING) {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        GetComponent<PlayerGFX>().MyState = GFXStates.ATTACKING;
        yield return new WaitForSeconds(0.4f);
        ChangeState(playerStates.IDLE);
    }

    public playerStates GetState() {
        return state;
    }
}

public enum playerStates
{
    WALKING,
    IDLE,
    INTERACTING
}