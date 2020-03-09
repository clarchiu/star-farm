using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection_ : MonoBehaviour
{
    private playerDir direction;

    void Start()
    {
        direction = playerDir.down;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") == -1) {
            direction = playerDir.left;
        }
        else if (Input.GetAxisRaw("Horizontal") == 1) {
            direction = playerDir.right;
        }
        else if (Input.GetAxisRaw("Vertical") == -1)
        {
            direction = playerDir.down;
        } else if (Input.GetAxisRaw("Vertical") == 1)
        {
            direction = playerDir.up;
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
