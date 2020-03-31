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
    private bool initialized = false;


    private void Start()
    {
        if (!initialized)
        {
            Initialize();
        }
    }

    private void Initialize()
    {
        shadow = new GameObject("Shadow");
        shadow.transform.parent = transform;

        shadow.transform.localPosition = new Vector2(0, -offset);
        shadow.transform.localRotation = Quaternion.identity;
        shadow.AddComponent<SpriteRenderer>();

        parentRenderer = GetComponent<SpriteRenderer>();
        shadowRenderer = shadow.GetComponent<SpriteRenderer>();
        shadowRenderer.sortingLayerName = parentRenderer.sortingLayerName;
        shadowRenderer.sortingOrder = parentRenderer.sortingOrder - 10;
        initialized = true;
        UpdateShadow();
    }

    public void UpdateShadow() {
        if (!initialized)
        {
            Initialize();
        }
        if (shadowType == ShadowType.tree1)
        {
            shadowRenderer.sprite = ResourceManager.Instance.treeShadow1;
            shadowRenderer.material = ResourceManager.Instance.shadowMaterial;
        }
        else if (shadowType == ShadowType.tree2)
        {
            shadowRenderer.sprite = ResourceManager.Instance.treeShadow2;
            shadowRenderer.material = ResourceManager.Instance.shadowMaterial;
        }
        else if (shadowType == ShadowType.reflect)
        {
            shadowRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
            shadowRenderer.material = ResourceManager.Instance.shadowMaterial;
        }
        else if (shadowType == ShadowType.playersAndEnemies)
        {
            shadowRenderer.sprite = ResourceManager.Instance.playersAndEnemiesShadow;
            shadowRenderer.material = ResourceManager.Instance.shadowMaterial;
            shadow.AddComponent<MoveShadow>();
            shadow.GetComponent<MoveShadow>().Initialize(offset, gameObject);
        }
    }

}

public enum ShadowType
{
    reflect,
    tree1,
    tree2,
    playersAndEnemies
}
