using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/*
 * This class handles the graphics layer of an enemy
 * in order to separate the graphical layer from the logical layer
 * - Clarence
 */

public enum GFXStates
{
    Moving,
    Attacking,
    Idling
}

public class EnemyGFX : MonoBehaviour
{
    private Animator animator;
    public Animator MyAnimator { get => animator; }

    private Vector2 direction;
    public Vector2 Direction //make it the responsibility of the logical layer to set direction
    {
        set
        {
            direction = (Vector2)value.normalized;
            animator.SetFloat("x", direction.x);
            animator.SetFloat("y", direction.y);
        }
    }

    private GFXStates state;
    public GFXStates MyState //make it the responsibility of the logical layer to set state
    {
        set
        {
            state = value;

            if (state == GFXStates.Moving)
            {
                ActivateLayer("Walk Layer");
            }
            else if (state == GFXStates.Attacking)
            {
                //Debug.Log("is attacking");
                ActivateLayer("Attack Layer");
            }
            else
            {
                //Debug.Log("is idling");
                ActivateLayer("Idle Layer");
            }
        }
    }

    private void Awake()
    {
        direction = Vector2.zero;
        state = GFXStates.Idling;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //HandleLayers();
    }

    private void ActivateLayer(string layerName)
    {
        for (int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }

        animator.SetLayerWeight(animator.GetLayerIndex(layerName), 1);
    }


    //TODO: deprecated, delete this after more testing

    //private AIPath aiPath;
    //private Rigidbody2D rb;

    //the following booleans decide which animation set should be played
    //public bool IsAttacking { get; set; }

    //public void HandleLayers()
    //{
    //    if (IsMoving)
    //    {
    //        //Debug.LogFormat("is moving: {0}", direction);
    //        ActivateLayer("Walk Layer");
    //        //animator.SetFloat("x", direction.x);
    //        //animator.SetFloat("y", direction.y);
    //    }
    //    else if (IsAttacking)
    //    {
    //        //Debug.Log("is attacking");
    //        ActivateLayer("Attack Layer");
    //        //animator.SetFloat("x", direction.x);
    //        //animator.SetFloat("y", direction.y);
    //    }
    //    else
    //    {
    //        //Debug.Log("is idling");
    //        ActivateLayer("Idle Layer");
    //    }
    //}
}