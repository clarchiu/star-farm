using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropShadow_Moveable : MonoBehaviour
{
    public float offset = 0.125f;
    public Sprite shadowSprite;
    private GameObject shadow;
    SpriteRenderer playerRenderer;
    SpriteRenderer shadowRenderer;


    private void Start()
    {
        shadow = new GameObject("Shadow");
        shadow.transform.parent = transform;

        shadow.transform.localPosition = new Vector2(0, -offset);
        shadow.transform.localRotation = Quaternion.identity;
        shadow.GetComponent<SpriteRenderer>().sprite = shadowSprite;

        playerRenderer = GetComponent<SpriteRenderer>();
        shadowRenderer = shadow.GetComponent<SpriteRenderer>();
        shadowRenderer.sortingLayerName = playerRenderer.sortingLayerName;
        shadowRenderer.sortingOrder = playerRenderer.sortingOrder - 10;
    }

    void LateUpdate()
    {
        shadow.transform.position = new Vector2(transform.position.x, transform.position.y - offset);
        shadowRenderer.sortingOrder = playerRenderer.sortingOrder - 10;
    }
}
