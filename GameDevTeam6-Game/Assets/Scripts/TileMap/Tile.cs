using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    private GameObject objectOnTile;
    private Modes tileMode;

    public Tile()
    {
        ResetTileInfo();
    }

    public void ResetTileInfo()
    {
        objectOnTile = null;
        tileMode = Modes.unbreakable;
    }
    public GameObject getObjectOnTile() { return objectOnTile; }
    public void setObjectOnTile(GameObject obj) { objectOnTile = obj; }
    public Modes getBreakMode() { return tileMode; }
    public void setBreakMode(Modes mode) { tileMode = mode; } 
}

public enum Modes
{
    breakable,
    unbreakable,
    soil
}