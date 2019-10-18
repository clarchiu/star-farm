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
        bool w = Input.GetKey("d");
        bool a = Input.GetKey("a");
        bool b = Input.GetKey("w");
        bool s = Input.GetKey("s");
        
        if (w == true)
        {
            transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
        }
        
        if (a == true)
        {
            transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
        }
        
        if (b == true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        }

        if (s == true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
        }
        
    }
}
