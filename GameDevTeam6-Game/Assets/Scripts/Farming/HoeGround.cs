using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HoeGround : MonoBehaviour
{
    private PlaceObjects place;
    private MultiTool tool;
    private GameObject player;
    [SerializeField]
    private Tilemap tileMap;
    public Tile[] tiles;

    private void Awake()
    {
        place = FindObjectOfType<PlaceObjects>();
        tool = FindObjectOfType<MultiTool>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (!(tool.GetMode() == ToolModes.farmMode))
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            place.GetMouseTile(out int tileX, out int tileY);

            if (tileMap.GetTile(new Vector3Int(tileX - 1, tileY - 1, 0)).name != "dirtc")
            {
                if (!place.InBounds(tileX, tileY))
                {
                    Debug.Log("Tried to hoe ground outside of bounds and failed");
                    return;
                }
                tileMap.SetTile(new Vector3Int(tileX - 1, tileY - 1, 0), tiles[0]);

            }
        }
    }
}
