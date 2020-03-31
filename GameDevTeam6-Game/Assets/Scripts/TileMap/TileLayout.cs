using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLayout : MonoBehaviour
{
    public int tileCountX;
    public int tileCountY;
    ObjectTile[,] tiles;

    private static TileLayout _instance;
    public static TileLayout Instance
    {
        get
        {
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<TileLayout>();
                }
                return _instance;
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        PlaceObjects place = FindObjectOfType<PlaceObjects>();

        tiles = new ObjectTile[tileCountX, tileCountY];
        for (int i = 0; i < tileCountX; i++)
        {
            for (int j = 0; j < tileCountY; j++)
            {


                tiles[i, j] = new ObjectTile();
            }
        }
    }

    public ObjectTile GetTile(int x, int y)
    {
        if (x<0 || y<0) //need to check if x and y are out of bounds
        {
            return null;
        }
        if (x >= tileCountX || y > tileCountY)
        {
            return null;
        }
        else 
        {
            return tiles[x, y];
        }
    }

    public int getTileCountX() {
        return tileCountX;
    }
    public int getTileCountY() {
        return tileCountY;
    }
}
