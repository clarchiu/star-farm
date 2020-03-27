using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmTile : MonoBehaviour
{
    [HideInInspector]
    public GameObject plant = null;
    private bool isWatered = false;

    public void WaterTile()
    {
        if (!isWatered)
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            Color32 color = new Color32(170, 170, 170, 255);
            renderer.color = color;
            isWatered = true;
        }
    }
}
