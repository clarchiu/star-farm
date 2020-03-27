using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class InitializeObjects : MonoBehaviour
{
    public Tilemap map;
    public GameObject[] minerals;
    public GameObject tree;
    public GameObject tree2;
    public GameObject invisibleCollision, invisbleBarrierNoCollision;

    public Sprite[] mineralSprites;
    public Sprite[] treeSprites;
    public Sprite[] tree2Sprites;
    private Vector2 center;
    float maxDistanceToEdgeOfMap;
    float dividesDistanceBetweenMinerals;

    private TileLayout layout;

    int tileCountX;
    int tileCountY;

    private void Awake()
    {
        try
        {
            layout = FindObjectOfType<TileLayout>();
        } catch(System.Exception e)
        {
            Debug.Log("Could not find tileLayout or placeObject scipt. Please add");
        }
        tileCountX = layout.getTileCountX();
        tileCountY = layout.getTileCountY();
        center = new Vector2(tileCountX / 2, tileCountY / 2);
        maxDistanceToEdgeOfMap = Mathf.Sqrt((tileCountX/2)*(tileCountX/2) + (tileCountY/2)*(tileCountY/2));
        dividesDistanceBetweenMinerals = maxDistanceToEdgeOfMap / minerals.Length;
    }

    private void Start()
    {
        PlaceObstacles();
        PlaceInvisibleCollisions();
        PlaceObjects.Instance.doneInitialize = true;

        //this is critical - Clarence
        AstarPath.active.Scan(); //scans the map to create grid graph for pathfinding
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

                            if (Random.Range(1, 20) > 2)
                            {
                                //Pick object to spawn
                                int random = Random.Range(0, 20);
                                float distance = Vector2.Distance(center, new Vector2(i, j));

                                // Debug.Log(distance);

                                int offset = Random.Range(-1, 1);
                                int mineralIndex = Mathf.RoundToInt(distance / dividesDistanceBetweenMinerals);


                                mineralIndex += offset;

                                if (mineralIndex > minerals.Length - 1) { mineralIndex = minerals.Length - 1; }
                                else if (mineralIndex < 0) { mineralIndex = 0; }
                                GameObject mineralToSpawn = minerals[mineralIndex];
                                PlaceObjects.Instance.CreateObject(mineralToSpawn, i, j);
                            } else
                            {
                                if (Random.Range(0, 4) == 0)
                                {
                                    PlaceObjects.Instance.CreateObject(tree2, i, j);
                                } else
                                {
                                    int spriteIndex = Random.Range(0, treeSprites.Length);
                                    PlaceObjects.Instance.CreateObject(tree, i, j);
                                    TileLayout.Instance.GetTile(i, j).getObjectOnTile().GetComponent<SpriteRenderer>().sprite = treeSprites[spriteIndex];
                                }

                            }
                        }
                        break;
                    }
                }
            }
        }
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
                        PlaceObjects.Instance.CreateObject(invisibleCollision, i, j);
                        TileLayout.Instance.GetTile(i, j).setBreakMode(TileMode.unbreakable);
                        break;
                    }
                }
            }
        }

        foreach (Vector2Int tilePos in shipTilePositions)
        {
            PlaceObjects.Instance.CreateObject(invisbleBarrierNoCollision, tilePos.x, tilePos.y);
            TileLayout.Instance.GetTile(tilePos.x, tilePos.y).setBreakMode(TileMode.unbreakable);
        }
    }
}

