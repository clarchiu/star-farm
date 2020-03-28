using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    private GameObject indicator;

    private MultiTool tool;
    private GameObject player;
    private Color32 red;
    private Color32 green;
    private Color32 orange;
    public Sprite blank;
    private SpriteRenderer indicatorRenderer;
    public bool doneInitialize = false;

    private static PlaceObjects _instance;
    public static PlaceObjects Instance
    {
        get
        {
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<PlaceObjects>();
                }
                if (_instance == null)
                {
                    Debug.Log("PlaceObjects script not found!, Add TileMap controller prefab to your scene!");
                }
                return _instance;
            }
        }
    }

    //creates audio sources and audioclips for place and destroy object sounds
    AudioSource Placeobject_audiosource;
    public AudioClip placeobject_sound;
    AudioSource destroyobject_audiosource;
    public AudioClip destroyobject_sound;

    private void Awake() {


        tool = FindObjectOfType<MultiTool>();
        if (!tool)
        {
            gameObject.SetActive(false);
            Debug.LogWarning("Multitool object was not found! Place multiTool prefab into your scene!");
            gameObject.SetActive(false);
        }
        try
        {
            player = GameObject.FindGameObjectWithTag("Player");
        } catch (System.Exception e)
        {
            Debug.Log("Player prefab not found! Put player prefab into your scene!");
            gameObject.SetActive(false);
        }
        try
        {
            indicator = GameObject.Find("Indicator");
        } catch (System.Exception e)
        {
            Debug.Log("Indicator prefab not found! Put Indicator prefab into your scene!");
            gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        red = new Color32(255, 0, 0, 100);
        green = new Color32(30, 255, 0, 100);
        orange = new Color32(200, 150, 0, 100);
        indicatorRenderer = indicator.GetComponent<SpriteRenderer>();
        // gets audiosource components
        Placeobject_audiosource = GetComponent<AudioSource>();
        destroyobject_audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!(tool.GetMode() == ToolModes.buildingMode))
        {
            return;
        }

        GameObject currentObject = ResourceManager.Instance.GetGenObject(GenSelector.Instance.chosenSeed);

        if (Inventory_gen.Instance.FindAmount(GenSelector.Instance.chosenSeed) > 0)
        {
            indicatorRenderer.sprite = currentObject.GetComponent<SpriteRenderer>().sprite;
        } else
        {
            indicatorRenderer.sprite = blank;
        }
        GetMouseTile(out int tileX, out int tileY);
        indicator.transform.position = new Vector2(tileX, tileY);

        if (InBounds(tileX, tileY) && NearPlayer(tileX, tileY, 2))
        {
            GameObject tileObject = GetComponent<TileLayout>().GetTile(tileX, tileY).getObjectOnTile();
            if (tileObject == null)
            {
                indicatorRenderer.color = green;
                if (Input.GetMouseButtonDown(0))
                {
                    if (Inventory_gen.Instance.FindAmount(GenSelector.Instance.chosenSeed) > 0)
                    {
                        if (!InBounds(tileX, tileY))
                        {
                            Debug.Log("Tried to create an object outside of bounds and failed");
                            return;
                        }
                        ObjectTile tile = GetComponent<TileLayout>().GetTile(tileX, tileY);
                        GameObject oldObj = tile.getObjectOnTile();
                        if (oldObj == null)
                        {
                            Inventory_gen.Instance.RemoveItem(GenSelector.Instance.chosenSeed, 1);
                            CreateObject(currentObject, tileX, tileY);
                        }
                    }
                    //plays place object sound once
                    Placeobject_audiosource.PlayOneShot(placeobject_sound, 0.5f);
                }
            }
            else
            {
                indicatorRenderer.color = orange;
                if (Input.GetMouseButtonDown(1)) {
                    //DestroyObject(tileX, tileY);
                    DamageObject(tileX, tileY);
                    player.GetComponent<PlayerStates>().ChangeState(playerStates.INTERACTING);
                }
            }
        }
        else
        {
            indicatorRenderer.color = red;
        }
    }


    //Creates object at Tile[x,y] if there is no other object
    public void CreateObject(GameObject newObj, int x, int y)
    {
        if (!InBounds(x, y))
        {
            Debug.Log("Tried to create an object outside of bounds and failed");
            return;
        }
        ObjectTile tile = GetComponent<TileLayout>().GetTile(x, y);
        GameObject oldObj = tile.getObjectOnTile();
        if (oldObj == null) {
            SoundEffects_.Instance.PlaySoundEffect(SoundEffect.objectPlace);
            Vector2 position = new Vector2(x, y);
            GameObject obj = Instantiate(newObj, position, Quaternion.identity);
            tile.setObjectOnTile(obj);
            if (doneInitialize)
            {
                AstarPath.active.Scan(); //scans the map to create grid graph for pathfinding
            }
            if (obj.GetComponent<SpriteRenderer>() != null)
            {
                obj.GetComponent<SpriteRenderer>().sortingOrder = -y;
            }
        }
    }

    private void DamageObject(int x, int y)
    {
        ObjectTile tile = GetComponent<TileLayout>().GetTile(x, y);
        GameObject objectOnTile = tile.getObjectOnTile();

        if (objectOnTile.GetComponent<ITargetable>() == null)
        {
            Debug.Log("Object behavior not attached to object. Destroying objects require this script to be attached");
        } else
        {
            if (PlayerStates.Instance.GetState() == playerStates.IDLE)
            {
                //SoundEffects_.Instance.PlaySoundEffect(SoundEffect.breaking);
                destroyobject_audiosource.PlayOneShot(destroyobject_sound, 1);
                objectOnTile.GetComponent<ITargetable>().RemoveHealth(objectOnTile, PlayerUpgrades.Instance.obstacleAttackDamage);
            }
        }
    }

    //Destroys object at Tile[x,y] if there is an object there
    public void DestroyObject(int x, int y) {
        if (!InBounds(x, y)) {
            Debug.Log("Tried to destroy an object outside of bounds and failed");
            return;
        }
        ObjectTile tile = GetComponent<TileLayout>().GetTile(x, y);
        GameObject objectOnTile = tile.getObjectOnTile();
        if (objectOnTile != null && tile.getBreakMode() != TileMode.unbreakable) {
            Tutorial.Instance.TriggerDialogue(5);
            GetComponent<TileLayout>().GetTile(x, y).ResetTileInfo();
            Destroy(objectOnTile);
            if (doneInitialize)
            {
                AstarPath.active.Scan(); //scans the map to create grid graph for pathfinding
            }
            PlayEffect.Instance.PlayBreakEffect(new Vector2(x,y));
            DropItem(objectOnTile);
        }
    }

    public void DropItem(GameObject obj)
    {
        if (obj.GetComponent<Drop_mineral>() == null) {
            Debug.Log("No object drop item set for this object");
        }
        else  {
            for (int i = 0; i < Random.Range(2, 5); i++) {
                obj.GetComponent<Drop_mineral>().DropItem(obj.transform.position.x, obj.transform.position.y);
            }
        }
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

    public bool NearPlayer(int x, int y, int limit)
    {
        int playerX = Mathf.RoundToInt(player.transform.position.x);
        int playerY = Mathf.RoundToInt(player.transform.position.y);
        return (Mathf.Abs(x - playerX) < limit && Mathf.Abs(y - playerY) < limit);
    }
}
