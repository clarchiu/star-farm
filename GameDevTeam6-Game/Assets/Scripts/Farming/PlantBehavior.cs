using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBehavior : MonoBehaviour
{
    private Plants plantProperties;
    private TimeSystem system;
    public float timeCounter;

    void Start()
    {
        plantProperties = GetComponent<Plants>();
        system = GetComponent<TimeSystem>();
    }

    void Update()
    {
        timeCounter++;
    }

    public void planting(string species) //bring in current time from TimeSystem
    {
        Plants newPlant = new Plants(species);
        float currTime = system.getSeconds();
        Instantiate(newPlant);
        growing(currTime);
        growFruits(currTime);
    }

    public void growing(float time)
    {
        int stages = plantProperties.getStages();

        if (timeCounter - time >= 360 && stages <= 5) //change stage 360 sec after growing() is callled
        {
            plantProperties.setStages(stages + 1);
        }        
    }

    public void harvesting()
    {
        int countFruits = plantProperties.getCountFruits();

        if (countFruits > 0)
        {
            //put fruit into inventory
            plantProperties.setArrFruits(countFruits, false);
            plantProperties.setCountFruits(countFruits - 1);
        }
    }

    public void growFruits(float time)
    {
        int countFruits = plantProperties.getCountFruits();

        if (timeCounter - time >= 480 && countFruits < 5) //add fruit 480 sec (8min) after growFruits() is called
        {
            plantProperties.setArrFruits(countFruits, true);
            plantProperties.setCountFruits(countFruits + 1);
        }

    }

}
