using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{
    private GameObject plant;

    void Start() {
        plant = null;
    }

    public GameObject GetPlant() {
        return plant;
    }

    public void SetPlant(GameObject p) {
        plant = p;
    }
}
