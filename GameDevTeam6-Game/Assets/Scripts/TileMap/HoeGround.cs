using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoeGround : MonoBehaviour
{
    public GameObject soil;
    private PlaceObjects place;

    private void Start()
    {
        place = FindObjectOfType<PlaceObjects>();
    }
    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            CreateSoil();
        }
    }
    void CreateSoil()
    {
        place.CreateObject(soil);
    }
}
