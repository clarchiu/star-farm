using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTile
{
    private GameObject objectOnTile;
    private TileMode tileMode;
    public bool Walkable = true; //default is true until setObjectOnTile is called

    public ObjectTile()
    {
        ResetTileInfo();
    }

    public void ResetTileInfo()
    {
        objectOnTile = null;
        tileMode = TileMode.unbreakable;
        Walkable = true;
    }

    public GameObject getObjectOnTile() { return objectOnTile; }

    public void setObjectOnTile(GameObject obj)
    {
        objectOnTile = obj;
        if (objectOnTile.CompareTag("Unwalkable"))
        {
            Walkable = false;
        } else
        {
            Walkable = true;
        }
    }
    public TileMode getBreakMode() { return tileMode; }
    public void setBreakMode(TileMode mode) { tileMode = mode; }

    //for debugging purposes
    public override string ToString()
    {

        return string.Format("Tile Walkable: {0}", Walkable);
    }
}

public enum TileMode
{
    breakable,
    unbreakable,
    soil
}