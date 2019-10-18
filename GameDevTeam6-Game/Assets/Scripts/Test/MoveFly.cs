using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFly : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	bool left = Input.GetKey("left");
        bool right = Input.GetKey("right");
        bool up = Input.GetKey("up");
        bool down = Input.GetKey("down");

        if (right) {
            transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, 0);
        } else if (left) {
        	transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, 0);
        } else if (up) {
        	transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        } else if (down){
        	transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        }
    }
}
