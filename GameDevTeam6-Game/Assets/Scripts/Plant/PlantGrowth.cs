using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    public int stages = 0;
    public int countGrow = 0;
    public int countFruit = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        countGrow++;
        countFruit++;

        if (countGrow == 360) //stage changes every 6 min -- plant grows completely in 30 min
        {
            if (stages <= 5)
            {
                stages++;
                countGrow -= 360;
            }
        }

        if (countFruit == 480) //fruit grows every 8 min
        {
            //spawn fruit if fruit != maxFruit
            countFruit -= 480;
        }
    }
}
