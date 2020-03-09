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
    public GameObject invisibleCollision, invisbleBarrierNoCollision;

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
        PlaceObstacles();
        PlaceInvisibleCollisions();
    }

    string[] tilesToPlaceObstaclesOn =
    {
        "dirtc",
        "corner1_1",
        "corner2_1",
        "corner2_2",
        "corner2_3",
        "side1",
        "side2"
    };


    void PlaceObstacles()
    {
        //Places boulders and trees when the map is first loaded - Randy
        for (int i = 0; i < tileCountX; i++) {
            for (int j = 0; j < tileCountY; j++) {
                Sprite spr = map.GetSprite(new Vector3Int(i-1, j-1, 0));

                foreach (string checkname in tilesToPlaceObstaclesOn)  {
                    if (spr.name == checkname) {
                        //To spawn or not
                        if (Random.Range(1, 20) > 4)  {
                            //Pick object to spawn
                            int random = Random.Range(0, 20);
                            if (random < 15)  {
                                place.CreateObject(boulder, i, j);
                                TileLayout.Instance.GetTile(i, j).getObjectOnTile().GetComponent<SpriteRenderer>().sprite = boulderSprites[Random.Range(0, boulderSprites.Length)];
                            }
                            else if (random < 18) {
                                place.CreateObject(tree, i, j);
                                TileLayout.Instance.GetTile(i, j).getObjectOnTile().GetComponent<SpriteRenderer>().sprite = treeSprites[Random.Range(0, treeSprites.Length)];
                            }
                            else  {
                                place.CreateObject(tree2, i, j);
                                TileLayout.Instance.GetTile(i, j).getObjectOnTile().GetComponent<SpriteRenderer>().sprite = tree2Sprites[Random.Range(0, tree2Sprites.Length)];
                            }
                        }
                        break;
                    }
                }
            }
        }

        //this is critical - Clarence
        AstarPath.active.Scan(); //scans the map to create grid graph for pathfinding
    }

    string[] tilesToPlaceCollisionOn = {
        "corner1_5",
        "corner2_5",
        "corner2_6",
        "corner2_7",
        "side5",
        "side6",
        "water"
    };

    Vector2Int[] shipTilePositions =
    {
        new Vector2Int(27, 27),
        new Vector2Int(27, 28),
        new Vector2Int(27, 29),
        new Vector2Int(27, 30),
        new Vector2Int(27, 31),

        new Vector2Int(28, 27),
        new Vector2Int(28, 28),
        new Vector2Int(28, 29),
        new Vector2Int(28, 30),
        new Vector2Int(28, 31),

        new Vector2Int(29, 27),
        new Vector2Int(29, 28),
        new Vector2Int(29, 29),
        new Vector2Int(29, 30),
        new Vector2Int(29, 31),

        new Vector2Int(30, 27),
        new Vector2Int(30, 28),
        new Vector2Int(30, 29),
        new Vector2Int(30, 30),
        new Vector2Int(30, 31),

        new Vector2Int(31, 27),
        new Vector2Int(31, 28),
        new Vector2Int(31, 29),
        new Vector2Int(31, 30),
        new Vector2Int(31, 31),
    };


    /*
     * Place collision on water tiles and ship 
    */
    void PlaceInvisibleCollisions()
    {
        for (int i = 0; i < tileCountX; i++)
        {
            for (int j = 0; j < tileCountY; j++)
            {
                Sprite spr = map.GetSprite(new Vector3Int(i - 1, j - 1, 0));
                foreach (string spriteNameToCheck in tilesToPlaceCollisionOn)
                {
                    if (spr.name == spriteNameToCheck)
                    {
                        place.CreateObject(invisibleCollision, i, j);
                        TileLayout.Instance.GetTile(i, j).setBreakMode(TileMode.unbreakable);
                        break;
                    }
                }
            }
        }

        foreach (Vector2Int tilePos in shipTilePositions)
        {
            place.CreateObject(invisbleBarrierNoCollision, tilePos.x, tilePos.y);
            TileLayout.Instance.GetTile(tilePos.x, tilePos.y).setBreakMode(TileMode.unbreakable);
        }
    }
}
