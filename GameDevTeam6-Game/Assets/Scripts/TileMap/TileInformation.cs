using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInformation : MonoBehaviour
{
    public int tileCountX;
    public int tileCountY;
    Tile[,] tiles;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new Tile[20,20];
        for (int i = 0; i < 20; i ++){
            for (int j = 0; j < 20; j++) {
                tiles[i, j] = new Tile();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
