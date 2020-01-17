using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection_ : MonoBehaviour
{
    private playerDir direction;
    private Animator anim;

    void Start()
    {
        direction = playerDir.down;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") == -1) {
            anim.speed = 1;
            direction = playerDir.left;
            anim.SetFloat("Horizontal", -1);
            anim.SetFloat("Vertical", 0);
        } else if (Input.GetAxisRaw("Horizontal") == 1) {
            anim.speed = 1;
            direction = playerDir.right;
            anim.SetFloat("Horizontal", 1);
            anim.SetFloat("Vertical", 0);
        } else if (Input.GetAxisRaw("Vertical") == -1)
        {
            anim.speed = 1;
            direction = playerDir.down;
            anim.SetFloat("Vertical", -1);
            anim.SetFloat("Horizontal", 0);
        } else if (Input.GetAxisRaw("Vertical") == 1)
        {
            anim.speed = 1;
            direction = playerDir.up;
            anim.SetFloat("Vertical", 1);
            anim.SetFloat("Horizontal", 0);
        } else
        {
            anim.speed = 0;
        }
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
