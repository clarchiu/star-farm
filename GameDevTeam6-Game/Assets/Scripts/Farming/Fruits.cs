using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    public int fruitGrowTime = 0;
    public GameObject fruit;
    public int maxFruit = 10; //can be changed
    public int numFruit = 0;

    void Update()
    {
        fruitGrowTime++;

        
    }
    void harvestFruit(GameObject plant)
    {
        //add fruit to inventory
        Destroy(fruit);
        numFruit--;
    }



}