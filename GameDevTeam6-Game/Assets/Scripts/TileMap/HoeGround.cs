using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoeGround : MonoBehaviour
{
    public GameObject soil;
    private PlaceObjects place;
    private MultiTool tool;

    private void Awake()
    {
        place = FindObjectOfType<PlaceObjects>();
        tool = FindObjectOfType<MultiTool>();
    }
    private void Update()
    {
        if (!(tool.GetMode() == ToolModes.hoeMode)) {
            return;
        }
        if (Input.GetMouseButtonDown(0)) {
            CreateSoil();
        }
    }
    void CreateSoil()
    {
        place.CreateObject(soil);
    }
}
