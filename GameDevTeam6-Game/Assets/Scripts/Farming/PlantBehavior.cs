using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBehavior : MonoBehaviour
{
   
    private Plants plantProperties;
    private TimeSystem system;
    public float timeCounter = 0;

    void Start()
    {
        plantProperties = GetComponent<Plants>();
        system = GetComponent<TimeSystem>();
    }

    void Update()
    {
        timeCounter++;
    }

    public void planting(string species) 
    {
        Plants newPlant = new Plants(species);
        float currTime = system.getSeconds(); //bring in current time from TimeSystem
        Instantiate(newPlant);
        growing(newPlant, currTime);
        growFruits(newPlant, currTime);
    }

    public void growing(Plants plant, float time)
    {
        int stages = 1;

        if (timeCounter - time >= 360 && stages <= 5) //change stage 360 sec after growing() is callled
        {
            plant.setStages(stages + 1);

            if (plant.getStages() == 5)
            {
                growFruits(plant, time);
            }
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

    public void growFruits(Plants plant, float time)
    {
        int countFruits = plant.getCountFruits();

        if (timeCounter - time >= 480 && countFruits < 5) //add fruit 480 sec (8min) after growFruits() is called
        {
            plant.setArrFruits(countFruits, true);
            plantProperties.setCountFruits(countFruits + 1);
        }

    }

}
