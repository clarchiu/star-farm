using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
	public GameObject enemy;
    public TimeSystem timeSystem;

    [SerializeField] private float startSpawnTime = 2f;
    [SerializeField] private float spawnInterval = 5f;

    // Start is called before the first frame update
    void Start()
    {
        //spawn enemy every spawnInterval starting at startSpawnTime
        InvokeRepeating("spawnEnemy", startSpawnTime, spawnInterval);
    }


    private void spawnEnemy()
    {
        int hour = timeSystem.getHour();
        if (hour >= 14 && hour < 22)
        {
            Vector3 positionAlongEdge = new Vector3(0, 0, 0);


            int randomEdge;  //choose a random edge
            float randomPos; //choose a random position along the edge

            randomEdge = Random.Range(0, 4);
            randomPos = Random.Range(0.0f, 1.0f);

            if (randomEdge == 0)  //top edge
                positionAlongEdge = Camera.main.ViewportToWorldPoint(new Vector3(randomPos, 1, Camera.main.nearClipPlane));
            else if (randomEdge == 1) //bottom Edge
                positionAlongEdge = Camera.main.ViewportToWorldPoint(new Vector3(randomPos, 0, Camera.main.nearClipPlane));
            else if (randomEdge == 2) //left Edge
                positionAlongEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, randomPos, Camera.main.nearClipPlane));
            else if (randomEdge == 3) //right Edge
                positionAlongEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, randomPos, Camera.main.nearClipPlane));

            if (Physics2D.OverlapBoxAll(new Vector2(positionAlongEdge.x, positionAlongEdge.y), new Vector2(0.6f, 0.6f), 0f).Length == 0)
            {
                GameObject newEnemy = Instantiate(enemy, positionAlongEdge, Quaternion.identity);
            }
        }


    }
}
