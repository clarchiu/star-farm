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
        int count = 0;
        count++;
        
        if (count == 480) //8x60 seconds
        {
            //regrow fruit
        }

        if (count == 360)
        {
            stages++;
        }

        while (count <= 1800) //30*60 seconds = 30 min = 1 day
        {
            
        }
    }
}
