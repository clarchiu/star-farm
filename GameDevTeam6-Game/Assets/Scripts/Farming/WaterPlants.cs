using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlants : MonoBehaviour
{
    MultiTool tool;

    private int maxWater = 5;

    private GameObject indicator;
    private SpriteRenderer indicatorRenderer;
    private Color32 red;
    private Color32 green;
    private GameObject player;
    public Sprite box;

    private void Awake()
    {
        indicator = GameObject.Find("Indicator");
        if (indicator == null)
        {
            Debug.Log("No indicator");
            this.enabled = false;
        }
        tool = FindObjectOfType<MultiTool>();
        indicatorRenderer = indicator.GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        if (player == null)
        {
            Debug.Log("No player");
        }

        red = new Color32(255, 0, 0, 100);
        green = new Color32(30, 255, 0, 100);
    }
    
    private void Update()
    {
        if (tool.GetMode() != ToolModes.wateringMode)
        {
            return;
        }
        PlaceObjects.Instance.GetMouseTile(out int tileX, out int tileY);
        indicator.transform.position = new Vector2(tileX, tileY);
        indicator.GetComponent<SpriteRenderer>().sprite = box;

        bool objectExists = (TileLayout.Instance.GetTile(tileX, tileY) != null && TileLayout.Instance.GetTile(tileX, tileY).getObjectOnTile() != null);
        bool isFarmTile = (objectExists && TileLayout.Instance.GetTile(tileX, tileY).getObjectOnTile().GetComponent<FarmTile>() != null);
        //Set indicator color
        if (!isFarmTile)
        {
            indicatorRenderer.color = red;
            return;
        }
        else
        {
            indicatorRenderer.color = green;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (isFarmTile && player.GetComponent<PlayerStates>().GetState() != playerStates.INTERACTING)
            {
                player.GetComponent<PlayerStates>().ChangeState(playerStates.INTERACTING);
                TileLayout.Instance.GetTile(tileX, tileY).getObjectOnTile().GetComponent<FarmTile>().WaterTile();
                TileLayout.Instance.GetTile(tileX, tileY).getObjectOnTile().GetComponent<FarmTile>().plant.GetComponent<PlantBehavior>().delay = 30;
            }
        }
         
    }
}
