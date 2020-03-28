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

    public GameObject farmTile; //Temporary

    private int selectedPlantIndex = 0;
    public Sprite indicatorSprite;

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


        if (!isFarmTile) {
            indicator.GetComponent<SpriteRenderer>().sprite = farmTile.GetComponent<SpriteRenderer>().sprite;
        } else {
            indicator.GetComponent<SpriteRenderer>().sprite = indicatorSprite;
        }

        //Set indicator color
        if (!place.InBounds(tileX, tileY) || !place.NearPlayer(tileX, tileY, 2) || (objectExists && !isFarmTile))
        {
            indicatorRenderer.color = red;
            return;
        }
        else
        {
            indicatorRenderer.color = green;
        }

        //Create plant
        if (isFarmTile && leftMouse && !plantExists)
        {
            if (Inventory_Seeds.Instance.FindAmount(SeedSelector.Instance.chosenSeed) > 0) {
                GameObject selectedPlant = ResourceManager.Instance.GetSeedObject(SeedSelector.Instance.chosenSeed);
                Tutorial.Instance.TriggerDialogue(6);
                GameObject plantObj = Instantiate(selectedPlant, new Vector2(tileX, tileY), Quaternion.identity);
                Inventory_Seeds.Instance.RemoveItem(SeedSelector.Instance.chosenSeed, 1);
                TileLayout.Instance.GetTile(tileX, tileY).getObjectOnTile().GetComponent<FarmTile>().plant = plantObj;

                SoundEffects_.Instance.PlaySoundEffect(SoundEffect.objectPlace);
            }

            player.GetComponent<PlayerStates>().ChangeState(playerStates.INTERACTING);
        }
        //Remove plant
        else if (plantExists && rightMouse)
        {
            GameObject plant = TileLayout.Instance.GetTile(tileX, tileY).getObjectOnTile().GetComponent<FarmTile>().plant;
            if (plant.GetComponent<Plants>().getStages() == 5)
            {
                Tutorial.Instance.TriggerDialogue(10);
                place.DropItem(plant);
            }
            PlayEffect.Instance.PlayBreakEffect(new Vector2(tileX, tileY));
            Destroy(plant);
            TileLayout.Instance.GetTile(tileX, tileY).getObjectOnTile().GetComponent<FarmTile>().plant = null;
            player.GetComponent<PlayerStates>().ChangeState(playerStates.INTERACTING);

            SoundEffects_.Instance.PlaySoundEffect(SoundEffect.harvest);
        }
        //Create farm tile
        else if (!isFarmTile && leftMouse)
        {
            place.CreateObject(farmTile, tileX, tileY);
            TileLayout.Instance.GetTile(tileX, tileY).setBreakMode(TileMode.farm);
            player.GetComponent<PlayerStates>().ChangeState(playerStates.INTERACTING);

            SoundEffects_.Instance.PlaySoundEffect(SoundEffect.hit);
        }
        //Remove farm tile
        else if (isFarmTile && rightMouse && !plantExists)
        {    
            place.DestroyObject(tileX, tileY);
            TileLayout.Instance.GetTile(tileX, tileY).ResetTileInfo();
            PlayEffect.Instance.PlayBreakEffect(new Vector2(tileX, tileY));
            player.GetComponent<PlayerStates>().ChangeState(playerStates.INTERACTING);

            SoundEffects_.Instance.PlaySoundEffect(SoundEffect.hit);
        }
    }
}
