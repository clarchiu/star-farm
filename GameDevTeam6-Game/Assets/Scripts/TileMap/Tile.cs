using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    private GameObject objectOnTile;
    private TileMode tileMode;

    public Tile()
    {
        ResetTileInfo();
    }

    public void ResetTileInfo()
    {
        objectOnTile = null;
        tileMode = TileMode.unbreakable;
    }
    public GameObject getObjectOnTile() { return objectOnTile; }
    public void setObjectOnTile(GameObject obj) { objectOnTile = obj; }
    public TileMode getBreakMode() { return tileMode; }
    public void setBreakMode(TileMode mode) { tileMode = mode; } 
}

public enum TileMode
{
    breakable,
    unbreakable,
    soil
}