using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    private int pixelSize = 16;
    public GameObject testObject;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 mouseToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int tileX = Mathf.RoundToInt(mouseToWorld.x);
            int tileY = Mathf.RoundToInt(mouseToWorld.y);

            if (InBounds(tileX, tileY)) {
                CreateObject(testObject, tileX, tileY);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mouseToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int tileX = Mathf.RoundToInt(mouseToWorld.x);
            int tileY = Mathf.RoundToInt(mouseToWorld.y);

            if (InBounds(tileX, tileY)) {
                DestroyObject(tileX, tileY);
            }
        }
    }
    //Creates object at Tile[x,y]
    public void CreateObject(GameObject newObj, int x, int y) {
        Tile tile = GetComponent<TileLayout>().GetTile(x, y);
        GameObject oldObj = tile.getObjectOnTile();
        if (oldObj == null)
        {
            Vector2 position = new Vector2(x, y);
            GameObject obj = Instantiate(newObj, position, Quaternion.identity);
            tile.setObjectOnTile(obj);
        }
        
    }

    public void DestroyObject(int x, int y)
    {
        Tile tile = GetComponent<TileLayout>().GetTile(x, y);
        GameObject objectOnTile = tile.getObjectOnTile();
        if (objectOnTile != null)
        {
            Destroy(objectOnTile);
        }
        GetComponent<TileLayout>().GetTile(x, y).ResetTileInfo();
    }

    private bool InBounds(int x, int y)
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
}
