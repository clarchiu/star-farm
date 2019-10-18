using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Start is called before the first frame update
	public GameObject fly;
	public 


    void Start()
    {
        InvokeRepeating("spawnFly", 2.0f, 1.0f);	//spawn fly every second starting at 2 seconds
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnFly() 
    {

    	Vector3 positionAlongEdge = new Vector3(0,0,0);

		int randomEdge = Random.Range(0,4);
        float randomPos = Random.Range(0.0f, 1.0f);

		if (randomEdge == 0) //top Edge
		{
        	positionAlongEdge = Camera.main.ViewportToWorldPoint(new Vector3(randomPos, 1, Camera.main.nearClipPlane));
		}
		else if (randomEdge == 1) //bottom Edge
		{
        	positionAlongEdge = Camera.main.ViewportToWorldPoint(new Vector3(randomPos, 0, Camera.main.nearClipPlane));
		}
		else if (randomEdge == 2) //left Edge
		{
       		positionAlongEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, randomPos, Camera.main.nearClipPlane));
		}
		else if (randomEdge == 3) //right Edge
		{
        	positionAlongEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, randomPos, Camera.main.nearClipPlane));
		}

    	GameObject newFly = Instantiate(fly, positionAlongEdge, Quaternion.identity);
    }
}
