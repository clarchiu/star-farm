using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    public int stages = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        int countSec = 0;
        countSec++;

        if (countSec == 360) //stages change every 6 min -- plant grows completely in 30 min
        {
            if (stages <= 5)
            {
                stages++;
                countSec -= 360;
            }
        }
    }
}
