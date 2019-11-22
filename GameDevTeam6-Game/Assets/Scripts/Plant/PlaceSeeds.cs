using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceSeeds : MonoBehaviour
{
    PlaceObjects place;
    TileLayout tiles;
    MultiTool tool;
    public GameObject plant; //Temporary

    private void Start()
    {
        tool = FindObjectOfType<MultiTool>();
        place = FindObjectOfType<PlaceObjects>();
        tiles = FindObjectOfType<TileLayout>();
    }

    private void Update()
    {
        if (!(tool.GetMode() == ToolModes.seedsMode)) {
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            SetPlant(plant);
        }
    }

    private void SetPlant(GameObject plant)
    {
        place.GetMouseTile(out int tileX, out int tileY);
        if (!place.InBounds(tileX, tileY))
        {
            Debug.Log("Tried to create a seed outside of bounds and failed");
            return;
        }

        GameObject obj = tiles.GetTile(tileX, tileY).getObjectOnTile();
        if (obj != null && obj.CompareTag("Soil") == true) {
            obj.GetComponent<Soil>().SetPlant(plant);
        }
    }
}
