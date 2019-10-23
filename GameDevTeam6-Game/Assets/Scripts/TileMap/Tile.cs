using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    private GameObject objectOnTile;
    private Modes breakMode;

    public Tile()
    {
        objectOnTile = null;
        breakMode = Modes.unbreakable;
    }
    public GameObject getObjectOnTile() { return objectOnTile; }
    public void setObjectOnTile(GameObject obj) { objectOnTile = obj; }
    public Modes getBreakMode() { return breakMode; }
    public void setBreakMode(Modes mode) { breakMode = mode; } 
}

public enum Modes
{
    breakable,
    unbreakable
}