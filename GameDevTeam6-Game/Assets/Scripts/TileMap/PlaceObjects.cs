using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    public GameObject currentObject;
    public GameObject indicator;

    private MultiTool tool;
    private GameObject player;
    private Color32 red;
    private Color32 green;
    private Color32 orange;
    private SpriteRenderer indicatorRenderer;

    private void Awake() {
        tool = FindObjectOfType<MultiTool>();
        if (!tool) {
            gameObject.SetActive(false);
            Debug.LogWarning("no tool found!!");
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start() {
        red = new Color32(255, 0, 0, 100);
        green = new Color32(30, 255, 0, 100);
        orange = new Color32(200, 150, 0, 100);
        indicatorRenderer = indicator.GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (!(tool.GetMode() == ToolModes.buildingMode)) {
            return;
        }
        indicatorRenderer.sprite = currentObject.GetComponent<SpriteRenderer>().sprite;
        GetMouseTile(out int tileX, out int tileY);
        indicator.transform.position = new Vector2(tileX, tileY);

        if (InBounds(tileX, tileY) && NearPlayer(tileX, tileY, 4))
        {
            GameObject tileObject = GetComponent<TileLayout>().GetTile(tileX, tileY).getObjectOnTile();
            if (tileObject == null)
            {
                indicatorRenderer.color = green;
                if (Input.GetMouseButtonDown(0))
                {
                    CreateObject(currentObject, tileX, tileY);
                }
            }
            else {
                indicatorRenderer.color = orange;
                if (Input.GetMouseButtonDown(1)) {
                    DestroyObject(tileX, tileY);
                    //PlayAttackAnimation();
                }
            }
        } else
        {
            indicatorRenderer.color = red;
        }
    }


    //Creates object at Tile[x,y] if there is no other object
    public void CreateObject(GameObject newObj, int x, int y) {
        if (!InBounds(x, y)) {
            Debug.Log("Tried to create an object outside of bounds and failed");
            return;
        }
        ObjectTile tile = GetComponent<TileLayout>().GetTile(x, y);
        GameObject oldObj = tile.getObjectOnTile();
        if (oldObj == null) {
            Vector2 position = new Vector2(x, y);
            GameObject obj = Instantiate(newObj, position, Quaternion.identity);
            tile.setObjectOnTile(obj);
        }
    }
    //Destroys object at Tile[x,y] if there is an object there
    public void DestroyObject(int x, int y) {
        Tutorial.Instance.TriggerDialogue(5);
        if (!InBounds(x, y)) {
            Debug.Log("Tried to destroy an object outside of bounds and failed");
            return;
        }

        ObjectTile tile = GetComponent<TileLayout>().GetTile(x, y);
        GameObject objectOnTile = tile.getObjectOnTile();
        if (objectOnTile != null) {
            Destroy(objectOnTile);
        }
        GetComponent<TileLayout>().GetTile(x, y).ResetTileInfo();
    }


    public bool InBounds(int x, int y) {
        if (x < 0 || x > GetComponent<TileLayout>().tileCountX - 1) {
            return false;
        }
        if (y < 0 || y > GetComponent<TileLayout>().tileCountY - 1) {
            return false;
        }
        return true;
    }

    public void GetMouseTile(out int tileX, out int tileY) {
        Vector2 mouseToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tileX = Mathf.RoundToInt(mouseToWorld.x);
        tileY = Mathf.RoundToInt(mouseToWorld.y);
    }

    public bool NearPlayer(int x, int y, int limit) {
        int playerX = Mathf.RoundToInt(player.transform.position.x);
        int playerY = Mathf.RoundToInt(player.transform.position.y);
        return (Mathf.Abs(x - playerX) < limit && Mathf.Abs(y - playerY) < limit);
    }
    /*
    public void PlayAttackAnimation()
    {
        playerDir dir = player.GetComponent<PlayerDirection_>().GetDirection();
        Animator ani = player.GetComponent<Animator>();

        ani.speed = 1;
        if (dir == playerDir.left)
        {
            ani.Play("attack_left");
        }
        else if (dir == playerDir.right)
        {
            ani.Play("attack_right");
        }
        else if (dir == playerDir.up)
        {
            ani.Play("attack_up");
        }
        else if (dir == playerDir.down)
        {
            ani.Play("attack_down");
        }

    }
    */
}
