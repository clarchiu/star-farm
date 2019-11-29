using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBehavior : MonoBehaviour
{
    private Plants plantProperties;
    private TimeSystem system;

    void Start()
    {
        plantProperties = GetComponent<Plants>();
        system = GetComponent<TimeSystem>();
    }


    void Update()
    {
/*
        if (growTime == 480 && numFruit != maxFruit) //fruit grows every 8 min
        {
            Instantiate(fruit); //spawn fruit
            fruitGrowTime -= 480;
            numFruit++;
        }
*/

    }

    public void planting(int currTime, string species) //bring in current time from TimeSystem
    {
        Plants newPlant = new Plants(species);
        Instantiate(newPlant);
        growing(currTime);
    }

    public void growing(int time)
    {
        float growTime = system.getSeconds();

        if (growTime - time >= 360 && plantProperties.getStages() <= 5)
        {
            plantProperties.setStages(plantProperties.getStages() + 1);
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

    public void growFruits()
    {
        int countFruits = plantProperties.getCountFruits();

        if (countFruits < 5)
        {
            plantProperties.setArrFruits(countFruits, true);
            plantProperties.setCountFruits(countFruits + 1);

        }

    }

}
