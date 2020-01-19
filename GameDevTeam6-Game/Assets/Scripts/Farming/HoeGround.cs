using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HoeGround : MonoBehaviour
{
    private PlaceObjects place;
    private MultiTool tool;
    private GameObject player;
    [SerializeField]
    private Tilemap tileMap = null;
    private TileLayout tileLayout;
    public Tile[] tiles;

    public GameObject indicator;
    private SpriteRenderer indicatorRenderer;

    public GameObject plant; //Temporary

    private Color32 red, green, orange;

    private void Awake()
    {
        place = FindObjectOfType<PlaceObjects>();
        tool = FindObjectOfType<MultiTool>();
        tileLayout = FindObjectOfType<TileLayout>();
        player = GameObject.FindGameObjectWithTag("Player");

        indicatorRenderer = indicator.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        red = new Color32(255, 0, 0, 100);
        green = new Color32(30, 255, 0, 100);
        orange = new Color32(200, 150, 0, 100);
    }

    private void Update()
    {
        if (!(tool.GetMode() == ToolModes.farmMode))
        {
            return;
        }

        place.GetMouseTile(out int tileX, out int tileY);

        indicator.transform.position = new Vector2(tileX, tileY);

        if (!place.InBounds(tileX, tileY) || !place.NearPlayer(tileX, tileY, 2))
        {
            indicatorRenderer.color = red;
            return;
        }

        if (tileMap.GetTile(new Vector3Int(tileX - 1, tileY - 1, 0)).name != "dirtc")
        {
            indicatorRenderer.sprite = tiles[0].sprite;
            indicatorRenderer.color = green;
            if (Input.GetMouseButtonDown(1))
            {
                tileMap.SetTile(new Vector3Int(tileX - 1, tileY - 1, 0), tiles[0]);
            }
        } else {
            indicatorRenderer.sprite = plant.GetComponent<SpriteRenderer>().sprite;

            if (tileLayout.GetTile(tileX, tileY).getObjectOnTile() == null) {
                indicatorRenderer.color = orange;
                if (Input.GetMouseButtonDown(0))
                {
                    Tutorial.Instance.TriggerDialogue(6);
                    place.CreateObject(plant, tileX, tileY);
                }
                if (Input.GetMouseButtonDown(1))
                {
                    tileMap.SetTile(new Vector3Int(tileX - 1, tileY - 1, 0), tiles[1]);
                }
            } else {
                indicatorRenderer.color = red;
                if (Input.GetMouseButtonDown(1)) {
                    if (tileLayout.GetTile(tileX, tileY).getObjectOnTile().tag == "Plant") {
                        if (tileLayout.GetTile(tileX, tileY).getObjectOnTile().GetComponent<Plants>().getStages() == 5)
                        {
                            Tutorial.Instance.TriggerDialogue(10);
                            Tutorial.Instance.TriggerDialogue(11);
                            Debug.Log("yay");
                        }
                        place.DestroyObject(tileX, tileY);
                    } else {
                        tileMap.SetTile(new Vector3Int(tileX - 1, tileY - 1, 0), tiles[1]);
                    }
                }
            }
        }
    }
}
