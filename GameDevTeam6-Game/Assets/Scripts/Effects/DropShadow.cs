using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Attach this script to objects that you want to have shadows for
public class DropShadow: MonoBehaviour
{
    public ShadowType shadowType;
    public float offset = 0.125f;

    private GameObject shadow;
    SpriteRenderer parentRenderer;
    SpriteRenderer shadowRenderer;

    private void Start()
    {
        shadow = new GameObject("Shadow");
        shadow.transform.parent = transform;

        shadow.transform.localPosition = new Vector2(0, -offset);
        shadow.transform.localRotation = Quaternion.identity;
        shadow.AddComponent<SpriteRenderer>();

        if (shadowType == ShadowType.tree1)
        {
            shadow.GetComponent<SpriteRenderer>().sprite = ResourceManager.Instance.treeShadow1;
            shadow.GetComponent<SpriteRenderer>().material = ResourceManager.Instance.shadowMaterial;
        }
        else if (shadowType == ShadowType.tree2)
        {
            shadow.GetComponent<SpriteRenderer>().sprite = ResourceManager.Instance.treeShadow2;
            shadow.GetComponent<SpriteRenderer>().material = ResourceManager.Instance.shadowMaterial;
        }
        else if (shadowType == ShadowType.reflect)
        {
            shadow.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
            shadow.GetComponent<SpriteRenderer>().material = ResourceManager.Instance.shadowMaterial;
        }
        else if (shadowType == ShadowType.playersAndEnemies)
        {
            shadow.GetComponent<SpriteRenderer>().sprite = ResourceManager.Instance.playersAndEnemiesShadow;
            shadow.GetComponent<SpriteRenderer>().material = ResourceManager.Instance.shadowMaterial;
            shadow.AddComponent<MoveShadow>();
            shadow.GetComponent<MoveShadow>().Initialize(offset, gameObject);
        }

        parentRenderer = GetComponent<SpriteRenderer>();
        shadowRenderer = shadow.GetComponent<SpriteRenderer>();
        shadowRenderer.sortingLayerName = parentRenderer.sortingLayerName;
        shadowRenderer.sortingOrder = parentRenderer.sortingOrder - 10;
    }

}

public enum ShadowType
{
    reflect,
    tree1,
    tree2,
    playersAndEnemies
}
