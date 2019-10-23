using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    public int pixelSize;
    public GameObject testObject;

    void Update() {
        if (Input.GetMouseButton(0)) {
            int tileX = Mathf.RoundToInt(Input.mousePosition.x / pixelSize);
            int tileY = Mathf.RoundToInt(Input.mousePosition.y / pixelSize);

            Debug.Log(tileX+ " " + tileY);
            if (tileX < 0 || tileX > GetComponent<TileInformation>().tileCountX - 1)
            {
                return;
            }
            if (tileY < 0 || tileY > GetComponent<TileInformation>().tileCountX - 1)
            {
                return;
            }
            CreateObject(testObject, tileX, tileY);
        }
    }

    //Creates object at Tile[x,y]
    void CreateObject(GameObject newObj, int x, int y) {
        GameObject oldObj = GetComponent<TileInformation>().GetTile(x, y).getObjectOnTile();
        if (oldObj == null)
        {
            Vector2 position = new Vector2(x * pixelSize, y * pixelSize);
            Instantiate(newObj, position, Quaternion.identity);
        }
        
    }
}
