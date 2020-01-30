using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class InitializeObjects : MonoBehaviour
{
    public Tilemap map;
    public GameObject boulder;

    private TileLayout layout;
    private PlaceObjects place;

    int tileCountX;
    int tileCountY;

    private void Awake()
    {
        layout = FindObjectOfType<TileLayout>();
        place = FindObjectOfType<PlaceObjects>();
        tileCountX = layout.getTileCountX();
        tileCountY = layout.getTileCountY();
    }

    private void Start()
    {
        placeBoulders();
    }

    void placeBoulders()
    {
        for (int i = 0; i < tileCountX; i++) {
            for (int j = 0; j < tileCountY; j++) {
                Sprite spr = map.GetSprite(new Vector3Int(i-1, j-1, 0));
                if (spr.name == "hd_0")
                {
                    place.CreateObject(boulder, i, j);
                }
            }
        }

        //this is critical - Clarence
        AstarPath.active.Scan(); //scans the map to create grid graph for pathfinding
    }
}
