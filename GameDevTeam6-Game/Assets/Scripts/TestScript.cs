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
        bool y = Input.GetKey("w");

        if (y == true)
        {
            transform.position = new Vector3(transform.position.x + 0.1f, 0, 0);
        }
    }
}
