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
    MOVING,
    ATTACKING,
    IDLING
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

            if (state == GFXStates.MOVING)
            {
                ActivateLayer("Walk Layer");
            }
            else if (state == GFXStates.ATTACKING)
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
        animator = GetComponent<Animator>();
        direction = Vector2.zero;
        state = GFXStates.IDLING;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (aiPath.canMove == true) //use the velocity from AIPath if it is moving
            direction = aiPath.velocity.normalized;

        HandleLayers();
    }

    public void HandleLayers()
    {
        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);

        if (IsMoving)
        {
            //Debug.LogFormat("is moving: {0}", direction);
            ActivateLayer("Walk Layer");
        }
        else if (IsAttacking)
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

    void ActivateLayer(string layerName)
    {
        for (int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }

        animator.SetLayerWeight(animator.GetLayerIndex(layerName), 1);
    }
}
