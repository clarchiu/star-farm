using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBehavior : MonoBehaviour
{

    private Plants plantProperties;
    [HideInInspector]
    public int delay = 60;

    void Start()
    {
        plantProperties = GetComponent<Plants>();
        StartCoroutine(Grow());
    }

    IEnumerator Grow()
    {
        while (plantProperties.getStages() <= 4)
        {
            plantProperties.setStages(plantProperties.getStages() + 1);
            yield return new WaitForSecondsRealtime(delay);
        }
    }
}
