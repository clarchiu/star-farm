using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBehavior : MonoBehaviour
{

    private Plants plantProperties;
    [HideInInspector]
    public int growDelay = 80;
    private float timer;

    void Start()
    {
        plantProperties = GetComponent<Plants>();
        timer = 0;

    }

    private void Update()
    {
        if (plantProperties.getStages() <= 4)
        {
            timer += Time.deltaTime;
            if (timer >= growDelay)
            {
                plantProperties.setStages(plantProperties.getStages() + 1);
                timer = 0;
            }
        }
    }
}
