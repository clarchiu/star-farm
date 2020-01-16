using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLayout : MonoBehaviour
{
    public int tileCountX;
    public int tileCountY;
    ObjectTile[,] tiles;

    // Start is called before the first frame update
    void Awake()
    {
        PlaceObjects place = FindObjectOfType<PlaceObjects>();

        tiles = new ObjectTile[tileCountX, tileCountY];
        for (int i = 0; i < tileCountX; i++) {
            for (int j = 0; j < tileCountY; j++) {
                tiles[i, j] = new ObjectTile();
            }
        }
    }
    public ObjectTile GetTile(int x, int y)
    {
        return tiles[x, y];
    }

    public int getTileCountX() {
        return tileCountX;
    }
    public int getTileCountY() {
        return tileCountY;
    }
}
