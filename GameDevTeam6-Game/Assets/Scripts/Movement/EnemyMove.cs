using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
	public GameObject player;

    [SerializeField] private float moveSpeed = 5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void moveTowardsObject(GameObject obj) {

    	float objX;
    	float objY;

    	Transform objTransform = obj.GetComponent<Transform>(); 
    	if (objTransform) { //check object has transform component
    		objX = objTransform.position.x;
    		objY = objTransform.position.y;
    	} else {
            return;
        }

        int randomDirection = Random.range(0,1);

        

    	
    }
}
