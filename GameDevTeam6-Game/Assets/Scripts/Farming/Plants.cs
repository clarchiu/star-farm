using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : MonoBehaviour
{
    private int stages;
    private string species;
    private bool[] arrFruits = new bool[5];
    private int countFruits;


    public Plants(string species)
    {
        this.species = species;
        stages = 0;
        countFruits = 0;
        bool[] arrFruits = new bool[] { false, false, false, false, false }; //set max fruit per plant to be 5 (subject to change)
    }

    public int getStages()
    {
        return stages;
    }

    public void setStages(int stages)
    {
        this.stages = stages;
    }

    public void setArrFruits(int index, bool exists)
    {
        arrFruits[index] = exists;
    }

    public int getCountFruits()
    {
        return countFruits;
    }

    public void setCountFruits(int countFruits)
    {
        this.countFruits = countFruits;
    }


 }
