using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceSeeds : MonoBehaviour
{
    public Tilemap tileMap;
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
        if (!(tool.GetMode() == ToolModes.farmMode)) {
            return;
        }

        place.GetMouseTile(out int tileX, out int tileY);

        place.CheckAndPlaceUpdate(plant, 2, plant);
    }
}
