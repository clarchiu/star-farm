using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection_ : MonoBehaviour
{
    private playerDir direction;
    private Animator anim;
    private bool walking;

    void Start()
    {
        direction = playerDir.down;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = GetComponent<Rigidbody2D>().velocity.normalized;
        anim.SetFloat("Horizontal", direction.x);
        anim.SetFloat("Vertical", direction.y);
    }

    public playerDir GetDirection() {
        return direction;
    }
}

public enum playerDir
{
    up,
    down,
    left,
    right
}
