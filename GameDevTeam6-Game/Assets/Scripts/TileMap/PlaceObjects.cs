using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    private int pixelSize = 16;
    public GameObject testObject;
    private MultiTool tool;

    private void Awake()
    {
        tool = FindObjectOfType<MultiTool>();
    }

    void Update() {
        if (!(tool.GetMode() == ToolModes.buildingMode)) {
            return;
        }
        if (Input.GetMouseButtonDown(0)) {
            CreateObject(testObject);
        }
        if (Input.GetMouseButtonDown(1))
        {
            GetMouseTile(out int tileX, out int tileY);
            DestroyObject(tileX, tileY);
        }
    }
    //Creates object at Tile[x,y] if there is no other object
    public void CreateObject(GameObject newObj, int x, int y) {
        if (!InBounds(x, y)) {
            Debug.Log("Tried to create an object outside of bounds and failed");
            return;
        }
        Tile tile = GetComponent<TileLayout>().GetTile(x, y);
        GameObject oldObj = tile.getObjectOnTile();
        if (oldObj == null)
        {
            Vector2 position = new Vector2(x, y);
            GameObject obj = Instantiate(newObj, position, Quaternion.identity);
            tile.setObjectOnTile(obj);
        }
        
    }
    //Creates object at mouse position if there is no other object
    public void CreateObject(GameObject newObj) {
        int tileX = 0;
        int tileY = 0;
        GetMouseTile(out tileX, out tileY);
        if (!InBounds(tileX, tileY)) {
            Debug.Log("Tried to create an object outside of bounds and failed");
            return;
        }

        Tile tile = GetComponent<TileLayout>().GetTile(tileX, tileY);
        GameObject oldObj = tile.getObjectOnTile();
        if (oldObj == null)
        {
            Vector2 position = new Vector2(tileX, tileY);
            GameObject obj = Instantiate(newObj, position, Quaternion.identity);
            tile.setObjectOnTile(obj);
        }
    }

    public void DestroyObject(int x, int y)
    {
        if (!InBounds(x, y)) {
            Debug.Log("Tried to destroy an object outside of bounds and failed");
            return;
        }

        Tile tile = GetComponent<TileLayout>().GetTile(x, y);
        GameObject objectOnTile = tile.getObjectOnTile();
        if (objectOnTile != null)
        {
            Destroy(objectOnTile);
        }
        GetComponent<TileLayout>().GetTile(x, y).ResetTileInfo();
    }

    public bool InBounds(int x, int y)
    {
        if (x < 0 || x > GetComponent<TileLayout>().tileCountX - 1)
        {
            return false;
        }
        if (y < 0 || y > GetComponent<TileLayout>().tileCountY - 1)
        {
            return false;
        }
        return true;
    }

    public void GetMouseTile(out int tileX, out int tileY)
    {
        Vector2 mouseToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tileX = Mathf.RoundToInt(mouseToWorld.x);
        tileY = Mathf.RoundToInt(mouseToWorld.y);
    }
}
