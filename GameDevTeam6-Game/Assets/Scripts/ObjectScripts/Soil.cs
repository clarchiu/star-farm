using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{
    private GameObject plant;

    void Awake() {
        plant = null;
    }

    public GameObject GetPlant() {
        return plant;
    }

    public void SetPlant(GameObject p) {
        if (plant != null) {
            Debug.Log("Tried to plant where there already is a plant");
            return;
        } 
        plant = Instantiate(p, transform.position, Quaternion.identity);
        plant.transform.parent = gameObject.transform;
    }
}
