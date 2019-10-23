using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int x = 1;
        float y = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        bool right = Input.GetKey("d");
        bool left = Input.GetKey("a");
        bool up = Input.GetKey("w");
        bool down = Input.GetKey("s");

        if (right)
        {
            transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, 0);
        }
        if (left)
        {
            transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, 0);
        }
        if (up)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        }
        if (down)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        }
    }

}
