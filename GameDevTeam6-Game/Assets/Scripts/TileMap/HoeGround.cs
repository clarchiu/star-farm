using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoeGround : MonoBehaviour
{
    public GameObject soil;
    private PlaceObjects place;
    private MultiTool tool;
    private GameObject player;

    private void Awake()
    {
        place = FindObjectOfType<PlaceObjects>();
        tool = FindObjectOfType<MultiTool>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (!(tool.GetMode() == ToolModes.hoeMode)) {
            return;
        }
        if (Input.GetMouseButtonDown(0)) {
            CreateSoil();
        }
    }
    void CreateSoil()
    {
        playerDir dir = player.GetComponent<PlayerDirection>().GetDirection();
        int posX = Mathf.RoundToInt(player.transform.position.x);
        int posY = Mathf.RoundToInt(player.transform.position.y);
        switch (dir) {
            case playerDir.up:
                place.CreateObject(soil, posX, posY+1); break;
            case playerDir.down:
                place.CreateObject(soil, posX, posY-1); break;
            case playerDir.left:
                place.CreateObject(soil, posX-1, posY); break;
            case playerDir.right:
                place.CreateObject(soil, posX+1, posY); break;
        }
    }
}
