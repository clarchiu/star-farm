using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class InitializeObjects : MonoBehaviour
{
    public Tilemap map;
    public GameObject boulder;
    public GameObject tree;
    public GameObject tree2;

    public Sprite[] boulderSprites;
    public Sprite[] treeSprites;
    public Sprite[] tree2Sprites;


    private TileLayout layout;
    private PlaceObjects place;

    int tileCountX;
    int tileCountY;

    private void Awake()
    {
        layout = FindObjectOfType<TileLayout>();
        place = FindObjectOfType<PlaceObjects>();
        tileCountX = layout.getTileCountX();
        tileCountY = layout.getTileCountY();
    }

    private void Start()
    {
        placeBoulders();
    }

    void placeBoulders()
    {
        //Places boulders and trees when the map is first loaded - Randy
        for (int i = 0; i < tileCountX; i++) {
            for (int j = 0; j < tileCountY; j++) {
                Sprite spr = map.GetSprite(new Vector3Int(i-1, j-1, 0));
                if (spr.name == "hd_0")
                {
                    //To spawn or not
                    if (Random.Range(1, 20) > 6)
                    {

                        //Pick object to spawn
                        int random = Random.Range(0, 20);

                        if (random < 15) {
                            place.CreateObject(boulder, i, j);
                            TileLayout.Instance.GetTile(i, j).getObjectOnTile().GetComponent<SpriteRenderer>().sprite = boulderSprites[Random.Range(0, boulderSprites.Length)];
                        } else if (random < 18) {
                            place.CreateObject(tree, i, j);
                            TileLayout.Instance.GetTile(i, j).getObjectOnTile().GetComponent<SpriteRenderer>().sprite = treeSprites[Random.Range(0, treeSprites.Length)];
                        } else
                        {
                            place.CreateObject(tree2, i, j);
                            TileLayout.Instance.GetTile(i, j).getObjectOnTile().GetComponent<SpriteRenderer>().sprite = tree2Sprites[Random.Range(0, tree2Sprites.Length)];
                        }
                    }
                }
            }
        }

        //this is critical - Clarence
        AstarPath.active.Scan(); //scans the map to create grid graph for pathfinding
    }
}
