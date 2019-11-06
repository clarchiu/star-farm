using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
	public GameObject enemy;

    [SerializeField] private float startSpawnTime = 2f;
    [SerializeField] private float spawnInterval = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //spawn enemy every spawnInterval starting at spawnInterval
        InvokeRepeating("spawnEnemy", startSpawnTime, spawnInterval);	
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnEnemy() 
    {
    	Vector3 positionAlongEdge = new Vector3(0,0,0);

		int randomEdge = Random.Range(0,4);   //choose a random edge
        float randomPos = Random.Range(0.0f, 1.0f); //choose a random position along the edge

		if (randomEdge == 0)  //top edge
        	positionAlongEdge = Camera.main.ViewportToWorldPoint(new Vector3(randomPos, 1, Camera.main.nearClipPlane));
		else if (randomEdge == 1) //bottom Edge
        	positionAlongEdge = Camera.main.ViewportToWorldPoint(new Vector3(randomPos, 0, Camera.main.nearClipPlane));
		else if (randomEdge == 2) //left Edge
       		positionAlongEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, randomPos, Camera.main.nearClipPlane));
		else if (randomEdge == 3) //right Edge
        	positionAlongEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, randomPos, Camera.main.nearClipPlane));

    	GameObject newEnemy = Instantiate(enemy, positionAlongEdge, Quaternion.identity);

        
    }
}
