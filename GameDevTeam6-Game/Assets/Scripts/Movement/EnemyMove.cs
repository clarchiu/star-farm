using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
	public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void moveTowardsObject(GameObject object) {

    	float x = 0.0f;
    	float y = 0.0f;

    	Transform objTransform = object.GetComponent<Transform>();
    	if (objTransform) {
    		x = objTransform.position.x;
    		y = objTransform.position.y;
    	}

    	
    }
}
