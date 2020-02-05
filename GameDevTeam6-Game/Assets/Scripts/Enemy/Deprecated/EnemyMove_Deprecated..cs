using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
	[SerializeField] private GameObject player = null;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float directionChangeInterval = 0.05f;

    private float timeLeft;
    private int randomDirection;

    // Start is called before the first frame update
    void Start()
    {
        throw new System.Exception("deprecated, don't use this -Clarence");
        randomDirection = Random.Range(0,2);
    }

    // Update is called once per frame
    void Update()
    {
    	//only change direction every second instead of every frame
    	timeLeft -= Time.deltaTime;
   		if(timeLeft <= 0)
   		{
   			randomDirection = Random.Range(0,2);
     		timeLeft += directionChangeInterval;
   		}

   		moveTowardsObject(player, randomDirection);
    }

    private void moveTowardsObject(GameObject obj, int randomDirection)
    {
    	float objX = 0f;
    	float objY = 0f;

    	Transform objTransform = obj.GetComponent<Transform>();
    	if (objTransform) { //check object has transform component
    		objX = objTransform.position.x;
    		objY = objTransform.position.y;
    	} else {
            return;
        }

        int directionX = getDirectionToMoveOnAxis(transform.position.x, objX);
        int directionY = getDirectionToMoveOnAxis(transform.position.y, objY);

        float deltaX = 0f;
        float deltaY = 0f;

        if (randomDirection == 1)
        	deltaX = directionX * moveSpeed * Time.deltaTime;
       	else
       		deltaY = directionY * moveSpeed * Time.deltaTime;

		float newPosX = transform.position.x + deltaX;
        float newPosY = transform.position.y + deltaY;
        transform.position = new Vector2(newPosX, newPosY);
    }

    private int getDirectionToMoveOnAxis(float selfPos, float objPos ) {
        if (selfPos < objPos) { return 1; }
        else if (selfPos > objPos) { return -1; }
        else { return 0; }
    }
}
