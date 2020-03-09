using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HoeGround : MonoBehaviour
{
    private PlaceObjects place;
    private MultiTool tool;
    private GameObject player;

    public GameObject indicator;
    private SpriteRenderer indicatorRenderer;

    public GameObject plant, farmTile; //Temporary

    private Color32 red, green, orange;

    private void Awake()
    {
        place = FindObjectOfType<PlaceObjects>();
        tool = FindObjectOfType<MultiTool>();
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

        bool objectExists = (TileLayout.Instance.GetTile(tileX, tileY) != null && TileLayout.Instance.GetTile(tileX, tileY).getObjectOnTile() != null);
        bool isFarmTile = (objectExists && TileLayout.Instance.GetTile(tileX, tileY).getObjectOnTile().GetComponent<FarmTile>() != null);
        bool plantExists = (isFarmTile && TileLayout.Instance.GetTile(tileX, tileY).getObjectOnTile().GetComponent<FarmTile>().plant != null);
        bool leftMouse = Input.GetMouseButtonDown(0);
        bool rightMouse = Input.GetMouseButtonDown(1);

        //Set indicator color
        if (!place.InBounds(tileX, tileY) || !place.NearPlayer(tileX, tileY, 2) || (objectExists && !plantExists)) {
            indicatorRenderer.color = red;
        }
        else {
            indicatorRenderer.color = green;
        }

        if (!isFarmTile) {
            indicator.GetComponent<SpriteRenderer>().sprite = farmTile.GetComponent<SpriteRenderer>().sprite;
        } else {
            indicator.GetComponent<SpriteRenderer>().sprite = plant.GetComponent<SpriteRenderer>().sprite;
        }

        //Create plant
        if (isFarmTile && leftMouse)
        {
            Tutorial.Instance.TriggerDialogue(6);
            GameObject plantObj = Instantiate(plant, new Vector2(tileX, tileY), Quaternion.identity);
            TileLayout.Instance.GetTile(tileX, tileY).getObjectOnTile().GetComponent<FarmTile>().plant = plantObj;
            player.GetComponent<PlayerStates>().ChangeState(playerStates.INTERACTING);
        }
        //Remove plant
        else if (plantExists && rightMouse && !plantExists)
        {
            Tutorial.Instance.TriggerDialogue(10);
            Tutorial.Instance.TriggerDialogue(11);
            GameObject plant = TileLayout.Instance.GetTile(tileX, tileY).getObjectOnTile().GetComponent<FarmTile>().plant;
            place.DropItem(plant);
            Destroy(plant);
            TileLayout.Instance.GetTile(tileX, tileY).getObjectOnTile().GetComponent<FarmTile>().plant = null;
            Inventory_mineral.Instance.GainItem(Mineral_type.copper, 1);
            player.GetComponent<PlayerStates>().ChangeState(playerStates.INTERACTING);
        }
        //Create farm tile
        else if (!isFarmTile && leftMouse)
        {
            place.CreateObject(farmTile, tileX, tileY);
            TileLayout.Instance.GetTile(tileX, tileY).setBreakMode(TileMode.farm);
            player.GetComponent<PlayerStates>().ChangeState(playerStates.INTERACTING);
        }
        //Remove farm tile
        else if (isFarmTile && rightMouse && !plantExists)
        {    
            place.DestroyObject(tileX, tileY);
            TileLayout.Instance.GetTile(tileX, tileY).ResetTileInfo();
            player.GetComponent<PlayerStates>().ChangeState(playerStates.INTERACTING);
        }
    }
}
