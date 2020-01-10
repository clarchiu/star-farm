using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoeGround : MonoBehaviour
{
    public GameObject soil;
    private PlaceObjects place;
    private MultiTool tool;
    private GameObject player;

    private void Awake()
    {
        place = FindObjectOfType<PlaceObjects>();
        tool = FindObjectOfType<MultiTool>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (!(tool.GetMode() == ToolModes.farmMode)) {
            return;
        }
        place.CheckAndPlaceUpdate(soil, 2, soil);
    }
}
