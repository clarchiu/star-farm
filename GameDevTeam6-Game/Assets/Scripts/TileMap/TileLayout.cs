using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLayout : MonoBehaviour
{
    public int tileCountX;
    public int tileCountY;
    Tile[,] tiles;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new Tile[tileCountX, tileCountY];
        for (int i = 0; i < tileCountX; i++)
        {
            for (int j = 0; j < tileCountY; j++)
            {
                tiles[i, j] = new Tile();
            }
        }
    }
    public Tile GetTile(int x, int y)
    {
        return tiles[x, y];
    }
}
