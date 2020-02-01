using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/*
 * This class handles the graphics layer of an enemy
 * in order to separate the graphical layer from the logical layer
 * - Clarence
 */

public class EnemyGFX : MonoBehaviour 
{
    private AIPath aiPath;
    private Rigidbody2D rb;

    private Animator animator;
    public Animator MyAnimator { get => animator; }

    private Vector2 direction;
    public Vector2 Direction { set => direction = (Vector2) value.normalized; }

    //the following booleans decide which animation set should be played
    public bool IsAttacking { get; set; }
    private bool IsMoving { get => aiPath.canMove || !rb.velocity.Equals(Vector2.zero); }
    

    // Start is called before the first frame update
    void Start()
    {
        aiPath = GetComponentInParent<AIPath>();
        rb = GetComponentInParent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        direction = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //direction has to be set by GFX in update otherwise it won't be accurate - Clarence 
        if (aiPath.canMove == true) //use the velocity from AIPath if it is moving
        {
            direction = aiPath.velocity.normalized;
        }
        else if (!IsAttacking) //otherwise it is in follow state and use the rigidbody
        {
            direction = rb.velocity.normalized;
        }

        HandleLayers();
    }

    public void HandleLayers()
    {
        if (IsMoving)
        {
            //Debug.LogFormat("is moving: {0}", direction);
            ActivateLayer("Walk Layer");
            animator.SetFloat("x", direction.x);
            animator.SetFloat("y", direction.y);
        }
        else if (IsAttacking)
        {
            //Debug.Log("is attacking");
            ActivateLayer("Attack Layer");
            animator.SetFloat("x", direction.x);
            animator.SetFloat("y", direction.y);
        }
        else
        {
            //Debug.Log("is idling");
            ActivateLayer("Idle Layer");
        }
    }

    private void ActivateLayer(string layerName)
    {
        for (int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }

        animator.SetLayerWeight(animator.GetLayerIndex(layerName), 1);
    }
}