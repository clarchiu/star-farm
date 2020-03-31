using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShadow : MonoBehaviour
{
    float offset;
    SpriteRenderer shadowRenderer;
    SpriteRenderer parentRenderer;
    GameObject parent;

    public void Initialize(float proposedOffset, GameObject proposedParent)
    {
        offset = proposedOffset;
        shadowRenderer = GetComponent<SpriteRenderer>();
        parent = proposedParent;
        parentRenderer = proposedParent.GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        transform.position = new Vector2(parent.transform.position.x, parent.transform.position.y - offset);
        shadowRenderer.sortingOrder = parentRenderer.sortingOrder - 10;
    }
}